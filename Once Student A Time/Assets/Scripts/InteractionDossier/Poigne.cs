using System.Collections;
using Manager;
using UnityEngine;
using Mission;
using UnityEngine;

namespace Interaction
{
    public class Poigne : InteractionDossier.Interaction
    {
        // ---------- SerializeField ----------

        [Header("Event")]
        [SerializeField] private OurEvent visiteGrandPa;
        [SerializeField] private OurEvent apporterMedoc;
        private string mes;
        
        // ---------- Attibuts ----------

        public static Poigne Instance  { get; private set; }
        private bool _hasMedoc;
        
        // ---------- Setter ----------

        public void MedocBought()
        {
            _hasMedoc = true;
        }
        
        // ---------- Constructeur ----------

        public override void Constructor()
        {
            Instance = this;
            _hasMedoc = false;
        }

        // ---------- Méthodes ----------

        public override void Touche()
        {
            if (visiteGrandPa.gameObject.activeSelf)
            {
                // afficher "vous avez rendu visite à votre grand père"
                
                visiteGrandPa.Happen();
                mes = "Vous avez rendu visite à votre grand-père";
                if (_hasMedoc)
                {
                    
                    apporterMedoc.Happen();
                }
            }
            else
            {
                mes = "Porte fermé";
            }

            StartCoroutine(Message());
        }
        
        // ---------- Coroutine ----------

        private IEnumerator Message()
        {
            ManagerDay.Instance.SetPensée(mes);
            yield return new WaitForSeconds(2); // Attend 2s
            ManagerDay.Instance.SetPensée("");
        }
    }
}