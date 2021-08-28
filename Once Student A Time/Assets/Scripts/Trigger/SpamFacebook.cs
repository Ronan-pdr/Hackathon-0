using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class SpamFacebook : MonoBehaviour
{
    public GameObject prefab;
    public Transform VerticalLayout;

    public float time;

    private void Awake()
    {
        StartCoroutine(StartSpammingCoroutine());
    }

    public IEnumerator StartSpammingCoroutine()
    {
        float timer = 0;
        while (timer < time)
        {
            yield return new WaitForSeconds(0.5f);
            timer += 0.5f;
            Destroy(Instantiate(prefab, VerticalLayout), 2);
        }
    }
}
