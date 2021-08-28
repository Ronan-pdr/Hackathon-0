using System.Collections.Generic;

namespace Reducto
{
    public class Valeur : Token
    {
        // attributs
        private Polynomial _val;
        public Polynomial Val => _val;
        private static char _variable = 'x';
        
        
        // constructeur
        public Valeur(string expression, ref int i, int l)
        {
            List<int> coefficients = new List<int>();
            int degre = 0;
            string n = "";

            for (; i < l && IsIt(expression[i]); i++)
            {
                char c = expression[i];
                if (IsVariable(c))
                {
                    degre += 1;

                    if (n.Length > 0)
                    {
                        coefficients.Add(int.Parse(n));
                        n = "";
                    }
                }
                else
                {
                    n += c;
                }
            }
            
            if (n.Length > 0)
            {
                coefficients.Add(int.Parse(n));
            }
            
            i -= 1;
            int coef = 1;
            foreach (int e in coefficients)
            {
                coef *= e;
            }

            _val = new Polynomial(new Monomial(coef, degre));
        }
        
        // méthodes
        public static bool IsIt(char c)
        {
            return IsChiffre(c) || IsVariable(c);
        }

        private static bool IsChiffre(char c)
        {
            return '0' <= c && c <= '9';
        }
        
        private static bool IsVariable(char c)
        {
            return c == _variable;
        }
        
        // surchargeur
        public override string ToString()
        {
            return _val.ToString();
        }
    }
}