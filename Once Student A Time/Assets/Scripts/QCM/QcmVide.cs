using Script.Menu;

namespace QCM
{
    public class QcmVide : Qcm
    {
        // ------------ Méthods ------------
        
        public override void OpenQcm()
        {
            MenuManager.Instance.OpenMenu(GetComponent<Menu>());
        }

        public override void Reset()
        {}
    }
}