using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
   [SerializeField] protected float damage;
    [SerializeField] private  AudioClip  hitSound;

  protected void OnTriggerEnter2D(Collider2D collision)
    {
        Player playerDectected = collision.GetComponent<Player>();
        if(playerDectected != null)
        {
            collision.GetComponent<Health>().TakeDamage(damage);  
            SoundManager.Instance.PlaySound(hitSound);
        }
    }
}
