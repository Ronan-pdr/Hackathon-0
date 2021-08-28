using System;
using System.ComponentModel;

namespace Reducto
{
    public class OperatorPrio : Operator
    {
        // -------------- attributs --------------
        
        // ceci exprime la hiérarchie des opérateurs à priorité
        private static char[][] operators =
        {
            new []{'^'},
            new []{'*', '/'},
            new []{'+', '-'}
        };

        // corespond à l'index où se situe dans la hiérarchie de 'operators'
        private int nivPrio;
        
        // -------------- constructeur --------------
        private OperatorPrio(char c1, int niv)
        {
            ToChar = c1;
            nivPrio = niv;
        }
        
        // -------------- méthodes --------------
        public static bool IsIt(char c1, ref OperatorPrio o)
        {
            int l1 = operators.Length;
            for (int niv = 0; niv < l1; niv++)
            {
                foreach (char c2 in operators[niv])
                {
                    if (c1 == c2)
                    {
                        o = new OperatorPrio(c1, niv);
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsLeftAssociative() => ToChar != '^';

        public Polynomial Calcul(Polynomial a, Polynomial b)
        {
            switch (ToChar)
            {
                case '*':
                    return a * b;
                case '/':
                    return a / b;
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '^':
                    return a ^ b;
                default:
                    throw new Exception($"L'opérateur {ToChar} n'existe pas !!!");
            }
        }

        // -------------- Les surchargeurs --------------
        // Evaluent la priorité des opérateurs

        public static bool operator ==(OperatorPrio o1, OperatorPrio o2)
        {
            ExceptionNull(o1, o2);

            return o1.nivPrio == o2.nivPrio;
        }
        
        public static bool operator !=(OperatorPrio o1, OperatorPrio o2)
        {
            return !(o1 == o2);
        }

        public static bool operator >(OperatorPrio o1, OperatorPrio o2)
        {
            ExceptionNull(o1, o2);

            return o1.nivPrio < o2.nivPrio;
        }
        
        public static bool operator <(OperatorPrio o1, OperatorPrio o2)
        {
            return o2 > o1;
        }

        private static void ExceptionNull(OperatorPrio o1, OperatorPrio o2)
        {
            if (o1 is null || o2 is null)
                throw new Exception("Pas d'opération avec des opérateurs nulls");
        }
        
        public override string ToString()
        {
            return ToChar.ToString();
        }
    }
}