using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionLivre : MonoBehaviour
{
    private Outline outline;
    public GameObject book;
    public GameObject book2;
    private bool BookOpen;
    private BasicMovement _movement;
    private void Awake()
    {
        _movement = FindObjectOfType<BasicMovement>();
        outline = GetComponent<Outline>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && BookOpen)
        {
            _movement.enabled = true;
            book.SetActive(false);
            book2.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnMouseEnter()
    {
        outline.enabled = true;
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!BookOpen)
            {
                BookOpen = true;
                _movement.enabled = false;
                book.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                BookOpen = false;
                _movement.enabled = true;
                book.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }
}
