using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivreTE : MonoBehaviour
{
    private Outline _outline;
    private BasicMovement _movement;
    private bool IsReading;
    [SerializeField] private GameObject bookTe;
    
    private void Awake()
    {
        _movement = FindObjectOfType<BasicMovement>();
        _outline = GetComponent<Outline>();
    }

    private void OnMouseEnter()
    {
        _outline.enabled = true;
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (IsReading)
            {
                bookTe.SetActive(false);
                _movement.enabled = true;
                IsReading = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                bookTe.SetActive(true);
                _movement.enabled = false;
                IsReading = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && IsReading)
        {
            bookTe.SetActive(false);
            _movement.enabled = true;
            IsReading = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnMouseExit()
    {
        _outline.enabled = false;
    }
}
