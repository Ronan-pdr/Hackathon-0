using Mission;
using UnityEngine;
using Interaction;
using Manager;

namespace Movements
{
    public class TeleportScript : MonoBehaviour
    {
        // ---------- SerializeField ----------
    
        [Header("Teleportation")]
        [SerializeField] private Transform toTp;
        [SerializeField] private GameObject player;
        
        [Header("Side")]
        [SerializeField] private bool ToRoom;

        [Header("Event")]
        [SerializeField] private OurEvent sortir;
    
        // ---------- Attributs ----------

        public static TeleportScript Instance { get; private set; }

        private bool _haveMasque;
        private Outline _outline;
    
        // ---------- Constructeur ----------
    
        private void Awake()
        {
            Instance = this;
            _haveMasque = false;
            _outline = GetComponent<Outline>();
        }
        
        // ---------- Setter ----------

        public void MyReset()
        {
            _haveMasque = false;
        }
        
        public void MaskTaken()
        {
            _haveMasque = true;
        }
    
        // ---------- Event ----------

        private void OnMouseEnter()
        {
            _outline.enabled = true;
        }

        private void OnMouseOver()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // téléportation
                player.transform.position = toTp.position;

                if (!ToRoom)
                {
                    // check event
                    sortir.Happen();

                    if (!_haveMasque)
                    {
                        // checker l'event masque
                        ManagerDay.Instance.ChekEvent(0);
                    }
                }
            }
        }

        private void OnMouseExit()
        {
            _outline.enabled = false;
        }
    }
}
