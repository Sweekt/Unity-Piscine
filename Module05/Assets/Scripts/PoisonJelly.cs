using UnityEngine;

public class PoisonJelly : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 1;

    void Update() {
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
        if (transform.position.y < -10)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            Destroy(gameObject);
        }
    }
}