using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject UI;
    private bool IsPaused = false;
    private BasicMovement _basicMovement;

    private void Awake()
    {
        _basicMovement = FindObjectOfType<BasicMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            if (IsPaused)
            {
                UI.SetActive(false);
                IsPaused = false;
                _basicMovement.enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                UI.SetActive(true);
                IsPaused = true;
                _basicMovement.enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void Resume()
    {
        UI.SetActive(false);
        IsPaused = false;
        _basicMovement.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Quit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu 2");
    }
}
