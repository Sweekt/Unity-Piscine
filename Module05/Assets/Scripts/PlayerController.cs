using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip jumpClip;
    public AudioClip damageClip;
    public AudioClip defeatClip;
    public AudioClip respawnClip;
    private AudioSource _audioSource;
    
    [Header("UI")]
    public CanvasGroup fadePanel;
    public float fadeDuration = 1f;
    
    [Header("Settings")]
    public int maxHealth = 3;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    
    private int _currentHealth;
    private Vector3 _respawnPoint;
    private bool _isDead = false;
    
    private float _horizontalInput;
    private bool _isGrounded;
    private bool _jumpRequest;
    private Vector3 _initialScale;

    void Start() {
        _initialScale = transform.localScale;
        _currentHealth = maxHealth;
        _respawnPoint = transform.position;
        _audioSource = GetComponent<AudioSource>();
    }
    
    void Update() {
        if (_isDead) return;
        
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && _isGrounded) {
            _jumpRequest = true;
            PlaySound(jumpClip);
        }
        if (_horizontalInput > 0) transform.localScale = new Vector3(Mathf.Abs(_initialScale.x), _initialScale.y, _initialScale.z);
        else if (_horizontalInput < 0) transform.localScale = new Vector3(-Mathf.Abs(_initialScale.x), _initialScale.y, _initialScale.z);
        animator.SetFloat("Speed", Mathf.Abs(_horizontalInput));
        animator.SetBool("IsJumping", !_isGrounded);
    }

    void FixedUpdate() {
        if (_isDead) {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        rb.linearVelocity = new Vector2(_horizontalInput * moveSpeed, rb.linearVelocity.y);
        if (_jumpRequest) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _jumpRequest = false;
        }
    }
    
    public void TakeDamage(int damage) {
        if (_isDead) return;

        _currentHealth -= damage;
        if (_currentHealth <= 0) {
            Die();
        }
        else {
            animator.SetTrigger("Hit");
            PlaySound(damageClip);
        }
    }
    
    void Die() {
        _isDead = true;
        animator.SetBool("IsDead", true);
        PlaySound(defeatClip);
        StartCoroutine(RespawnRoutine());
    }
    
    IEnumerator RespawnRoutine() {
        float timer = 0f;
        while (timer < fadeDuration) {
            timer += Time.deltaTime;
            fadePanel.alpha = timer / fadeDuration;
            yield return null;
        }
        fadePanel.alpha = 1f;
        yield return new WaitForSeconds(0.5f);
        transform.position = _respawnPoint;
        _currentHealth = maxHealth;
        _isDead = false;
        animator.SetBool("IsDead", false);
        PlaySound(respawnClip);
        timer = 0f;
        while (timer < fadeDuration) {
            timer += Time.deltaTime;
            fadePanel.alpha = 1f - (timer / fadeDuration);
            yield return null;
        }
        fadePanel.alpha = 0f;
    }
    
    void PlaySound(AudioClip clip) {
        _audioSource.PlayOneShot(clip);
    }
}