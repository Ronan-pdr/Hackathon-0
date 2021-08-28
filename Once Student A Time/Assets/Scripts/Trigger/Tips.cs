using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips : MonoBehaviour
{
   public GameObject text;
   private void Awake()
   {
      Destroy(text, 3);
   }
}
