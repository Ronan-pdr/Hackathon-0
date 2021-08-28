using UnityEngine;

namespace InteractionDossier
{
    public abstract class Interaction : MonoBehaviour
    {
        // ---------- Attributs ----------
        
        protected Outline _outline;
        
        // ---------- Constructor ----------

        private void Awake()
        {
            _outline = GetComponent<Outline>();
            Constructor();
        }
        
        // ---------- Abstract Méthodes ----------

        public abstract void Constructor();
        public abstract void Touche();


        // ---------- Event ----------

        private void OnMouseEnter()
        {
            _outline.enabled = true;
        }

        private void OnMouseOver()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Touche();
            }
        }
    
        private void OnMouseExit()
        {
            _outline.enabled = false;
        }
    }
}