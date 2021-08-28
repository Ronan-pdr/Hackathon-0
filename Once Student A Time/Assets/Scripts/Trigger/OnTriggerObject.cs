using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnTriggerObject : MonoBehaviour
{
    public bool NeedToBeSeen;
    private Renderer _renderer;
    private bool Declenched;
    public string pensée;
    public float time;
    [SerializeField] private TextMeshProUGUI penséeText;
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (_renderer.isVisible && !Declenched && NeedToBeSeen)
        {
           DéclenchePensée();
        }
    }

    public void DéclenchePensée()
    {
        Declenched = true;
        StartCoroutine(TypeText());
        
    }

    private IEnumerator TypeText()
    {
        foreach (var letter in pensée)
        {
            penséeText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        Invoke(nameof(CancelText), time);
    }

    private void CancelText()
    {
        penséeText.text = "";
    }
}
