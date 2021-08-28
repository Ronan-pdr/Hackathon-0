using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class Basket : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 3ème jour
            if (ManagerDay.Instance.Day == 2)
            {
                ManagerDay.Instance.ChekEvent(3);
            }
        }
    }
}
