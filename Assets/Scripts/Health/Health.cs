using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;

public class Health : MonoBehaviour
{
    
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private Animator anim;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private float startingHealth;
    private float currentHealth;

    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool isDead { get; private set; } = false;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        
    }



    private void Start()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        currentHealth = startingHealth;
        
        anim.SetTrigger("Idle");
        if (playerCollider != null)
            playerCollider.enabled = true;
    }
   
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
           OnHit();
            
        }
        else
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        isDead = true;
        SoundManager.instance.PlaySound(deathSound);
        anim.SetTrigger("Dead");
        anim.SetBool("IsDead", true);

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        // Opcional: Trava a posição
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;

        // Desativa controle de movimento
        GetComponent<PlayerController>().enabled = false;
    }

    public void OnHit()
    {
        SoundManager.instance.PlaySound(hurtSound);
        anim.SetTrigger("Hit");
    }
    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }
    public void Respawn()
    {
        currentHealth = startingHealth;
        anim.SetTrigger("Idle");  
        if (playerCollider != null)
            playerCollider.enabled = true;
        
    }
}
