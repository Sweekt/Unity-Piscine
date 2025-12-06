using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject target;
    public float speed = 0.5f;
    public float health = 3f;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            BulletMovement scriptBullet = other.GetComponent<BulletMovement>();
            if (scriptBullet)
                health -= scriptBullet.damages;
            Destroy(other.gameObject);
            if (health <= 0)
                Destroy(this.gameObject);
        }
    }
}
