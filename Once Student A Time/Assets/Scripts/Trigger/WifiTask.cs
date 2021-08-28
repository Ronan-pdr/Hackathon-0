using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using Mission;
using Script.Menu;
using UnityEngine;

public class WifiTask : MonoBehaviour
{
    [SerializeField] private Menu teams;
    [SerializeField] private Menu[] pasCours;
    [SerializeField] private Menu problemeCo;
    [SerializeField] private Menu cours;
    
    [Header("EventTeams")]
    [SerializeField] private GameObject[] barreWifi;
    [SerializeField] private OurEvent coursCheck;
    [SerializeField] private Transform borneWifi;

    private void Awake()
    {
        if (barreWifi.Length != 3)
        {
            throw new Exception();
        }
    }
    
    public void OnTeams()
    {
        MenuManager.Instance.OpenMenu(teams);
        float dist = Vector3.Distance(borneWifi.position, transform.position);
        
        if (!coursCheck.gameObject.activeSelf)
        {
            Debug.Log("Wifi not activeself");
            // il n'y même pas cours
            MenuManager.Instance.ForceOpen(pasCours[ManagerDay.Instance.Day]);
        }
        else if (dist < 10)
        {
            Debug.Log("Near borne");
            // il a la wifi
            MenuManager.Instance.ForceOpen(cours);

            if (ManagerDay.Instance.Day == 3)
            {
                Debug.Log("Wifi");
                coursCheck.Happen();
            }
        }
        else
        {
            Debug.Log("lqegnrlnlqegrngqre");
            // il a y pas la wifi
            MenuManager.Instance.ForceOpen(problemeCo);

            if (dist < 10)
            {
                SetBarre(0);
            }
            else if (dist < 30)
            {
                SetBarre(1);
            }
            else
            {
                SetBarre(2);
            }
            
            void SetBarre(int index)
            {
                for (int i = 0; i < 3; i++)
                {
                    barreWifi[i].SetActive(i == index);
                }
            }
        }
    }

}
