using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Base target; 
    public GameObject spawnie;
    public float freq = 2.0f;
    
    private float timer;
    private bool gameEnded = false;

    void Update() {
        if (target == null) return;
        if (target.health <= 0) {
            if (!gameEnded) {
                DespawnAllEnemies();
                gameEnded = true;
            }
            return;
        }
        timer += Time.deltaTime;
        if (timer >= freq) {
            SpawnEnemy();
            timer = 0;
        }
    }

    void SpawnEnemy() {
        GameObject newEnemy = Instantiate(spawnie, transform.position, Quaternion.identity);
        EnemyMovement scriptEnemy = newEnemy.GetComponent<EnemyMovement>();
        if (scriptEnemy != null) {
            scriptEnemy.target = target.gameObject; 
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