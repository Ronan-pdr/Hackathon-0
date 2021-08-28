using System;
using Manager;
using UnityEngine;
namespace Mission
{
    public enum ColorMission
    {
        Green,
        Red
    }
    
    public class OurEvent : MonoBehaviour
    {
        // ------------ SerializeField ------------
        
        [Header("Message")]
        [SerializeField] private string messHappen;
        [SerializeField] private string messNotHappen;
        
        [Header("Note")]
        [SerializeField] private int noteHappen;
        [SerializeField] private int noteNotHappen;
        
        [Header("Précision")]
        [SerializeField] private bool badWhenHappen;
        
        // ------------ Attributs ------------
        
        private bool _happened;
        private int _day;
        
        // ------------ Getter ------------
        
        public string GetMess()
        {
            return _happened ? messHappen : messNotHappen;
        }
        public int GetNote()
        {
            return _happened ? noteHappen : noteNotHappen;
        }

        public ColorMission GetColor()
        {
            return _happened != badWhenHappen ? ColorMission.Green : ColorMission.Red;
        }
        
        // ------------ Setter ------------
        
        public void Reinitialiser()
        {
            _happened = false;
        }
        
        public void Happen()
        {
            // est ce le jour de l'event ?
            if (ManagerDay.Instance.Day + 1 == _day)
            {
                Debug.Log("Happened");
                _happened = true;
            }
        }
        // ------------ Constructor ------------
        
        private void Awake()
        {
            _happened = false;
            _day = GetComponentInParent<ManageEvent>().Day;
        }
    }
}