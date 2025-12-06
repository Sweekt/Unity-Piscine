using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Configuration")]
    public Base baseTarget;
    public GameObject target; 
    public GameObject spawnie;
    public float freq = 2.0f;
    [Header("Wave system")]
    public int totalEnemiesInWave = 10;
    private int currentEnemiesSpawned = 0;
    private float timer;
    private bool gameEnded = false;
    private bool waveFinishedSpawning = false;

    void Update() {
        // Lose
        if (baseTarget.health <= 0) {
            gameEnded = true;
        }
        // Win
        if (waveFinishedSpawning && !gameEnded) {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
                gameEnded = true;
                Debug.Log("Victoire !");
                if(GameManager.Instance) GameManager.Instance.LevelComplete();
            }
        }
        // Enemy spawn
        if (!waveFinishedSpawning) {
            timer += Time.deltaTime;
            if (timer >= freq) {
                SpawnEnemy();
                timer = 0;
            }
        }
    }

    void SpawnEnemy() {
        if (currentEnemiesSpawned >= totalEnemiesInWave) {
            waveFinishedSpawning = true;
            return;
        }
        GameObject newEnemy = Instantiate(spawnie, transform.position, Quaternion.identity);
        EnemyMovement scriptEnemy = newEnemy.GetComponent<EnemyMovement>();
        if (scriptEnemy) {
            scriptEnemy.target = target.gameObject; 
        }
        currentEnemiesSpawned++;
    }

    
}