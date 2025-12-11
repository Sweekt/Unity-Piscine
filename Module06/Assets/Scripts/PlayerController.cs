using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Input System")]
    public InputActionReference moveAction;
    public InputActionReference sneakAction;
    public InputActionReference lookAction;

    [Header("References")]
    public Transform fpsCameraTransform;
    public Renderer johnRenderer;

    [Header("Settings")]
    public float moveSpeed = 5f;
    public float sneakSpeed = 2f;
    public float rotationSpeed = 720f;
    public float mouseSensitivity = 100f;
    public float gravity = 9.81f;

    [Header("Game State")]
    public bool isFpsMode = false;

    private CharacterController _controller;
    private Animator _animator;
    private Vector3 _velocity;
    private Vector2 _inputVector;
    private Vector2 _lookInput;
    private float _xRotation = 0f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        
        UpdateCursorState();
    }

    private void OnEnable() {
        moveAction.action.Enable();
        if (sneakAction != null) sneakAction.action.Enable();
        if (lookAction != null) lookAction.action.Enable();
    }

    private void OnDisable() {
        moveAction.action.Disable();
        if (sneakAction != null) sneakAction.action.Disable();
        if (lookAction != null) lookAction.action.Disable();
    }

    void Update() {
        _inputVector = moveAction.action.ReadValue<Vector2>();
        _lookInput = lookAction.action.ReadValue<Vector2>();

        if (isFpsMode)
            MoveFPS();
        else
            MoveTPS();

        ApplyGravity();
    }

    public void SetFpsMode(bool active)
    {
        isFpsMode = active;
        UpdateCursorState();
        if (johnRenderer != null)
        {
            if (active)
                johnRenderer.shadowCastingMode = ShadowCastingMode.ShadowsOnly;

            else
                johnRenderer.shadowCastingMode = ShadowCastingMode.On;
        }
        if (!active && fpsCameraTransform != null) {
            fpsCameraTransform.localRotation = Quaternion.identity;
            _xRotation = 0f;
        }
    }

    void UpdateCursorState()
    {
        if (isFpsMode) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void MoveTPS() {
        Vector3 direction = new Vector3(_inputVector.x, 0f, _inputVector.y).normalized;
        bool isSneaking = sneakAction != null && sneakAction.action.IsPressed();
        float currentSpeed = isSneaking ? sneakSpeed : moveSpeed;
        if (direction.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            _controller.Move(direction * currentSpeed * Time.deltaTime);
        }
        UpdateAnimation(isSneaking);
    }

    void MoveFPS() {
        float mouseX = _lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = _lookInput.y * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);

        if (fpsCameraTransform != null)
        {
            fpsCameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        }
        float z = _inputVector.y; 
        float x = _inputVector.x; 
        bool isSneaking = sneakAction != null && sneakAction.action.IsPressed();
        float currentSpeed = isSneaking ? sneakSpeed : moveSpeed;
        Vector3 move = transform.right * x + transform.forward * z;

        if (move.magnitude > 0.1f)
        {
            _controller.Move(move * currentSpeed * Time.deltaTime);
        }
        UpdateAnimation(isSneaking);
    }

    void UpdateAnimation(bool isSneaking) {
        float animValue = _inputVector.magnitude;       
        if (isSneaking && animValue > 0) animValue *= .8f;
        _animator.SetFloat("Speed", animValue);
    }

    void ApplyGravity() {
        if (_controller.isGrounded && _velocity.y < 0) _velocity.y = -2f;
        _velocity.y -= gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}