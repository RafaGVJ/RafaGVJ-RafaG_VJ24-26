using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
   [SerializeField] protected float damage;
  
    private PlayerController playerController;
    private void Awake()
    {
        playerController= FindAnyObjectByType<PlayerController>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Health playerHealth = collision.GetComponent<Health>();
        PlayerController playerDetected = collision.GetComponent<PlayerController>();

        // Garante que só aplica dano se for o jogador e se ele não estiver morto
        if (playerHealth != null && playerDetected != null && !playerHealth.isDead)
        {
            playerHealth.TakeDamage(damage);
            
        }
    }
}
