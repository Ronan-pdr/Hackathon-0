using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class Sucres : MonoBehaviour
{
    private Outline _outline;

    private void Awake()
    {
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
            FindObjectOfType<ManagerDay>().ChekEvent(5);
            StartCoroutine(Green());
        }
    }

    IEnumerator Green()
    {
        _outline.OutlineColor = Color.green;
        yield return new WaitForSeconds(0.25f);
        _outline.OutlineColor = Color.white;
    }

    private void OnMouseExit()
    {
        _outline.enabled = false;
    }
}
