using System;
using System.Collections.Generic;
using Manager;
using TMPro;
using UnityEngine;

namespace Langage
{
    public class Interpreteur : MonoBehaviour
    {
        // ------------ SerializeField ------------
        
        [Header("Result")]
        [SerializeField] private TextMeshProUGUI output;

        // ------------ Attributs ------------
        public static Interpreteur Instance { get; private set; }

        private Dictionary<string, KeyWord> _keyWords;

        public Dictionary<string, Variable> Variables { get; private set; }
        
        public Comparator Comparator { get; private set; }

        public string Error { private get; set; }
        
        // ------------ Reset ------------

        public void Reset()
        {
            output.text = "";
        }

        // ------------ Constructor ------------

        private void Awake()
        {
            Instance = this;
        }

        private void Initialize()
        {
            Instance = this;

            _keyWords = new Dictionary<string, KeyWord>();
            
            _keyWords.Add("si", new If());
            _keyWords.Add(Else.Key, new Else());
            _keyWords.Add("retourner", new Retourner());

            Comparator = new Comparator();

            Variables = new Dictionary<string, Variable>();
            
            Error = null;
        }
        
        // ------------ Public Méthods ------------

        public void Interprate(TMP_InputField code)
        {
            Instance = this;
            output.text = Interprate(code.text);
        }

        private string Interprate(string code)
        {
            Initialize();
            Variable var = Interprate(code.Split(new []{'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries));

            if (var is null)
            {
                if (Error is null)
                {
                    return "There is an error";
                }
                
                return $"{Error}";
            }

            string mes = $"Result : {var}";
            if (ManagerDay.Instance.GetExo().Test(code))
            {
                mes += Environment.NewLine + "Bien joué !" +
                       Environment.NewLine + "Vous avez fait vos devoirs.";
            }
            
            return mes;
        }
        
        public Variable Interprate(string[] lines)
        {
            Variable var = null;

            int l = lines.Length;
            for (int i = 0; i < l && Error is null; i++)
            {
                string[] expressions = lines[i].Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
                Debug.Log($"expressions = {expressions[0]}");

                if (expressions.Length == 0)
                {
                    // rien, ligne vide
                }
                else if (_keyWords.ContainsKey(expressions[0]))
                {
                    // Manage the other key words
                    KeyWord keyWord = _keyWords[expressions[0]];

                    if (keyWord.IsEnd)
                    {
                        // concerne seulement le return
                        return keyWord.Action(expressions, ref i);
                    }

                    Variable res = keyWord.Action(lines, ref i);
                    
                    if (!(res is null))
                    {
                        return res;
                    }
                }
                else if (IsVarAttribution(expressions, ref var))
                {
                    Variables[expressions[0]] = var;
                }
                else
                {
                    // error
                    Error = "Invalid syntax";
                    return null;
                }
            }

            return null;
        }

        public static bool IsIndented(ref string line)
        {
            if (line.Length < 4)
            {
                return false;
            }

            for (int i = 0; i < 4; i++)
            {
                if (line[i] != ' ')
                {
                    return false;
                }
            }

            line = line.Substring(4);
            return true;
        }
        
        // ------------ Private Méthods ------------

        private bool IsVarAttribution(string[] expressions, ref Variable var)
        {
            int l = expressions.Length;
            if (l < 3 || expressions[1] != "=")
            {
                return false;
            }
            
            if (!Variable.IsValidName(expressions[0]))
            {
                return false;
            }
            
            return Variable.IsValidToken(expressions, 2, l, ref var);
        }
    }
}