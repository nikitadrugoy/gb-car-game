using JoostenProductions;
using Tools;
using UnityEngine;

public class SwipeInputView : BaseInputView
{
    private const float AccelerationAmount = 0.05f;
    private const float BreakAmount = 0.1f;
    private const float BreakTimeout = 1f;
    private const float Threshold = 0.1f;

    private Vector2 _touchStartPosition;
    private float _timeFromBreak;

    public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
    {
        base.Init(leftMove, rightMove, speed);

        UpdateManager.SubscribeToUpdate(OnUpdate);
    }

    private void OnUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _touchStartPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    Speed += SwipeToAcceleration(_touchStartPosition, touch.position);
                    break;
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

    private float SwipeToAcceleration(Vector2 from, Vector2 to)
    {
        if (Mathf.Abs(from.x - to.x) <= Threshold)
        {
            return 0f;
        }
        
        return from.x > to.x 
            ? AccelerationAmount 
            : 0 - AccelerationAmount;
    }
}