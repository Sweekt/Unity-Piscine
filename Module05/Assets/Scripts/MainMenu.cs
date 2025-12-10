using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button resumeButton;
    public Button newGameButton;
    public Button diaryButton;

    void Start() {
        newGameButton.onClick.AddListener(() => GameManager.instance.NewGame());
        if (PlayerPrefs.HasKey("LastScene")) {
            resumeButton.interactable = true;
            resumeButton.onClick.AddListener(() => GameManager.instance.LoadGame());
        }
        else
            resumeButton.interactable = false; 
        diaryButton.onClick.AddListener(() => SceneManager.LoadScene("Diary"));
    }
}