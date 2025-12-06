using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject confirmQuitPanel;

    public void toggleOn() {
        gameObject.SetActive(true);
        confirmQuitPanel.SetActive(false);
    }

    public void toggleOff() {
        gameObject.SetActive(false);
    }

    public void toggleConfirmation() {
        resumeButton.SetActive(false);
        quitButton.SetActive(false);
        confirmQuitPanel.SetActive(true);
    }
    
    public void CancelQuit() {
        resumeButton.SetActive(true);
        quitButton.SetActive(true);
        confirmQuitPanel.SetActive(false);
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
