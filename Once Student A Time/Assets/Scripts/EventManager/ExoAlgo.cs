using System;
using System.Runtime.CompilerServices;
using Manager;
using UnityEngine;

namespace Mission
{
    public class ExoAlgo : MonoBehaviour
    {
        // ------------ SerializeField ------------

        [SerializeField] private OurEvent eventExoAlgo;
        
        // ------------ Setter ------------

        public void Reset()
        {
            
        }

        // ------------ Tests ------------

        public bool Test(string code)
        {
            switch (ManagerDay.Instance.Day)
            {
                case 0:
                    return false;
                case 1:
                    return Aux("retourner");
                case 2:
                    return Aux("=");
                default:
                    return Aux("si");
            }

            bool Aux(string keyWord)
            {
                if (code.Contains(keyWord))
                {
                    eventExoAlgo.Happen();
                    return true;
                }

                return false;
            }
        }
    }
}