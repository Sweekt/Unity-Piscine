using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [Header("Game Data")]
    public int score = 0;
    private int _oldscore;
    
    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public GameObject messagePanel;

    void Start() {
        UpdateUI();
    }

    public void AddScore(int amount) {
        score += amount;
        UpdateUI();
    }

    void UpdateUI() {
        if (scoreText == null) {
            GameObject textObj = GameObject.Find("ScoreText");
            if (textObj != null) scoreText = textObj.GetComponent<TextMeshProUGUI>();
        }
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void LoadNextLevel() {
        _oldscore = score;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void RestartLevel() {
        score = _oldscore;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}