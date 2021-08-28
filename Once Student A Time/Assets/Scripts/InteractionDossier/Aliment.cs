using System.Collections;
using System.Security.Cryptography;
using Manager;
using Mission;
using TMPro;
using UnityEngine;

namespace Interaction
{
    public class Aliment : InteractionDossier.Interaction
    {
        // ---------- SerializeField ----------
        
        [Header("Type")]
        [SerializeField] private bool bad;

        [Header("Event")]
        [SerializeField] private OurEvent course;
        [SerializeField] private OurEvent sucre;

        // ---------- Constructeur ----------

        public override void Constructor()
        {}

        // ---------- Event ----------

        public override void Touche()
        {
            Destroy(gameObject);
            
            course.Happen();
                
            if (bad)
            {
                sucre.Happen();
            }

            StartCoroutine(Message());
        }
        
        // ---------- Coroutine ----------

        private IEnumerator Message()
        {
            ManagerDay.Instance.SetPensée("Vous avez acheté de la nourriture");
            yield return new WaitForSeconds(2); // Attend 2s
            ManagerDay.Instance.SetPensée("");
        }
    }
}