using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip healthSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerDectet = collision.GetComponent<PlayerController>();
        if (playerDectet != null)
        {
            collision.GetComponent<Health>().AddHealth(healthValue);
            SoundManager.instance.PlaySound(healthSound);
            Destroy(gameObject);
        }
    }
}
