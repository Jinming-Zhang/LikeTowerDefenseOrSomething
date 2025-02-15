using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Camera Movement Config")]
    [SerializeField] private Vector2 _MoveSpeed = new Vector2(10.0f, 10.0f);

    [SerializeField] private Vector2 _LookSpeed = new Vector2(10.0f, 10.0f);

    [Header("Input References")]
    [SerializeField] private PlayerInput _PlayerInput;

    [SerializeField] private InputActionReference _MoveInputAction;
    [SerializeField] private InputActionReference _LookInputAction;

    private Vector2 _MoveInput;
    private Vector2 _LookInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-_MoveInput.x, 0, -_MoveInput.y) *
                              Time.deltaTime;
        transform.eulerAngles +=
            new Vector3(_LookInput.x, 0, _LookInput.y) * Time.deltaTime;
    }

    private void OnEnable()
    {
        _MoveInputAction.action.started += OnMoveInputStarted;
        _MoveInputAction.action.canceled += OnMoveInputEnd;
        _LookInputAction.action.performed += OnLookInputStarted;
        _LookInputAction.action.performed += OnLookInputEnd;
    }

    private void OnDisable()
    {
        _MoveInputAction.action.performed -= OnMoveInputStarted;
        _MoveInputAction.action.canceled -= OnMoveInputEnd;
        _LookInputAction.action.performed -= OnLookInputStarted;
        _LookInputAction.action.performed -= OnLookInputEnd;
    }

    void OnMoveInputStarted(InputAction.CallbackContext context)
    {
        _MoveInput = context.ReadValue<Vector2>() * _MoveSpeed;
    }

    void OnMoveInputEnd(InputAction.CallbackContext context)
    {
        _MoveInput = Vector2.zero;
    }

    void OnLookInputStarted(InputAction.CallbackContext context)
    {
        _LookInput = context.ReadValue<Vector2>() * _LookSpeed;
    }

    void OnLookInputEnd(InputAction.CallbackContext context)
    {
        _LookInput = Vector2.zero;
    }
}