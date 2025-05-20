using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public event Action<float, float> OnHealthChanged;

    public float StartingHealth => startingHealth;
    public float CurrentHealth => currentHealth;
    public bool IsDead => currentHealth <= 0;

    [SerializeField] private float startingHealth = 10f;
    private float currentHealth;

    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private Animator anim;
    [SerializeField] private Collider playerCollider;

    private void Start()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        currentHealth = startingHealth;
        OnHealthChanged?.Invoke(currentHealth, startingHealth);
        anim.SetTrigger("Idle");
        if (playerCollider != null)
            playerCollider.enabled = true;
    }
    public void AddHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, startingHealth);
        OnHealthChanged?.Invoke(currentHealth, startingHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        OnHealthChanged?.Invoke(currentHealth, startingHealth);

        if (currentHealth > 0)
        {
            SoundManager.Instance.PlaySound(hurtSound);
            anim.SetTrigger("Hit");
        }
        else
        {
            SoundManager.Instance.PlaySound(deathSound);
            anim.SetTrigger("Dead");
            if (playerCollider != null)
                playerCollider.enabled = false;
        }
    }
    public void Respawn()
    {
        currentHealth = startingHealth;
        anim.SetTrigger("Idle");  
        if (playerCollider != null)
            playerCollider.enabled = true;
        OnHealthChanged?.Invoke(currentHealth, startingHealth);
    }
}
