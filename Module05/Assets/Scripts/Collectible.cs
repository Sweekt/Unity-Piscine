using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int points = 5;
    public AudioClip collectSound;
    private string _uniqueID;
    
    void Start() {
        _uniqueID = "Leaf_" + transform.position.x + "_" + transform.position.y;
        if (PlayerPrefs.GetInt(_uniqueID) == 1) {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            GameManager.instance.AddLevelScore(points);
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            PlayerPrefs.SetInt(_uniqueID, 1);
            PlayerPrefs.Save();
            Destroy(gameObject);
        }
    }
}