using System;
using JoostenProductions;
using UnityEngine;

public class TrailView : MonoBehaviour
{
    private Camera _camera;
    
    public void Init()
    {
        _camera = Camera.main;
        
        UpdateManager.SubscribeToUpdate(UpdatePosition);
    }

    private void UpdatePosition()
    {
        foreach (Touch touch in Input.touches)
        {
            Vector3 position = touch.position;
        
            position = _camera.ScreenToWorldPoint(position);
            position.z = 0;

            transform.position = position;
        }
    }

    private void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(UpdatePosition);
    }
}