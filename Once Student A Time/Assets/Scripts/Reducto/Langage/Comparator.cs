using System;
using System.Collections.Generic;
using System.Linq;

namespace Langage
{
    public class Comparator
    {
        // ------------ Attributs ------------

        private Dictionary<string, Func<Variable, Variable, bool>> _comparators;

        private char[] arr = 
        {
            '=', '!', '>', '<'
        };
        
        // ------------ Constructor ------------

        public Comparator()
        {
            _comparators = new Dictionary<string, Func<Variable, Variable, bool>>();
            
            _comparators.Add("==", Equal);
            _comparators.Add("!=", NotEqual);
            _comparators.Add("<", Inferior);
            _comparators.Add(">", Superior);
            _comparators.Add("<=", InferiorOrEqual);
            _comparators.Add(">=", SuperiorOrEqual);
        }
        
        // ------------ Methods ------------

        public bool ItBeginWell(char c)
        {
            return arr.Contains(c);
        }
        
        public bool IsIt(string comparator)
        {
            return _comparators.ContainsKey(comparator);
        }

        public bool Compare(Variable var1, string comparator, Variable var2)
        {
            if (!_comparators.ContainsKey(comparator))
            {
                throw new Exception();
            }

            return _comparators[comparator](var1, var2);
        }

        private bool Equal(Variable var1, Variable var2)
        {
            Error(var1, var2);

            return var1 == var2;
        }
        
        private bool NotEqual(Variable var1, Variable var2)
        {
            Error(var1, var2);

            return var1 != var2;
        }
        
        private bool Inferior(Variable var1, Variable var2)
        {
            Error(var1, var2);

            return var1 < var2;
        }
        
        private bool Superior(Variable var1, Variable var2)
        {
            Error(var1, var2);

            return var1 == var2;
        }
        
        private bool InferiorOrEqual(Variable var1, Variable var2)
        {
            Error(var1, var2);

            return var1 <= var2;
        }
        
        private bool SuperiorOrEqual(Variable var1, Variable var2)
        {
            Error(var1, var2);

            return var1 >= var2;
        }

        private void Error(Variable var1, Variable var2)
        {
            if (var1.GetType() != var2.GetType())
            {
                throw new Exception();
            }
        }
    }
}