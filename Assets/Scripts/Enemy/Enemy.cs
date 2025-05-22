using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController playerDectected = collision.gameObject.GetComponent<PlayerController>();
        if (playerDectected != null)
        {
            collision.rigidbody.GetComponent<Health>().TakeDamage(damage);

        }
    }


}
   


