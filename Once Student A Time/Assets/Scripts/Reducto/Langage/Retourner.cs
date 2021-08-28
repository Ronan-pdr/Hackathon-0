using UnityEngine;

namespace Langage
{
    public class Retourner : KeyWord
    {
        // ------------ Attributs ------------

        public Retourner()
        {
            IsEnd = true;
        }
        
        
        // ------------ Methods ------------
        public override Variable Action(string[] expressions, ref int i)
        {
            Variable res = null;
            if (Variable.IsValidToken(expressions, 1, expressions.Length, ref res))
            {
                return res;
            }

            return null;
        }
    }
}