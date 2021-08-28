using Manager;
using UnityEngine;

namespace Interaction
{
    public class Rideau : InteractionDossier.Interaction
    {
        // ------------ Attibut ------------
    
        private bool _isOpen;
    
        // ------------ Constructeur ------------

        public override void Constructor()
        {
            Close();
        }
        
        // ------------ Methodes ------------

        private void Open()
        {
            _isOpen = true;
            transform.position = new Vector3(2.119232f, 0.3386898f, -3799.542f);
            transform.localScale = new Vector3(0.0105f, 0.0075f, 0.0075f);
        }

        public void Close()
        {
            _isOpen = false;
            transform.position = new Vector3(1.4186f, 0.3387f, -3799.542f);
            transform.localScale = new Vector3(0.0020601f, 0.0075f, 0.0075f);
        }

        // ------------ Event ------------

        public override void Touche()
        {
            if (_isOpen)
            {
                Close();
            }
            else
            {
                if (ManagerDay.Instance.Day == 1)
                {
                    ManagerDay.Instance.ChekEvent(4);
                }

                Open();
            }
        }
    }
}
