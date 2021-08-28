using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calendrier : MonoBehaviour
{
    private bool Seen;
    private void OnMouseEnter()
    {
        if (!Seen)
        {
            Seen = true;
            GetComponent<OnTriggerObject>().DéclenchePensée();
        }
    }
}
