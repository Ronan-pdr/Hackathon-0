using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseGameObject : MonoBehaviour
{
   public void OpenAndClose(GameObject _gameObject) => _gameObject.SetActive(!_gameObject.activeSelf);
}
