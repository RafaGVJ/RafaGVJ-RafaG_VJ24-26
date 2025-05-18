using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Void : MonoBehaviour
{
    [SerializeField] private AudioSource playerFall;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Quando o jogador entrar no void
        {
            collision.GetComponent<PlayerRespawn>().CheckRespawn();// Respawn no último checkpoint ou reinício da cena
            playerFall.Play();
        }
        
    }
}