using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(CinemachineCamera))]
public class PlayerCameraController : MonoBehaviour
{
    [Header("Camera Movement Config")]
    [SerializeField] private Vector2 _MoveSpeed = new Vector2(10.0f, 10.0f);

    [SerializeField] private float _ZoomSpeed = 5.0f;
    [SerializeField] private bool _InvertY = false;
    [SerializeField] [Range(1, 90)] private float _MaxFov;

    [Header("Input References")]
    [SerializeField] private InputActionReference _PanInputAction;

    [SerializeField] private InputActionReference _RotateStartAction;
    [SerializeField] private InputActionReference _RotateAction;
    [SerializeField] private InputActionReference _ZoomAction;

    private Vector2 _MoveInput;
    private CinemachineCamera _Camera;
    private bool _ShouldRotate;

    private void Awake()
    {
        _Camera = GetComponent<CinemachineCamera>();
    }

    void Update()
    {
        transform.position += new Vector3(-_MoveInput.x, 0, -_MoveInput.y) *
                              Time.deltaTime;
    }

    private void OnEnable()
    {
        _PanInputAction.action.performed += OnPan;

        _RotateStartAction.action.performed += OnRotateStart;
        _RotateStartAction.action.canceled += OnRotateEnd;

        _RotateAction.action.performed += OnRotate;
        _ZoomAction.action.performed += OnZoom;
    }

    private void OnDisable()
    {
        _PanInputAction.action.performed -= OnPan;

        _RotateStartAction.action.performed -= OnRotateStart;
        _RotateStartAction.action.canceled -= OnRotateEnd;
        _RotateAction.action.performed -= OnRotate;

        _ZoomAction.action.performed -= OnZoom;
    }

    void OnPan(InputAction.CallbackContext context)
    {
        _MoveInput = context.ReadValue<Vector2>() * _MoveSpeed;
    }

    void OnRotateStart(InputAction.CallbackContext context)
    {
        _ShouldRotate = true;
    }

    void OnRotateEnd(InputAction.CallbackContext context)
    {
        _ShouldRotate = false;
    }

    void OnRotate(InputAction.CallbackContext context)
    {
        if (!_ShouldRotate)
        {
            return;
        }

        Vector2 delta = context.ReadValue<Vector2>();
        Vector3 curRotation = transform.eulerAngles;
        curRotation.x += _InvertY ? delta.y : -delta.y;
        if (IsLookingUp(delta.y))
        {
            if (curRotation.x > 89 && curRotation.x < 270)
            {
                curRotation.x = 89;
            }
        }

        else
        {
            if (curRotation.x < 270 && curRotation.x > 90)
            {
                curRotation.x = 271;
            }
        }

        curRotation.y += delta.x;
        transform.eulerAngles = curRotation;
    }

    void OnZoom(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        float fov = Mathf.Clamp(_Camera.Lens.FieldOfView - value.y * _ZoomSpeed,
            1, _MaxFov);
        _Camera.Lens.FieldOfView = fov;
    }

    bool IsLookingUp(float deltaY)
    {
        return !_InvertY && deltaY < 0 || _InvertY && deltaY > 0;
    }
}