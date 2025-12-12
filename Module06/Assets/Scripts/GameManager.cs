using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public TextMeshProUGUI keyText;
    public CanvasGroup winPanel;
    public CanvasGroup losePanel;
    public float fadeDuration = 1f;
    public int key;
    public AudioClip winClip;
    public AudioClip loseClip;
    private AudioSource _audioSource;
    public bool isGameOver = false;
    public PlayerController playerController;
    
    void Awake() {
        if (instance && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    public void getKey() {
        key++;
        updateUI();
    }

    void resetKey() {
        key = 0;
        updateUI();
    }

    void updateUI() {
        keyText.text = "Keys: " + key;
    }

    public void win()
    {
        PlaySound(winClip);
        StartCoroutine(WinRoutine());
    }

    public void lose()
    {
        if (isGameOver) return;
        isGameOver = true;
        PlaySound(loseClip);
        StartCoroutine(LoseRoutine());
    }
    
    IEnumerator WinRoutine() {
        float timer = 0f;
        while (timer < fadeDuration) {
            timer += Time.deltaTime;
            winPanel.alpha = timer / fadeDuration;
            yield return null;
        }
        winPanel.alpha = 1f;
        yield return new WaitForSeconds(0.5f);
        
        resetLevel();
        
        timer = 0f;
        while (timer < fadeDuration) {
            timer += Time.deltaTime;
            winPanel.alpha = 1f - (timer / fadeDuration);
            yield return null;
        }
        winPanel.alpha = 0f;
    }
    
    IEnumerator LoseRoutine() {
        float timer = 0f;
        while (timer < fadeDuration) {
            timer += Time.deltaTime;
            losePanel.alpha = timer / fadeDuration;
            yield return null;
        }
        losePanel.alpha = 1f;
        yield return new WaitForSeconds(0.5f); 
        
        resetLevel();
        
        timer = 0f;
        while (timer < fadeDuration) {
            timer += Time.deltaTime;
            losePanel.alpha = 1f - (timer / fadeDuration);
            yield return null;
        }
        losePanel.alpha = 0f;
        isGameOver = false;
    }

    void resetLevel()
    {
        resetKey();
        playerController.Reset();
        GhostAI[] ghosts = FindObjectsOfType<GhostAI>();
        foreach(GhostAI ghost in ghosts)
            ghost.ResetGhost();
        FloatingObject[] Keys = FindObjectsOfType<FloatingObject>();
        foreach (FloatingObject floatingObject in Keys)
            floatingObject.RespawnKey();
    }
    
    void PlaySound(AudioClip clip) {
        _audioSource.PlayOneShot(clip);
    }
}
