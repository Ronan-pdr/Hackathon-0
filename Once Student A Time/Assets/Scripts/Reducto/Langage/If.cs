using System;
using UnityEngine;

namespace Langage
{
    public class If : KeyWord
    {
        // ------------ Attributs ------------

        public If()
        {
            IsEnd = false;
        }
        
        // ------------ Méthods ------------
        public override Variable Action(string[] lines, ref int index)
        {
            int i = index;
            
            if (Error(lines, i))
            {
                return null;
            }

            int l = lines.Length;
            string[] expressions = lines[i].Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
            
            // Vérifier qu'entre le "si" et le ":", il y a un booléen
            if (!Bool.IsIt(expressions, 1, out Bool res))
            {
                Interpreteur.Instance.Error = "Il doit y avoir un booléen entre le 'si' et le ':'";
                return null;
            }

            int nIndentedLineOfIf = FindNIndentation(lines, ref i, ref index);
            int indexMiddle = i;
            
            // enlever les espaces
            for (; i < l && lines[i].Length == 0; i++)
            {}

            if (i == l)
                return null;

            bool thereIsElse;
            int nIndentedLineOfElse = 0;
            if (ThereIsElse(lines[i]))
            {
                if (Error(lines, i))
                {
                    return null;
                }

                // le sinon
                FindNIndentation(lines, ref i, ref index);

                index += 1;

                FindNIndentation(lines, ref i, ref index);
                thereIsElse = true;
            }
            else
            {
                thereIsElse = false;
            }

            string[] indentedLines;
            if (res.Key)
            {
                indentedLines = GetIndentedLines(lines, nIndentedLineOfIf, ref indexMiddle);
                Debug.Log($"index = {indexMiddle} && len = {indentedLines[0]}");
                
                return Interpreteur.Instance.Interprate(indentedLines);
            }

            if (thereIsElse)
            {
                indentedLines = GetIndentedLines(lines, nIndentedLineOfElse, ref i);
                return Interpreteur.Instance.Interprate(indentedLines);
            }

            return null;
        }

        private bool ThereIsElse(string expression)
        {
            // split
            string[] expressions = expression.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);

            return expressions[0].Substring(0, Else.Key.Length) == Else.Key;
        }

        private int FindNIndentation(string[] lines, ref int i, ref int index)
        {
            int l = lines.Length;
            
            int res = 1;
            for (i += 2; i < l && Interpreteur.IsIndented(ref lines[i]); i++)
            {
                res++;
            }

            index += res;
            return res;
        }

        private string[] GetIndentedLines(string[] lines, int nIndentedLine, ref int i)
        {
            string[] linesIndented = new string[nIndentedLine];
                
            i -= nIndentedLine;
            for (int j = 0; j < nIndentedLine; i++, j++)
            {
                linesIndented[j] = lines[i];
            }

            return linesIndented;
        }

        private bool Error(string[] lines, int i)
        {
            // Vérifier qu'il existe une ligne suivante
            int l = lines.Length;
            if (i + 1 == l)
            {
                Interpreteur.Instance.Error = "Après un si ou un sinon, il y a forcément une ligne";
                return true;
            }

            // Vérifier que la ligne suivante est bien indenté
            if (!Interpreteur.IsIndented(ref lines[i + 1]))
            {
                Interpreteur.Instance.Error = "Après un si ou un sinon, la ligne doit être indentée";
                return true;
            }

            string line = lines[i];
            int j;
            for (j = line.Length - 1; j >= 0 && line[j] == ' '; j--)
            {}

            // Vérifier l'existence du ':'
            if (line[j] != ':')
            {
                Interpreteur.Instance.Error = "Les si et les sinon doivent avoir ':' à la fin de la ligne";
                return true;
            }

            lines[i] = lines[i].Substring(0, j);
            return false;
        }
    }
}