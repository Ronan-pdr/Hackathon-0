using System;

namespace Reducto
{
    public abstract class Token
    {
        public enum Type
        {
            Operateur,
            ParenthèseOuvrante,
            ParenthèseFermante,
            Valeur,
            None
        }
    }
}