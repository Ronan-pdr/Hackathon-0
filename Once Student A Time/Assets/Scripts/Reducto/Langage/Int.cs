using System;
using UnityEngine;

namespace Langage
{
    public class Int : Variable
    {
        // ------------ Attributs ------------
        
        private int _key;
        
        // ------------ Constructeur ------------

        public Int(int key)
        {
            _key = key;
        }
        
        // ----------- Méthodes -----------

        public static bool IsIt(string expression, ref Variable var)
        {
            if (int.TryParse(expression, out int n))
            {
                var = new Int(n);
                return true;
            }
            
            return false;
        }
        
        // ------------ Surchargeur ------------

        protected override bool Equal(Variable var2)
        {
            Error(var2);

            return _key == ((Int) var2)._key;
        }

        protected override bool Inferior(Variable var2)
        {
            Error(var2);

            return _key < ((Int) var2)._key;
        }

        private void Error(Variable var2)
        {
            if (!(var2 is Int))
            {
                throw new Exception();
            }
        }
        
        public override string ToString()
        {
            return _key.ToString();
        }
    }
}