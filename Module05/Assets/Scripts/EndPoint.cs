using UnityEngine;
using TMPro;

public class EndPoint : MonoBehaviour
{
    public int requiredScore = 25;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (GameManager.instance.levelScore >= requiredScore) {
                Debug.Log("Niveau terminé !");
                GameManager.instance.LoadNextLevel();
            }
            else {
                Debug.Log("Pas assez de points !");
                if (GameManager.instance.inGameMessagePanel) {
                    GameManager.instance.inGameMessagePanel.SetActive(true);
                    Invoke("HideMessage", 2f);
                }
            }
        }
    }
    
    void HideMessage() {
        if (GameManager.instance.inGameMessagePanel) GameManager.instance.inGameMessagePanel.SetActive(false);
    }
}