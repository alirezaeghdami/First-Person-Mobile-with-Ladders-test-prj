using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerTouchRotation : MonoBehaviour
{
    private Finger MovementFinger;
    private Vector2 firstTouch;
    private Quaternion RotationAmount;
    private float xRotation = 0;

    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private float sensitivity;

    [SerializeField]
    private float upRotation, downRotation;

    private PlayerTouchMovement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerTouchMovement>();
    }

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

    private void HandleFingerMove(Finger MovedFinger)
    {
        if (MovedFinger == MovementFinger)
        {
            ETouch.Touch currentTouch = MovedFinger.currentTouch;

            Vector3 dis = currentTouch.screenPosition - firstTouch;
            RotationAmount = Quaternion.Euler(-dis.y, dis.x, 0);
        }
    }

    private void HandleLoseFinger(Finger LostFinger)
    {
        if (LostFinger == MovementFinger)
        {
            MovementFinger = null;
            RotationAmount = Quaternion.identity;
        }
    }

    private void HandleFingerDown(Finger TouchedFinger)
    {
        if (MovementFinger == null && TouchedFinger.screenPosition.x > Screen.width / 2f)
        {
            MovementFinger = TouchedFinger;
            RotationAmount = Quaternion.identity;
            firstTouch = TouchedFinger.screenPosition;
        }
    }

    private void Update()
    {
        if (!playerMovement.isClimbing)
        {
            playerTransform.Rotate(Vector3.up * RotationAmount.y * sensitivity * Time.deltaTime);

            xRotation += RotationAmount.x;
            xRotation = Mathf.Clamp(xRotation, downRotation, upRotation);

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }
    }
}
