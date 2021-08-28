using System;
using System.Collections;
using System.Collections.Generic;
using Script.Menu;
using TMPro;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
   public static bool OnComputer;
   private static bool PasswordEntered;
   public string password;
   private Outline outline;

   [Header("On Computer")] 
   private BasicMovement _basicMovement;
   private Camera _camera;
   [SerializeField] private Texture2D CursorTexture;
   
   private void Awake()
   {
      _camera = Camera.main;
      outline = GetComponentInChildren<Outline>();
   }
   
   private void OnMouseEnter()
   {
      outline.enabled = true;
   }

   private void OnMouseOver()
   {
      if (Input.GetKeyDown(KeyCode.E) && CompareTag("Computer") && !OnComputer && !OpenTelAndPorteFeuille.LookingPocket)
      {
         _basicMovement = FindObjectOfType<BasicMovement>();
         _basicMovement.enabled = false;
         
         if (PasswordEntered)
            MenuManager.Instance.OpenMenu("Bureau");
         else
            MenuManager.Instance.OpenMenu("Password");
         
         Cursor.visible = true;
         Cursor.lockState = CursorLockMode.None;
         //Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);
         OnComputer = true;
         _camera.transform.position = new Vector3(3.05f, 1.553039f, -3798.35f);
         _camera.transform.rotation = Quaternion.Euler(0, 90, 0);
      }
   }

   private void OnMouseExit()
   {
      outline.enabled = false;
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Escape) && OnComputer)
      {
         MenuManager.Instance.OpenMenu("Cursor");
         _basicMovement.enabled = true;
         Cursor.lockState = CursorLockMode.Locked;
         Cursor.visible = false;
         //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
         OnComputer = false;
         _camera.transform.localPosition = new Vector3(0, 0.5f, 0);
      }
   }

   public void CheckPassword(TMP_InputField inputField)
   {
      if (inputField.text == password)
      {
         PasswordEntered = true;
         MenuManager.Instance.OpenMenu("Bureau");
      }
      else
         inputField.text = "";
   }
}
