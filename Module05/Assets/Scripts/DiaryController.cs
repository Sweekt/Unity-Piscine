using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DiaryController : MonoBehaviour {
    
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI deathCountText;
    
    [Header("Images des Stages")]
    public Image stage1Image;
    public Image stage2Image;
    public Image stage3Image;

    void Start() {
        if (GameManager.instance == null) return;
        totalScoreText.text = "" + GameManager.instance.totalScore;
        deathCountText.text = "" + GameManager.instance.deathCount;
        int unlocked = GameManager.instance.unlockedStage;
        if (stage1Image) stage1Image.color = Color.white; 
        if (stage2Image)
            stage2Image.color = (unlocked >= 2) ? Color.white : Color.gray;
        if (stage3Image)
            stage3Image.color = (unlocked >= 3) ? Color.white : Color.gray;
    }

    
    
    
    public void BackToMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}