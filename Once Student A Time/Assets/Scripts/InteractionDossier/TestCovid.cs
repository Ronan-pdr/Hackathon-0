using System.Collections;
using System.Collections.Generic;
using Manager;
using Mission;
using UnityEngine;

public class TestCovid : MonoBehaviour
{
    // -------------- Attributs --------------

    [Header("Event")]
    [SerializeField] private OurEvent test;
        
    // -------------- Event --------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ManagerDay.Instance.Day == 2)
        {
            StartCoroutine(Message());
                
            test.Happen();
        }
    }
    
    // ---------- Coroutine ----------

    private IEnumerator Message()
    {
        ManagerDay.Instance.SetPensée("Vous vous êtes fait tester. \n Vous êtes négatif.");
        yield return new WaitForSeconds(2); // Attend 2s
        ManagerDay.Instance.SetPensée("");
    }
}
