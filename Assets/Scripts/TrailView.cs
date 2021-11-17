using System;
using JoostenProductions;
using UnityEngine;

public class TrailView : MonoBehaviour
{
    private RectTransform _rectTransform;
    
    public void Init()
    {
        _rectTransform = GetComponent<RectTransform>();

        UpdateManager.SubscribeToUpdate(OnUpdate);
    }

    private void OnUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            _rectTransform.anchoredPosition = touch.position;
        }
        
        // Trail почему-то смещается. никак не могу понять что нужно учесть в расчете позиции.
        // Может быть влияют параметры разметки родительского трансформа?

        // _rectTransform.anchoredPosition = Input.mousePosition;
    }
}