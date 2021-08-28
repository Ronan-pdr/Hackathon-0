using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class Dormir : MonoBehaviour
{
    public bool HasSlept;

    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }
    
    private void OnMouseEnter()
    {
        _outline.enabled = true;
    }

    private void OnMouseExit()
    {
        _outline.enabled = false;
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            HasSlept = true;
            ManagerDay.Instance.EndOfDay();
        }
    }
}
