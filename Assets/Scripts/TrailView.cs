using System;
using JoostenProductions;
using UnityEngine;

public class TrailView : MonoBehaviour
{
    private Camera _camera;
    
    public void Init()
    {
        _camera = Camera.main;
        
        // Не совсем понимаю зачем нужен UpdateManager. Но для целостности тут тоже его использую.
        UpdateManager.SubscribeToUpdate(UpdatePosition);
    }

    private void UpdatePosition()
    {
        Vector3 position = Input.mousePosition;
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            position = touch.position;
        }

        position = _camera.ScreenToWorldPoint(position);
        position.z = 0;

        transform.position = position;
    }

    private void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(UpdatePosition);
    }
}