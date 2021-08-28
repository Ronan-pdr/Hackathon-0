using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    [Serializable]
    public struct DialogueText
    {
        public int Time;
        public string content;
        public string Character;
    }
    
    public List<DialogueText> list = new List<DialogueText>();
    [Header("Taylor")]
    public TextMeshProUGUI GP;
    
    [Header("Victoria")]
    public TextMeshProUGUI Victoria;


    private void Awake()
    {
        StartCoroutine(dialogueCoroutine());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public IEnumerator dialogueCoroutine()
    {
        foreach (var dialogue in list)
        {
            if (dialogue.Character == "G")
            {
                Victoria.text = "";
                GP.text = "";
                foreach (var chr in dialogue.content)
                {
                    GP.text += chr;
                    yield return new WaitForSeconds(0.02f);
                }

            }
            else
            {
                Victoria.text = "";
                GP.text = ""; 
                foreach (var chr in dialogue.content)
                {
                    Victoria.text += chr;
                    yield return new WaitForSeconds(0.02f);
                }
            }

            yield return new WaitForSeconds(dialogue.Time);
        } 
        GP.text = "";
        Victoria.text = "";
    }
}
