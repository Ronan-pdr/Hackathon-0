using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FridegAnimation : MonoBehaviour
{
   private bool HaveBeenOpen;
   [SerializeField] private Animator animator;
   [SerializeField] private Outline outline;
   private static readonly int IsOpen = Animator.StringToHash("IsOpen");
   private bool IsOpenBool;
   [SerializeField] private TextMeshProUGUI penséeText;
   public float time;
   private void Awake()
   {
      animator = GetComponent<Animator>();
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
         if (!HaveBeenOpen)
         {
            StartCoroutine(TypeText("Mince... Faut vraiment que j'aille faire des courses"));
            HaveBeenOpen = true;
         }
         IsOpenBool = !IsOpenBool;
         animator.SetBool(IsOpen, IsOpenBool);
      }
   }
   
   private IEnumerator TypeText(string pensée)
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

   private void OnMouseExit()
   {
      outline.enabled = false;
   }
}
