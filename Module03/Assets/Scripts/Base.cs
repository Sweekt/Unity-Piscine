using UnityEngine;

public class Base : MonoBehaviour
{
    public int health = 5;
    public float rotateSpeed = 15f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other) {
        health -= 1;
        Debug.Log("You lost one health point. HP remaining: " + health);
        Destroy(other.gameObject);
        if (health <= 0) {
            GetComponent<SpriteRenderer>().enabled = false;
            DespawnAllEnemies();
            Debug.Log("Game Over!");
            if(GameManager.Instance) GameManager.Instance.GameOver();
        }
    }
    
    void DespawnAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            Destroy(enemy);
        }
    }
}
