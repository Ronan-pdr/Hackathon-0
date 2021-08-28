using System.Collections;
using System.Security.Cryptography;
using Manager;
using Mission;
using Script.Menu;
using UnityEngine;

namespace Interaction
{
    public class Medicament : InteractionDossier.Interaction
    {
        // ---------- Constructeur ----------

        public override void Constructor()
        {}
        
        // ---------- Méthodes ----------

        public override void Touche()
        {
            StartCoroutine(Message());
            
            Destroy(gameObject);

            Poigne.Instance.MedocBought();
        }
        
        // ---------- Coroutine ----------

        private IEnumerator Message()
        {
            ManagerDay.Instance.SetPensée("Vous avez acheté des médicaments");
            yield return new WaitForSeconds(2); // Attend 2s
            ManagerDay.Instance.SetPensée("");
        }
    }
}