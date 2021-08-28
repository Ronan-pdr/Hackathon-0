using System;
using Langage;
using Manager;
using QCM;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Mission
{
    public class ManageEvent : MonoBehaviour
    {
        // ------------ SerializeField ------------
        
        [Header("Precision")]
        [SerializeField] private int day;
        
        [Header("Event Dodo")]
        [SerializeField] private OurEvent eventDodo;
        [SerializeField] private int timeMax;
        
        [Header("Qcm")]
        [SerializeField] private Qcm qcm;
        
        [Header("Positions")]
        [SerializeField] private Transform trCannette;
        [SerializeField] private Transform trTelephone;
        [SerializeField] private Transform trPorteFeuille;
        [SerializeField] private Transform trMasque;
        
        // ------------ Attributs ------------
        
        private OurEvent[] _ourEvents;
        private float _timeBeginOfDay;
        
        // ------------ Getter ------------
        
        public int Day => day;
        public Qcm Qcm => qcm;
        public Transform TrCannette => trCannette;
        public Transform TrTelephone => trTelephone;
        public Transform TrPorteFeuille => trPorteFeuille;
        public Transform TrMasque => trMasque;
        
        // ------------ Setter ------------
        
        public void ResetDay()
        {
            foreach (OurEvent ourEvent in _ourEvents)
            {
                ourEvent.Reinitialiser();
            }
            
            _timeBeginOfDay = Time.time;
            qcm.Reset();
        }
        
        // ------------ Constructor ------------
        
        private void Awake()
        {
            _ourEvents = GetComponentsInChildren<OurEvent>();
            _timeBeginOfDay = Time.time;
        }
        
        // ------------ Méthods ------------
        
        public void ChekEvent(int i)
        {
            _ourEvents[i].Happen();
        }
        
        public void PrintResult(TextMeshProUGUI[] textEndDays)
        {
            float timeDay = (Time.time - _timeBeginOfDay) / 60;
            Debug.Log($"time day :{timeDay}, time time :{Time.time}, begin :{_timeBeginOfDay}");
            if (timeDay < timeMax)
            {
                eventDodo.Happen();
            }
            
            textEndDays[0].text = $"Résumé du jour {Day}:";
            
            int l = _ourEvents.Length;
            int i;
            for (i = 0; i < l; i++)
            {
                if (_ourEvents[i].GetColor() == ColorMission.Green)
                {
                    textEndDays[i + 1].color = Color.green;
                }
                else
                {
                    textEndDays[i + 1].color = Color.red;
                }
                
                textEndDays[i+1].text = _ourEvents[i].GetMess() + Environment.NewLine;
            }

            l = textEndDays.Length;
            for (i += 1; i < l; i++)
            {
                textEndDays[i].text = "";
            }
        }
        
        public int GetNoteResult()
        {
            int note = 0;
            foreach (OurEvent eventt in _ourEvents)
            {
                note += eventt.GetNote();
            }
            return note;    
        }
    }
}
