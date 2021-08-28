using System;
using System.ComponentModel;

namespace Reducto
{
    public abstract class Operator : Token
    {
        // attribut
        protected char ToChar;
        
        // getter
        public char GetChar() => ToChar;
    }

    public class Unaire : Operator
    {
        // constructeur
        public Unaire()
        {
            ToChar = '-';
        }
        
        // méthodes
        public Polynomial Calcul(Polynomial poly)
        {
            return -poly;
        }

        public static bool IsIt(char c) => c == '-';
        
        // surchargeur
        public override string ToString()
        {
            return "§";
        }
    }
    
    public class Parenthese : Operator
    {
        // constructeur
        public Parenthese(Type type)
        {
            if (type == Type.ParenthèseOuvrante)
            {
                ToChar = '(';
            }
            else if (type == Type.ParenthèseFermante)
            {
                ToChar = ')';
            }
            else
            {
                throw new Exception($"{type} n'est pas une parenthèse");
            }
        }

        // surchargeur
        public override string ToString()
        {
            return ToChar.ToString();
        }
    }
}