using UnityEngine;
using System.Collections;

public class InteractiveVine : MonoBehaviour
{
    public Animator animator;
    public AudioClip attackSound;
    public float attackCooldown = 2f;
    private bool _isPlayerInRange = false;
    private bool _isAttacking = false;
    private AudioSource _audioSource;
    
    void Start() {
        _audioSource = GetComponent<AudioSource>(); // NOUVEAU
    }
    
    void Update() {
        if (_isPlayerInRange && !_isAttacking)
            StartCoroutine(AttackRoutine());
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player"))
            _isPlayerInRange = true;
    }
    
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            _isPlayerInRange = false;
        }
    }

    IEnumerator AttackRoutine() {
        _isAttacking = true;
        animator.SetTrigger("Attack");
        if (attackSound && _audioSource) 
            _audioSource.PlayOneShot(attackSound);
        yield return new WaitForSeconds(attackCooldown);
        _isAttacking = false;
    }
}