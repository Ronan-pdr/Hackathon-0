using System;
using Mission;
using Script.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
namespace QCM
{
    public class QcmComplet : Qcm
    {
        // ------------ SerializeField ------------
        
        [Header("For end page")]
        [SerializeField] private TextMeshProUGUI textResult;
        [SerializeField] private TextMeshProUGUI textButton;
        [SerializeField] private Menu bureau;

        [Header("Pages")]
        [SerializeField] private Page[] pages;
        
        [Header("Event")]
        [SerializeField] private OurEvent qcm;
        
        // ------------ Attributs ------------
        
        private int _index;
        private int _note;
        
        // ------------ Setter ------------
        
        private void SetPage(bool etat)
        {
            pages[_index].gameObject.SetActive(etat);
        }
        
        public override void Reset()
        {
            _index = 0;
            _note = 0;

            foreach (Page page in pages)
            {
                if (page is PageQuestion)
                {
                    ((PageQuestion)page).Reset();
                }
            }
        }
        
        // ------------ Constructor ------------
        
        private void Awake()
        {
            Reset();
        }
        
        // ------------ Activation ------------
        
        public override void OpenQcm()
        {
            // ouvrir le menu qcm
            MenuManager.Instance.OpenMenu(GetComponent<Menu>());
            
            // cacher toutes les pages
            foreach (Page page in pages)
            {
                page.gameObject.SetActive(false);
            }
            
            SetPage(true);
            textButton.text = "Suivant";
        }
        
        // ------------ Méthods ------------
        
        public void NextPage()
        {
            // update la note
            if (pages[_index] is PageQuestion)
            {
                _note += ((PageQuestion)pages[_index]).Result();
            }
            
            // l'ancienne page est enlevé
            SetPage(false);

            if (_index + 1 == pages.Length)
            {
                // fin --> retour sur le menu
                MenuManager.Instance.OpenMenu(bureau);
            }
            else
            {
                // next page
                _index += 1;
                SetPage(true);

                if (_index + 1 == pages.Length)
                {
                    // set la page de résultat
                    
                    string mes = $"Votre note est {_note} / {(pages.Length - 2) * 2}" + Environment.NewLine;
                    if (_note > -2)
                    {
                        qcm.Happen();
                        mes += "Vous avez réussi !";
                    }
                    else
                    {
                        mes += "Vous avez échoué !";
                    }
                
                    textResult.text = mes;
                    textButton.text = "Retour";
                }
            }
        }
    }
}
