using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    [Header("Global Progress")]
    public int totalScore = 0;
    public int deathCount = 0;
    public int unlockedStage = 1;

    [Header("Current Level Data")]
    public int levelScore = 0;
    
    private bool _isLoadingSave = false;
    
    [Header("UI References")]
    public GameObject hudContainer;
    public TextMeshProUGUI hudScoreText;
    public TextMeshProUGUI hudHPText;

    public GameObject inGameMessagePanel; 

    void Awake() {
        if (instance && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable() { SceneManager.sceneLoaded += OnSceneLoaded; }
    void OnDisable() { SceneManager.sceneLoaded -= OnSceneLoaded; }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (hudContainer != null) {
            bool isMenuOrDiary = scene.name == "MainMenu" || scene.name == "Diary";
            hudContainer.SetActive(!isMenuOrDiary);
        }
        if (!_isLoadingSave && scene.name.StartsWith("Stage")) {
            levelScore = 0; 
            UpdateHUD(3);
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject restartBtn = GameObject.Find("RestartButton"); 
        if (restartBtn) restartBtn.GetComponent<Button>().onClick.AddListener(RestartLevel);
        GameObject menuBtn = GameObject.Find("MenuButton");
        if (menuBtn) menuBtn.GetComponent<Button>().onClick.AddListener(ReturnToMenu);
        if (_isLoadingSave) {
            ApplyLoadedData();
            _isLoadingSave = false;
        }
        else if (player != null && scene.name != "MainMenu") {
            GameObject startPoint = GameObject.FindGameObjectWithTag("StartPoint");
            if (startPoint != null) player.transform.position = startPoint.transform.position;
            SaveGame(); 
        }
        UpdateHUDUI();
    }
    
    public void AddLevelScore(int amount) {
        levelScore += amount;
        UpdateHUDUI();
    }

    public void UpdateHUD(int currentHP) {
        if (hudHPText) hudHPText.text = "HP: " + currentHP;
        UpdateHUDUI();
    }

    void UpdateHUDUI() {
        if (hudScoreText) hudScoreText.text = "Leaves: " + levelScore;
    }
    
    public void AddDeath() {
        deathCount++;
        PlayerPrefs.SetInt("DeathCount", deathCount); 
    }
    
    public void LoadNextLevel() {
        totalScore += levelScore;
        levelScore = 0;
        unlockedStage++;
        if (unlockedStage > 3) unlockedStage = 3;
        SaveGame();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(currentSceneIndex + 1);
        else
            ReturnToMenu();
    }

    public void RestartLevel() {
        levelScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SaveGame() {
        if (SceneManager.GetActiveScene().name == "MainMenu") return;

        PlayerPrefs.SetInt("TotalScore", totalScore);
        PlayerPrefs.SetInt("DeathCount", deathCount);
        PlayerPrefs.SetInt("UnlockedStage", unlockedStage);
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("CurrentLevelScore", levelScore);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            PlayerController pc = player.GetComponent<PlayerController>();
            PlayerPrefs.SetInt("HP", pc.currentHealth);
            PlayerPrefs.SetFloat("PosX", player.transform.position.x);
            PlayerPrefs.SetFloat("PosY", player.transform.position.y);
            PlayerPrefs.SetFloat("PosZ", player.transform.position.z);
        }
        PlayerPrefs.Save();
    }

    public void LoadGame() {
        if (!PlayerPrefs.HasKey("LastScene")) return;
        _isLoadingSave = true;
        totalScore = PlayerPrefs.GetInt("TotalScore");
        deathCount = PlayerPrefs.GetInt("DeathCount");
        unlockedStage = PlayerPrefs.GetInt("UnlockedStage");
        levelScore = PlayerPrefs.GetInt("CurrentLevelScore");
        SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
    }
    
    public void NewGame() {
        PlayerPrefs.DeleteAll();
        totalScore = 0;
        levelScore = 0;
        deathCount = 0;
        unlockedStage = 1;
        SceneManager.LoadScene("Stage1");
    }

    void ApplyLoadedData() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            float x = PlayerPrefs.GetFloat("PosX");
            float y = PlayerPrefs.GetFloat("PosY");
            float z = PlayerPrefs.GetFloat("PosZ");
            player.transform.position = new Vector3(x, y, z);

            int savedHP = PlayerPrefs.GetInt("HP", 3);
            player.GetComponent<PlayerController>().SetHealth(savedHP);
            UpdateHUD(savedHP);
        }
    }

    public void ReturnToMenu() {
        SaveGame();
        SceneManager.LoadScene("MainMenu");
    }
}