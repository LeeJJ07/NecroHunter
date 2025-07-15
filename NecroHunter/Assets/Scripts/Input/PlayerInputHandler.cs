using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }

    private Finger movementFinger;

    private Vector2 startFingerPosition;
    private float maxMovement = 100.0f;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleLoseFinger;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleLoseFinger;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void HandleFingerMove(Finger movedFinger)
    {
        if (movedFinger != movementFinger)
            return;

        Vector2 knobPosition;
        ETouch.Touch currentTouch = movedFinger.currentTouch;

        if (Vector2.Distance(currentTouch.screenPosition,
            startFingerPosition) > maxMovement)
        {
            knobPosition = (
                currentTouch.screenPosition - startFingerPosition
                ).normalized * maxMovement;
        }
        else
        {
            knobPosition = currentTouch.screenPosition - startFingerPosition;
        }

        MovementInput = knobPosition / maxMovement;

    }
    private void HandleLoseFinger(Finger lostFinger)
    {
        if (lostFinger != movementFinger)
            return;

        MovementInput = Vector2.zero;
        movementFinger = null;
        startFingerPosition = Vector2.zero;
    }
    private void HandleFingerDown(Finger touchedFinger)
    {
        if (movementFinger != null)
            return;

        MovementInput = Vector2.zero;
        movementFinger = touchedFinger;
        startFingerPosition = touchedFinger.screenPosition;
    }
}
