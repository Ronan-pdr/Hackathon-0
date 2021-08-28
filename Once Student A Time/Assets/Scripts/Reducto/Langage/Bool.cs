using System;
using System.Collections.Generic;
using UnityEngine;

namespace Langage
{
    public class Bool : Variable
    {
        // ------------ Attributs ------------
        
        public bool Key { get;}
        
        // ------------ Constructeur ------------

        private Bool(bool key)
        {
            Key = key;
        }
        
        // ----------- Méthodes -----------
        
        public static bool IsIt(string[] expressions, int i, out Bool res)
        {
            // Il y a le "if" mais pas le ":" dans expressions
            
            int l = expressions.Length;
            res = null;

            Comparator comparator = Interpreteur.Instance.Comparator;
            string expr1 = "";
            string comp = "";
            int j = 0;
            for (; i < l; i++)
            {
                string s = expressions[i];
                for (j = 0; j < s.Length && !comparator.ItBeginWell(s[j]); j++)
                {
                    expr1 += s[j];
                }
                
                if (j < s.Length)
                {
                    // récupérer le comparateur
                    comp = s[j].ToString();
                    j += 1;
                    
                    if (j < s.Length && comparator.ItBeginWell(s[j]))
                    {
                        comp += s[j];
                        j += 1;
                    }
                    
                    break;
                }
            }
            
            Debug.Log($"expr1 = '{expr1}', comp = {comp}");

            if (i == l)
            {
                if (l != 2)
                    return false;
                
                // unique expression
                return UniqueExpression(expressions[1], ref res);
            }
            
            string expr2 = expressions[i].Substring(j);
            for (i += 1; i < l; i++)
            {
                expr2 += expressions[i];
            }

            // triple expression
            return TripleExpression(expr1, comp, expr2, ref res);
        }
        
        private static bool UniqueExpression(string expression, ref Bool var)
        {
            Dictionary<string, Variable> variables = Interpreteur.Instance.Variables;
            
            // variable
            if (variables.ContainsKey(expression) && variables[expression] is Bool)
            {
                var = (Bool) variables[expression];
            }
            
            if (expression == "vrai")
            {
                var = new Bool(true);
                return true;
            }
            
            if (expression == "faux")
            {
                var = new Bool(false);
                return true;
            }

            return false;
        }

        private static bool TripleExpression(string expr1, string comparateur, string expr2, ref Bool res)
        {
            Comparator comparator = Interpreteur.Instance.Comparator;

            if (!comparator.IsIt(comparateur))
            {
                throw new Exception();
            }

            (Variable var1, Variable var2) = (null, null);
            
            if (IsOperation(new []{expr1}, 0, 1, ref var1))
            {}
            else if (UniqueExpression(expr1, ref res))
            {
                var1 = res;
            }
            else
            {
                // it's not a Bool expression
                return false;
            }
            
            if (IsOperation(new []{expr2}, 0, 1, ref var2))
            {}
            else if (UniqueExpression(expr2, ref res))
            {
                var2 = res;
            }
            else
            {
                // it's not a Bool expression
                return false;
            }

            res = new Bool(comparator.Compare(var1, comparateur, var2));
            return true;
        }
        
        // ------------ Surchargeur ------------

        protected override bool Equal(Variable b2)
        {
            if (b2 is null)
            {
                throw new Exception();
            }

            return Key == ((Bool)b2).Key;
        }

        protected override bool Inferior(Variable var2)
        {
            return false;
        }

        public override string ToString()
        {
            return Key.ToString();
        }
    }
}