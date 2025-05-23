using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private int healthValue;
    [SerializeField] private AudioClip healthSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerDectet = collision.GetComponent<PlayerHealth>();
        if (playerDectet != null)
        {
            collision.GetComponent<PlayerHealth>().AddHealth(healthValue);
            SoundManager.instance.PlaySound(healthSound);
            Destroy(gameObject);
        }
    }
}
