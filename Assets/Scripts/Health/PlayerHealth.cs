using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Componentes")]
    [SerializeField] private Animator anim;
    [SerializeField] private Collider2D playerCollider;

    [Header("Áudio")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    [Header("Vida")]
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;
    

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool isDead { get; private set; } = false;

    public event Action<int, int> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    private void Start()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        anim.SetTrigger("Idle");

        if (playerCollider != null)
            playerCollider.enabled = true;

        isDead = false;
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        currentHealth = Mathf.Clamp(currentHealth - damageAmount, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth > 0)
        {
            OnHit();
        }
        else
        {
            isDead = true;
            OnDeath?.Invoke();
            Die();
        }
    }

    private void OnHit()
    {
        SoundManager.instance?.PlaySound(hurtSound);
        anim.SetTrigger("Hit");
    }

    private void Die()
    {
        SoundManager.instance?.PlaySound(deathSound);
        anim.SetTrigger("Dead");
        anim.SetBool("IsDead", true);

        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;

        GetComponent<PlayerController>().enabled = false;
    }

    public void AddHealth(int value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void Respawn()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        isDead = false;
        anim.SetBool("IsDead", false);
        anim.SetTrigger("Idle");
        var rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = Vector2.zero;

        var controller = GetComponent<PlayerController>();
        if (controller != null)
            controller.enabled = true;

    }
}
