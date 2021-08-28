using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class DragObjects : MonoBehaviour
{
    private Vector3 _screenPoint;
    private Vector3 _offset;
    private Camera _camera;

    private void Awake() {
        _camera = Camera.main;
    }
		
    void OnMouseDown(){
        _screenPoint = _camera.WorldToScreenPoint(gameObject.transform.position);
        _offset = transform.position - _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
    }
		
    void OnMouseDrag(){
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
        Vector3 cursorPosition = _camera.ScreenToWorldPoint(cursorPoint) + _offset;
        transform.position = cursorPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CanTriggerJaune"))
        {
            if (ManagerDay.Instance.Day == 0)
            {
                FindObjectOfType<ManagerDay>().ChekEvent(3);
            }
        }
    }
}
