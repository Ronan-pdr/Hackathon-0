using System;

namespace Reducto
{
    public class BinTree<T>
    {
        // attributs
        private BinTree<T> left;
        private BinTree<T> right;
        private T key;
        
        // constructeur
        private BinTree(T key, BinTree<T> left, BinTree<T> right)
        {
            this.key = key;
            this.left = left;
            this.right = right;
        }

        // il faut donner une pile sous le format d'inversion polonaise inversée
        public static BinTree<Token> Expression(Pile<Token> pile)
        {
            if (pile.IsEmpty())
            {
                throw new Exception("La pile ne contient pas une opération valide");
            }

            Token t = pile.Depiler();

            if (t is Valeur) // cas feuille
            {
                return new BinTree<Token>(t, null, null);
            }

            if (t is Unaire) // point simple droit
            {
                return new BinTree<Token>(t, null, Expression(pile));
            }

            if (t is Parenthese)
            {
                throw new Exception("Impossible d'avoir des parenthèses dans cette fonction");
            }
            
            // cas point double
            BinTree<Token> droite = Expression(pile);
            BinTree<Token> gauche = Expression(pile);
            return new BinTree<Token>(t, gauche, droite);
        }
    }
}