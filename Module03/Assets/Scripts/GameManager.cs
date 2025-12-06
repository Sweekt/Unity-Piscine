using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("UI References")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [Header("Pause State")]
    [SerializeField] private bool isPaused = false;
    [Header("Player Stats")]
    [SerializeField] private int playerCurrentLife;
    public int energy = 0;
    public int energyCap = 50;
    public float energyPerSec = 1;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI hpText;
    public Base baseScript;
    public PauseManager pauseManager;
    float timer = 0;

    void Awake() { Instance = this; }

    void Update() {
        timer += Time.deltaTime;
        if (timer >= 1 / energyPerSec) {
            if (energy < energyCap)
                energy += 1;
            timer = 0;
        }
        energyText.text = "Energy: " + energy + "/" + energyCap;
        hpText.text = "HP: " + baseScript.health;
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    void TogglePause() {
        if (!isPaused) {
            isPaused = true;
            Time.timeScale = 0f;
            pauseManager.toggleOn();
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseManager.toggleOff();
    }
    
    public bool CanAfford(int cost)
    {
        return energy >= cost;
    }

    public void SpendEnergy(int cost)
    {
        energy -= cost;
    }
    
    public bool IsPaused => isPaused;
    
    public void GameOver()
    {
        Time.timeScale = 0f;
        losePanel.SetActive(true);
    }
    
    public void LevelComplete()
    {
        Time.timeScale = 0f;
        CalculateAndShowRank();
        winPanel.SetActive(true);
    }
    
    void CalculateAndShowRank()
    {
        // --- Formule de Score ---
        int finalScore = baseScript.health * (energy * 2);
        
        // --- Système de Rangs ---
        string rank = "C - SURVIVOR";

        if (finalScore >= 400) rank = "S - GODLIKE";
        else if (finalScore >= 250) rank = "A - EXCELLENT";
        else if (finalScore >= 100) rank = "B - GOOD";

        // --- Affichage ---
        rankText.text = rank;
        scoreText.text = "Score: " + finalScore;
        if(rank.Contains("S")) rankText.color = Color.yellow;
    }
}