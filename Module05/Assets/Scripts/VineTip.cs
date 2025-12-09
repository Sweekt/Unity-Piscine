using UnityEngine;

public class VineTip : MonoBehaviour
{
    public int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player) {
                player.TakeDamage(damage);
            }
        }
    }
}