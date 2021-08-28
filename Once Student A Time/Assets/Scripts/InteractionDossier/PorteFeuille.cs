using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteFeuille : MonoBehaviour
{
    private Outline outline;

    public GameObject tips;
    // Start is called before the first frame update
    void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnMouseEnter()
    {
        outline.enabled = true;
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (tips != null)
            {
                tips.SetActive(true);
                Destroy(tips, 3);
            }

            OpenTelAndPorteFeuille.Instance.PorteFeuilleGotten = true;
            Destroy(gameObject);
        }
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }
}
