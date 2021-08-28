using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class TriggerCourses : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ManagerDay.Instance.Day == 0)
            {
                ManagerDay.Instance.ChekEvent(2);
            }
        }
    }
}
