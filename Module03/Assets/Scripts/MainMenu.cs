using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void StartGame() {
        SceneManager.LoadScene("map01");
    }

    public void CloseGame() {
        Debug.Log("Le jeu se ferme !");
        Application.Quit();
    }
}