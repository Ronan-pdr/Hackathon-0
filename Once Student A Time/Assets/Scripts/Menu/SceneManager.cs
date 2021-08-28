using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{

    public void LoadScene(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
    
    public void LoadScene(int n)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(n);
    }

    public void CloseApp()
    {
        Application.Quit();
    }
}
