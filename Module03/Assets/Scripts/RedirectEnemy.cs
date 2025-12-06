using UnityEngine;

public class RedirectEnemy : MonoBehaviour
{
    public GameObject nextTarget;
    void OnTriggerEnter2D(Collider2D other) {
        EnemyMovement Enemy = other.GetComponent<EnemyMovement>();
        if (Enemy)
            Enemy.target = nextTarget;
    }
}
