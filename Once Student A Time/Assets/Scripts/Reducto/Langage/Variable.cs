using System;
using System.Collections.Generic;
using Reducto;
using UnityEngine;

namespace Langage
{
    public abstract class Variable
    {
        // ------------ Public Méthodes ------------

        public static bool IsValidName(string name)
        {
            if (!IsAlpha(name[0]))
            {
                Interpreteur.Instance.Error = "Invalid name --> first char must be a letter, " +
                                              $"'{name[0]}' isn't alowed";
                return false;
            }

            int l = name.Length;
            for (int i = 1; i < l; i++)
            {
                if (!IsValidChar(name[i]))
                {
                    Interpreteur.Instance.Error = "Invalid name --> a variable name can have letter, " +
                                                  $"digit or '_' but '{name[i]}' is not allowed";
                    return false;
                }
            }

            return true;
        }

        public static bool IsValidToken(string[] expressions, int begin, int end, ref Variable var)
        {
            if (Bool.IsIt(expressions, begin, out Bool res))
            {
                var = res;
                return true;
            }

            return IsOperation(expressions, begin, end, ref var);
        }

        public static bool IsOperation(string[] expressions, int begin, int end, ref Variable var)
        {
            // transformer expressions en une string en remplaçant toutes
            // les variables par leurs valeurs
            string newExpression = "";
            Variable interVar = null;
            string potVar, s;

            for (int i = begin; i < end; i++)
            {
                s = expressions[i];
                int l = s.Length;

                for (int j = 0; j < l; j++)
                {
                    potVar = "";
                    
                    for (; j < l && IsValidChar(s[j]); j++)
                    {
                        potVar += s[j];
                    }
                    
                    if (IsVariableEnregistred(potVar, ref interVar))
                    {
                        if (!(interVar is Int))
                        {
                            Interpreteur.Instance.Error = "Bool can't be in a operation";
                            return false;
                        }
                    
                        newExpression += interVar.ToString();
                    }
                    else
                    {
                        newExpression += potVar;
                    }
                    
                    if (j < l)
                    {
                        newExpression += s[j];
                    }
                }
            }

            return IsOperation(newExpression, ref var);
        }

        protected static bool IsOperation(string expression, ref Variable var)
        {
            Polynomial res;
            try
            { 
                res = Reducto.Reducto.Parse(expression);
            }
            catch (Exception e)
            {
                Debug.Log($"Erreur shunting : {e}");
                return false;
            }

            if (res.Monomials.Count != 1)
            {
                throw new Exception();
            }
            
            var = new Int(res.Monomials[0].Coef);
            return true;
        }
        
        private static bool IsVariableEnregistred(string expression, ref Variable var)
        {
            // variable
            Dictionary<string, Variable> variables = Interpreteur.Instance.Variables;
            
            Debug.Log($"expression = '{expression}', contain --> {variables.ContainsKey(expression)}");

            if (variables.ContainsKey(expression))
            {
                var = variables[expression];
                return true;
            }

            return false;
        }
        
        // ------------ Protected Méthodes ------------
        
        protected static bool IsAlpha(char c)
        {
            return 'a' <= c && c <= 'z' ||
                   'A' <= c && c <= 'Z';
        }

        protected static bool IsDigit(char c)
        {
            return '0' <= c && c <= '9';
        }
        
        protected static bool IsValidChar(char c)
        {
            return IsAlpha(c) ||
                   IsDigit(c) ||
                   c == '_';
        }
        
        // ------------ Surchargeur ------------

        protected abstract bool Equal(Variable var2);
        
        public static bool operator ==(Variable var1, Variable var2)
        {
            Error(var1, var2);

            return var1.Equal(var2);
        }

        public static bool operator !=(Variable var1, Variable var2)
        {
            return !(var1 == var2);
        }
        
        protected abstract bool Inferior(Variable var2);
        
        public static bool operator <(Variable var1, Variable var2)
        {
            Error(var1, var2);

            return var1.Inferior(var2);
        }
        
        public static bool operator >(Variable var1, Variable var2)
        {
            Error(var1, var2);

            return !(var1 <= var2);
        }
        
        public static bool operator >=(Variable var1, Variable var2)
        {
            return !(var1 < var2);
        }
        
        public static bool operator <=(Variable var1, Variable var2)
        {
            return var1 < var2 || var1 == var2;
        }

        private static void Error(Variable var1, Variable var2)
        {
            if (var1 is null || var2 is null)
            {
                throw new Exception();
            }
        }
    }
}