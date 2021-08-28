using UnityEngine;

namespace Langage
{
    public class Else : KeyWord
    {
        // ------------ Attributs ------------

        public const string Key = "sinon";
        
        // ------------ Constructor ------------

        public Else()
        {
            IsEnd = false;
        }
        
        // ------------ Méthods ------------
        public override Variable Action(string[] expressions, ref int i)
        {
            Interpreteur.Instance.Error = $"It can't have a 'sinon' without a 'si'";
            return null;
        }
    }
}