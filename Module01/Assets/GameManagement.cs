using UnityEngine;
using UnityEngine.InputSystem;

public class GameManagement : MonoBehaviour
{
    public PlayerMovement Claire;
    public PlayerMovement Thomas;
    public PlayerMovement John;
    public PlayerMovement activePlayer;
    public Camera mainCamera; 

    void Start() {
        activePlayer = Claire;
        if (Claire != null) {
            Claire.SetActive(true);
            Thomas.SetActive(false);
            John.SetActive(false);
        }
        Vector3 initCam;
        initCam.x = 0f;
        initCam.y = 0.5f;
        initCam.z = -2.5f;
        mainCamera.transform.position = initCam;
    }

    void Update() {
        if (Keyboard.current != null) {
            if (Keyboard.current.digit1Key.wasPressedThisFrame) ChangeCharacter(Claire);
            else if (Keyboard.current.digit2Key.wasPressedThisFrame) ChangeCharacter(Thomas);
            else if (Keyboard.current.digit3Key.wasPressedThisFrame) ChangeCharacter(John);
            if (Keyboard.current.rKey.wasPressedThisFrame || Keyboard.current.backspaceKey.wasPressedThisFrame) {
                Claire.Reset();
                John.Reset();
                Thomas.Reset();
            }
        }
        if (activePlayer != null) {
            Vector3 targetPos = activePlayer.GetCurrentPosition();
            Vector3 camPos = mainCamera.transform.position;
            camPos.x = targetPos.x;
            camPos.y = targetPos.y;
            mainCamera.transform.position = camPos;
        }
    
    }

    void ChangeCharacter(PlayerMovement newCharacter) {
        if(activePlayer != null)
            activePlayer.SetActive(false);
        activePlayer = newCharacter;
        activePlayer.SetActive(true);
    }
}
