using JoostenProductions;
using Tools;
using UnityEngine;

public class TapInputView : BaseInputView
{
    private const float AccelerationAmount = 0.05f;
    private const float BreakAmount = 0.1f;
    private const float BreakTimeout = 1f;
    
    private float _timeFromBreak;
    private int _screenCenterX;

    public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
    {
        base.Init(leftMove, rightMove, speed);
        
        _screenCenterX = Screen.width / 2;
        
        UpdateManager.SubscribeToUpdate(OnUpdate);
    }

    private void OnUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Speed = touch.position.x >= _screenCenterX 
                    ? Speed + AccelerationAmount 
                    : Speed - AccelerationAmount;
            }
        }

        _timeFromBreak += Time.deltaTime;

        if (_timeFromBreak >= BreakTimeout)
        {
            Break();
            
            _timeFromBreak -= BreakTimeout;
        }

        Move();
    }

    private void Move()
    {
        Debug.Log(Speed);

        if (Speed > 0)
        {
            OnRightMove(Speed);
        }
        else
        {
            OnLeftMove(Speed);
        }
    }

    private void Break()
    {
        if (Speed > 0)
        {
            Speed -= BreakAmount;

            if (Speed < 0)
                Speed = 0;
        }
        else
        {
            Speed += BreakAmount;

            if (Speed > 0)
                Speed = 0;
        }
    }

    private void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(OnUpdate);
    }
}