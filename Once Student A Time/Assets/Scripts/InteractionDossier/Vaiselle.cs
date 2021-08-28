using System.Collections;
using System.Collections.Generic;
using Manager;
using Mission;
using UnityEngine;

namespace Interaction
{
    public class Vaiselle : InteractionDossier.Interaction
    {
        [SerializeField] private OurEvent vaiselleEvent;
        
        // ---------- Constructeur ----------

        public override void Constructor()
        {}

        // ---------- Méthodes ----------

        public override void Touche()
        {
            StartCoroutine(Message());
            vaiselleEvent.Happen();
            gameObject.SetActive(false);
        }

        // ---------- Coroutine ----------

        private IEnumerator Message()
        {
            ManagerDay.Instance.SetPensée("Vous avez fait la vaiselle");
            yield return new WaitForSeconds(2); // Attend 2s
            ManagerDay.Instance.SetPensée("");
        }
    }
}