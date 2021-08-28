using System;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine;

using Debug = UnityEngine.Debug;

namespace Reducto
{
    public static class Reducto
    {
        public static Polynomial Parse(string expression)
        {
            Pile<Token> pile = ShuntingYard(expression);
            Polynomial poly = Calcul(pile);

            return poly;
        }

        private static Polynomial Calcul(Pile<Token> pile)
        {
            if (pile.IsEmpty())
            {
                return new Polynomial();
            }

            Token t = pile.Depiler();

            if (t is Valeur) // cas feuille
            {
                return ((Valeur) t).Val;
            }

            if (t is Unaire) // point simple droit
            {
                Polynomial poly = Calcul(pile);
                return ((Unaire)t).Calcul(poly);
            }

            if (t is Parenthese)
            {
                throw new Exception("Impossible d'avoir des parenthèses dans cette fonction");
            }
            
            // cas point double
            Polynomial droite = Calcul(pile);
            Polynomial gauche = Calcul(pile);
            return ((OperatorPrio) t).Calcul(gauche, droite);
        }

        private static Pile<Token> ShuntingYard(string expression)
        {
            Pile<Token> output = new Pile<Token>();
            Pile<Operator> stackOper = new Pile<Operator>();

            // est ce que le précedent était une valeur
            Token.Type prec = Token.Type.None;
            
            int l = expression.Length;
            for (int i = 0; i < l; i++) // tant que 'there are tokens to be read'
            {
                char c = expression[i];
                OperatorPrio o = null; // null sinon le ref gueule
                
                if (IsUselessSyntax(c))
                {
                    Console.WriteLine($"Syntaxe inutile '{c}'");
                }
                else if (Valeur.IsIt(c)) // les valeurs
                {
                    if (prec == Token.Type.Valeur)
                    {
                        throw new ArgumentException("Deux monomes ont été retrouvé à la suite");
                    }

                    if (prec == Token.Type.ParenthèseFermante) // multiplication implicite
                    {
                        OperatorPrio.IsIt('*', ref o);
                        stackOper.Empiler(o);
                    }
                    
                    output.Empiler(new Valeur(expression, ref i, l));
                    prec = Token.Type.Valeur;
                }
                else if (c == '(') // c'est une parenthèse ouvrante
                {
                    if (prec == Token.Type.Valeur) // multiplication implicite
                    {
                        OperatorPrio.IsIt('*', ref o);
                        stackOper.Empiler(o);
                    }
                    
                    stackOper.Empiler(new Parenthese(Token.Type.ParenthèseOuvrante));
                    prec = Token.Type.ParenthèseOuvrante;
                }
                else if (c == ')') // c'est une parenthèse fermante
                {
                    while (!stackOper.IsEmpty() &&
                           stackOper.Premier().GetChar() != '(') // tant qu'on ne trouve pas sa parenthèse ouvrante
                    {
                        output.Empiler(stackOper.Depiler());  
                    }

                    if (stackOper.IsEmpty())
                    {
                        throw new ArgumentException($"Il y a une parenthèse fermante sans l'ouvrante à l'index {i}");
                    }

                    stackOper.Depiler(); // enlever la parenthèse gauche
                    prec = Token.Type.ParenthèseFermante;
                }
                else if (prec == Token.Type.Operateur || prec == Token.Type.None) // unaire
                {
                    if (Unaire.IsIt(c)) // le moins
                    {
                        if (stackOper.IsEmpty())
                        {
                            stackOper.Empiler(new Unaire());
                        }
                        else if (stackOper.Premier() is Unaire) // simplification
                        {
                            Console.WriteLine("annuler deux unaires");
                            stackOper.Depiler(); // les deux unaires s'annulent
                        }
                        else
                        {
                            stackOper.Empiler(new Unaire());
                            Console.WriteLine($"On décale pas '{stackOper.Premier()}'");
                        }
                    }
                    else if (c == '+') // le plus qui est inutile
                    {
                        Console.WriteLine("Useless unaire '+'");
                    }
                    else if (OperatorPrio.IsIt(c, ref o))
                    {
                        throw new ArgumentException($"'{o}' n'est pas un unaire");
                    }
                    else // Charactère qui n'existe juste pas ou des parenthèses
                    {
                        throw new ArgumentException($"Invalid syntaxe : '{c}'");
                    }
                }
                else if (OperatorPrio.IsIt(c, ref o)) // c'est un opérateur avec priorité
                {
                    // On va décaler des opérateurs de la pile 'stackOper' dans la pile 'output'
                    while (MustDecaler(stackOper, o))
                    {
                        output.Empiler(stackOper.Depiler());
                    }

                    stackOper.Empiler(o);
                    prec = Token.Type.Operateur;
                }
                else if ('a' <= c && c <= 'z' || 'A' <= c && c <= 'Z')
                {
                    throw new ArgumentException($"'{c}' n'est pas une variable acceptée");
                }
                else
                {
                    throw new ArgumentException($"Invalid syntaxe : '{c}'");
                }
            }
            
            if (output.IsEmpty())
            {
                throw new ArgumentException("Les expressions vides ne sont pas acceptées");
            }

            if (prec == Token.Type.Operateur)
            {
                throw new ArgumentException("Le dernier operateur ne possède aucune expression à sa droite");
            }

            // plus qu'à tout décaler dans l'autre pile
            while (!stackOper.IsEmpty())
            {
                Operator o = stackOper.Depiler();
                if (o is Parenthese) // c'est une parenthèse
                {
                    throw new ArgumentException("Problème de paranthèse");
                }
                
                output.Empiler(o); 
            }
            
            return output;
        }

        private static bool IsUselessSyntax(char c)
        {
            return c == ' ' || c == '\t';
        }

        private static bool MustDecaler(Pile<Operator> stackOper, OperatorPrio o1)
        {
            if (stackOper.IsEmpty() || stackOper.Premier() is Parenthese) // y'a rien ou c'est une parenthèse
            {
                return false; // on veut pas décaler
            }
            
            if (stackOper.Premier() is Unaire)
            {
                Console.WriteLine("On decale notre unaire");
                return true; // le unaire se fait décaler par n'importe quoi
            }

            OperatorPrio o2 = (OperatorPrio) stackOper.Premier();

            // o2 est en priorité ou (a la même prio et à une naturelle prio de gauche à droite)
            return o1 < o2 || o1 == o2 && o2.IsLeftAssociative();
        }
    }
}