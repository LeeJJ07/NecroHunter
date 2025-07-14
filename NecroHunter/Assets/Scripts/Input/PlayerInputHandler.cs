using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private NavMeshAgent player;

    private Finger movementFinger;
    private Vector2 movementAmount;

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

        movementAmount = knobPosition / maxMovement;
        Debug.Log(movementAmount);

    }
    private void HandleLoseFinger(Finger lostFinger)
    {
        if (lostFinger != movementFinger)
            return;

        movementFinger = null;
        movementAmount = Vector2.zero;
        startFingerPosition = Vector2.zero;
    }
    private void HandleFingerDown(Finger touchedFinger)
    {
        if (movementFinger != null)
            return;

        movementFinger = touchedFinger;
        movementAmount = Vector2.zero;
        startFingerPosition = touchedFinger.screenPosition;
    }

    private void Update()
    {
        Vector3 scaledMovement = player.speed * Time.deltaTime * new Vector3(
            movementAmount.x,
            0.0f,
            movementAmount.y
        );

        player.Move(scaledMovement);
        player.transform.LookAt(player.transform.position + scaledMovement, Vector3.up);
    }
}
