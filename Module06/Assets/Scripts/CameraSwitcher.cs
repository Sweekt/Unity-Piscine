using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    [Header("Input System")]
    public InputActionReference switchAction;

    [Header("Cameras")]
    public GameObject tpsCamera;
    public GameObject fpsCamera;
    [Header("Player")]
    public PlayerController playerController;
    private bool _isFpsMode = false;

    private void OnEnable() => switchAction.action.Enable();
    private void OnDisable() => switchAction.action.Disable();

    void Start() {
        UpdateCameraState();
    }

    void Update() {
        if (switchAction.action.WasPressedThisFrame()) {
            _isFpsMode = !_isFpsMode;
            UpdateCameraState();
        }
    }

    void UpdateCameraState() {
        tpsCamera.SetActive(!_isFpsMode);
        fpsCamera.SetActive(_isFpsMode);

        if(playerController != null)
            playerController.SetFpsMode(_isFpsMode);
    }
}