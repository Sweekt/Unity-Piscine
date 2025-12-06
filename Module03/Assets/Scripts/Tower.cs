using UnityEngine;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    public GameObject bullet;
    public List<GameObject> enemiesInRange = new List<GameObject>();
    public float freq = 3.0f;
    public float damages = 0.1f;
    private float timer;
    void Update()
    {
        if (enemiesInRange.Count > 0) {
            timer += Time.deltaTime;
            if (timer >= freq) {
                GameObject closestEnemy = findClosest();
                Shoot(closestEnemy);
                timer = 0;
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            enemiesInRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            enemiesInRange.Remove(other.gameObject);
        }
    }

    void Shoot(GameObject target) {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        BulletMovement scriptBullet = newBullet.GetComponent<BulletMovement>();
        if (scriptBullet != null) {
            scriptBullet.target = target.gameObject;
            scriptBullet.damages = damages;
        }
    }

    GameObject findClosest() {
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        for (int i = enemiesInRange.Count - 1; i >= 0; i--) {
            if (enemiesInRange[i] == null) {
                enemiesInRange.RemoveAt(i);
                continue;
            }
            float distance = Vector3.Distance(enemiesInRange[i].transform.position, currentPos);
            if (distance < minDistance) {
                closest = enemiesInRange[i];
                minDistance = distance;
            }
        }
        return closest;
    }
}