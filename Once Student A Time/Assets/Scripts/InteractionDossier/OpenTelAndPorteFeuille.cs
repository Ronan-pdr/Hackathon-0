using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using Script.Menu;
using UnityEngine;

public class OpenTelAndPorteFeuille : MonoBehaviour
{
    public static bool LookingPocket;
    public static OpenTelAndPorteFeuille Instance;
    public bool TelGotten;
    public bool PorteFeuilleGotten;

    public GameObject Tel;
    public GameObject Portefeuille;
    public BasicMovement BasicMovement;

    public GameObject telButton;
    public GameObject porteFeuilleButton;

    public Menu[] telMenus;
    
    private void Awake()
    {
        BasicMovement = FindObjectOfType<BasicMovement>();
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !InteractionHandler.OnComputer)
        {
            if (BasicMovement.enabled)
            {
                LookingPocket = true;
                BasicMovement.enabled = false;
                if (TelGotten)
                    telButton.SetActive(true);

                if (PorteFeuilleGotten)
                    porteFeuilleButton.SetActive(true);
                Debug.Log(PorteFeuilleGotten);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                LookingPocket = false;
                BasicMovement.enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                telButton.SetActive(false);
                porteFeuilleButton.SetActive(false);
                Tel.SetActive(false);
                Portefeuille.SetActive(false);
                MenuManager.Instance.OpenMenu("Cursor");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !BasicMovement.enabled)
        {
            BasicMovement.enabled = true;
            Tel.SetActive(false);
            Portefeuille.SetActive(false);
            telButton.SetActive(false);
            porteFeuilleButton.SetActive(false);
            LookingPocket = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            MenuManager.Instance.OpenMenu("Cursor");
        }
    }

    public void Reset()
    {
        TelGotten = false;
        PorteFeuilleGotten = false;
        
        telButton.SetActive(false);
        porteFeuilleButton.SetActive(false);
    }

    public void SetActiveRightMenu()
    {
        MenuManager.Instance.OpenMenu(telMenus[ManagerDay.Instance.Day]);
    }
}
