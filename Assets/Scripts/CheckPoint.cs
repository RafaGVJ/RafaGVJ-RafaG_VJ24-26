using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerRespawn respawn = collision.GetComponent<PlayerRespawn>();
                if (respawn != null)
                {
                    respawn.CurrentCheckpoint = transform;
                    // Tocar som, animação etc.
                }
            }
        }
    
}
