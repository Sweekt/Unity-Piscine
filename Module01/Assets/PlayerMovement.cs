using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 2f;
    public float gravity = -9.81f;
    public float jumpHeight = 0.5f;
    public Transform groundCheck;
    public float groundDistance = 0.05f;
    public LayerMask groundMask;
    public GameObject Exit;
    Vector3 velocity;
    bool isGrounded;
    Vector3 defaultPosition;
    bool isActive = false;
    bool isOnExit;

    void Start () {
        defaultPosition = transform.position;
    }

    void Update() {
        if (isActive) {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            // Moving
            if (isGrounded && velocity.y < 0)
                velocity.y = -2f;
            float x = 0;
            if (Keyboard.current != null) {
               if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) x = 1;
                if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) x = -1;
            }
            Vector3 move = transform.right * x;
            controller.Move(move * speed * Time.deltaTime);
            // Jumping
            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        // Checking Exit
        float distance = Vector3.Distance(Exit.transform.position, transform.position);
        if(distance < 0.03f)
            isOnExit = true;
        else 
            isOnExit = false;
    }

    public void Reset() {
        controller.enabled = false; 
        transform.position = defaultPosition;
        controller.enabled = true;
    }

    public void SetActive (bool state) {
        isActive = state;
    }

    public Vector3 GetCurrentPosition () {
        return transform.position;
    }
    public bool exitState () {
        return (isOnExit);
    }
}