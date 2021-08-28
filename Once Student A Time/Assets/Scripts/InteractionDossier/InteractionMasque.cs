using System.Collections;
using Manager;
using Movements;
using UnityEngine;

namespace InteractionDossier
{
    public class InteractionMasque : Interaction
    {
        // ------------ Constructeur ------------

        public override void Constructor()
        {}
        
        // ------------ Event ------------

        public override void Touche()
        {
            TeleportScript.Instance.MaskTaken();
            StartCoroutine(Green());
        }
    
        IEnumerator Green()
        {
            _outline.OutlineColor = Color.green;
            yield return new WaitForSeconds(0.25f);
            _outline.OutlineColor = Color.white;
        }
    }
}
