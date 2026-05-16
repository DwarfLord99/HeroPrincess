using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCameraController : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float zoomLerpSpeed = 10f;
    [SerializeField] private float minZoomDistance = 3f;
    [SerializeField] private float maxZoomDistance = 15f;

    private PlayerInputActions inputActions;

    private CinemachineCamera cam;
    private CinemachineOrbitalFollow orbitalFollow;
    private Vector2 scrollDelta;

    private float targetZoomDistance;
    private float currentZoomDistance;


    void Start()
    {
        inputActions = new PlayerInputActions();
        inputActions.Enable();
        inputActions.CameraControls.MouseZoom.performed += HandleMouseZoom;

        Cursor.lockState = CursorLockMode.Locked;

        cam = GetComponent<CinemachineCamera>();
        orbitalFollow = cam.GetComponent<CinemachineOrbitalFollow>();

        targetZoomDistance = currentZoomDistance = orbitalFollow.Radius;
    }

    private void HandleMouseZoom(InputAction.CallbackContext context)
    {
        scrollDelta = context.ReadValue<Vector2>();
    }

    void Update()
    {
        if (scrollDelta.y != 0)
        {
            if (orbitalFollow != null)
            {
                targetZoomDistance = Mathf.Clamp(orbitalFollow.Radius - scrollDelta.y * zoomSpeed, minZoomDistance, maxZoomDistance);
                scrollDelta = Vector2.zero; // Reset scroll delta after processing
            }
        }

        float bumperDelta = inputActions.CameraControls.GamePadZoom.ReadValue<float>();
        if (bumperDelta != 0)
        {
            targetZoomDistance = Mathf.Clamp(orbitalFollow.Radius - bumperDelta * zoomSpeed, minZoomDistance, maxZoomDistance);
        }

        currentZoomDistance = Mathf.Lerp(currentZoomDistance, targetZoomDistance, Time.deltaTime * zoomLerpSpeed);
        orbitalFollow.Radius = currentZoomDistance;
    }
}
