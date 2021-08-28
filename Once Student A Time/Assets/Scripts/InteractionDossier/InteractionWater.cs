using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWater : MonoBehaviour
{
    private Outline _outline;
    public bool IsRigging;
    [SerializeField] private ParticleSystem particle;
    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    public void StartRigging()
    {
        particle.Play();
        IsRigging = true;
    }

    private void OnMouseEnter()
    {
        _outline.enabled = true;
    }


    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsRigging)
        {
            // On a arrêté l'eau
            IsRigging = false;
        }
    }

    private void OnMouseExit()
    {
        _outline.enabled = false;
    }
}
