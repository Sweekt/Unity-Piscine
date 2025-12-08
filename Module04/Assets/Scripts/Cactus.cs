using UnityEngine;

public class CactusShooter : MonoBehaviour
{
    public Animator animator;
    public AudioClip throwSound;
    public GameObject jellyPrefab;
    public Transform firePoint;
    public float detectionRange = 5f;
    public float shootCooldown = 2f;
    
    private Transform _player;
    private float _timer;
    private AudioSource _audioSource;

    void Start() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        float distance = Vector2.Distance(transform.position, _player.position);
        if (distance < detectionRange) {
            _timer += Time.deltaTime;
            if (_timer > shootCooldown) {
                Shoot();
                _timer = 0;
            }
        }
    }

    void Shoot() {
        animator.SetTrigger("Attack");
        if (throwSound && _audioSource)
            _audioSource.PlayOneShot(throwSound);
        Instantiate(jellyPrefab, firePoint.position, firePoint.rotation);
    }
    
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}