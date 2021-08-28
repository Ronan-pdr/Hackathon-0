using System;
using System.Collections.Generic;
using Interaction;
using Langage;
using Mission;
using Movements;
using QCM;
using Script.Menu;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Manager
{
    public class ManagerDay : MonoBehaviour
    {
        // ------------ SerializeField ------------
        
        [Header("Manager")]
        [SerializeField] private ManageEvent[] manageEvents;
        [SerializeField] private GameObject[] phones;

        [Header("Message")] 
        [SerializeField] private TextMeshProUGUI pensée;
        
        [Header("Date/Planning")]
        [SerializeField] private Material[] planningList;
        [SerializeField] private MeshRenderer planning;
        
        [Header("Toile de fin de jounée")]
        [SerializeField] private GameObject sceneEndDay;
        [SerializeField] private TextMeshProUGUI[] textEndDay;
        [SerializeField] private Button nextDay;
        
        [Header("Fin de jeu")]
        [SerializeField] private Menu endGame;
        
        [Header("Prefab")]
        [SerializeField] private GameObject canettePrefab;
        [SerializeField] private GameObject telephonePrefab;
        [SerializeField] private GameObject porteFeuillePrefab;
        [SerializeField] private GameObject masquePrefab;
        
        [Header("Décor à activer")]
        [SerializeField] private GameObject kevin;
        [SerializeField] private GameObject vaisselle;
        [SerializeField] private Rideau rideau;

        // ------------ Attributs ------------
        
        public static ManagerDay Instance;
        public int Day { get; private set; }

        private List<GameObject> _objects;

        private GameObject _kevin;

        // ------------ Getter ------------

        public ExoAlgo GetExo()
        {
            return manageEvents[Day].GetComponent<ExoAlgo>();
        }

        // ------------ Setter ------------

        private void SetFalseScene()
        {
            nextDay.gameObject.SetActive(false);
            sceneEndDay.gameObject.SetActive(false);
        }

        private void MyReset()
        {
            OpenTelAndPorteFeuille.Instance.Reset();
            Interpreteur.Instance.Reset();
            TeleportScript.Instance.MyReset();

            SetFalseScene();
        }
        
        private void SetPlanning(bool etat)
        {
            manageEvents[Day].gameObject.SetActive(etat);
            if (etat)
            {
                planning.material = planningList[Day];
            }
        }

        public void SetPensée(string message) => pensée.text = message;
        
        
        // ------------ Open ------------
        
        public void OpenQcm()
        {
            manageEvents[Day].Qcm.OpenQcm();
        }

        private void OpenTelephone()
        {
            if (Day > 0)
            {
                phones[Day - 1].SetActive(false);
            }
            
            phones[Day].SetActive(true);
        }
        
        // ------------ Constructor ------------
        
        private void Start()
        {
            Instance = this;
            Day = 0;
            _objects = new List<GameObject>();
            SetFalseScene();
            Verification();
            OpenTelephone();
            
            foreach (ManageEvent manageEvent in manageEvents)
            {
                manageEvent.gameObject.SetActive(false);
            }
            
            SetPlanning(true);
            ManageObjet();
        }
        
        private void Verification()
        {
            // Les menus / dossiers
            Aux(manageEvents);
            Aux(phones);

            void Aux<T>(T[] array)
            {
                if (array.Length != 4)
                    throw new Exception();
            }
        }
        
        // ------------ Update ------------
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                NewDay();
            }
        }
        
        // ------------ Public Méthode ------------
        
        public void NewDay()
        {
            if (Day == 3)
            {
                // fin de jeu
                MenuManager.Instance.OpenMenu(endGame);
                FindObjectOfType<BasicMovement>().enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                return;
            }
            
            // reset
            MyReset();
            SetPlanning(false);

            Day += 1;
            
            // set pour le nouveau jour
            OpenTelephone();
            ManageObjet();
            SetPlanning(true);
            kevin.SetActive(Day == 2);
            vaisselle.SetActive(Day == 3);

            // deplacement
            FindObjectOfType<BasicMovement>().enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void ChekEvent(int i)
        {
            Debug.Log($"Event : {manageEvents[Day].name}");
            manageEvents[Day].ChekEvent(i);
        }
        
        public void EndOfDay()
        {
            sceneEndDay.SetActive(true);
            manageEvents[Day].PrintResult(textEndDay);

            if (manageEvents[Day].GetNoteResult() >= 0)
            {
                nextDay.gameObject.SetActive(true);
            }
            
            // deplacement
            FindObjectOfType<BasicMovement>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
        public void Restart()
        {
            MyReset();
            manageEvents[Day].ResetDay();
            ManageObjet();

            // deplacement
            FindObjectOfType<BasicMovement>().enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        // ------------ Private Methodes ------------

        private void ManageObjet()
        {
            rideau.Close();
            
            // détruire tous les objets de la journée précédente
            foreach (GameObject gameObj in _objects)
            {
                Destroy(gameObj);
            }
            
            _objects = new List<GameObject>();
            
            // placer les objets
            Placer(canettePrefab, manageEvents[Day].TrCannette);
            Placer(telephonePrefab, manageEvents[Day].TrTelephone);
            Placer(porteFeuillePrefab, manageEvents[Day].TrPorteFeuille);
            Placer(masquePrefab, manageEvents[Day].TrMasque);
        }
        
        private void Placer(GameObject obj, Transform tr)
        {
            _objects.Add(Instantiate(obj, tr.position, tr.rotation));
        }
    }
}