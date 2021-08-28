using System;
using UnityEngine;
using UnityEngine.UI;

namespace QCM
{
    public class PageQuestion : Page
    {
        // ------------ SerializeField ------------

        [SerializeField] private bool[] _answers;
        
        // ------------ Attributs ------------

        private Toggle[] _toggles;
        
        // ------------ Setter ------------

        public void Reset()
        {
            if (_toggles is null)
                return;

            foreach (Toggle toggle in _toggles)
            {
                toggle.isOn = false;
            }
        }
        
        // ------------ Constructor ------------

        private void Awake()
        {
            _toggles = GetComponentsInChildren<Toggle>();
            
            if (_answers.Length != _toggles.Length)
            {
                throw new Exception("Les réponses n'ont pas été bien remplies");
            }

            Reset();
        }

        // ------------ Event ------------

        public int Result()
        {
            int l = _toggles.Length;
            for (int i = 0; i < l; i++)
            {
                if (_answers[i] != _toggles[i].isOn)
                {
                    return -1;
                }
            }

            return 2;
        }
    }
}