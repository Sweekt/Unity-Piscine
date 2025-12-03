using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public GameManagement Game;
    public GameObject Output;
    void Update() {
        // Rotating the ball
        Vector3 diagonalAxis = new Vector3(1f, 1f, 0f);
        transform.Rotate(diagonalAxis, rotationSpeed * Time.deltaTime);
        // Checking if player is on TP
        PlayerMovement ActivePlayer = Game.activePlayer;
        float distance = Vector3.Distance(ActivePlayer.transform.position, transform.position);
        if(distance < 0.05f) {
            Vector3 OutputPos = Output.transform.position;
            if (transform.position.x >= ActivePlayer.transform.position.x)
                OutputPos.x += 0.1f;
            else
                OutputPos.x -= 0.1f;
            ActivePlayer.transform.position = OutputPos;
        }
    }
}