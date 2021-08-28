namespace Langage
{
    public abstract class KeyWord
    {
        // ------------ Attributs ------------
        
        public bool IsEnd { get; protected set; }
        
        // ------------ Méthods ------------

        public abstract Variable Action(string[] expressions, ref int i);
    }
}