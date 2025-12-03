using UnityEngine;
using UnityEngine.SceneManagement; // <--- OBLIGATOIRE pour changer de scène

public class EndingLevel : MonoBehaviour
{
    public PlayerMovement Claire;
    public PlayerMovement Thomas;
    public PlayerMovement John;
    public string nextLevelName = "Level2"; 

    private bool levelFinished = false;

    void Update() {
        // Si le niveau est déjà fini, on ne fait rien
        if (levelFinished) return;
        if (Claire.exitState() && Thomas.exitState() && John.exitState())
        {
            Debug.Log("Level Ended - Loading next scene...");
            levelFinished = true;
            if (nextLevelName == "gameEnd")
                Debug.Log("You've finished every level in the game, congratulation !");
            else
                SceneManager.LoadScene(nextLevelName);
        }
    }
}