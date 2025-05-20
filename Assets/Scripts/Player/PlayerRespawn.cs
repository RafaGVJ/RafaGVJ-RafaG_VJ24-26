using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
   [SerializeField] private AudioClip checkpointSound;
  
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;

    public Transform CurrentCheckpoint { get => currentCheckpoint; set => currentCheckpoint = value; }

    private void Awake()
    {
        if (playerHealth == null)
            playerHealth = GetComponent<Health>();

        if (playerHealth == null)
            playerHealth = FindObjectOfType<Health>(); // Garante que vai encontrar, mesmo em outro GameObject

        uiManager = FindObjectOfType<UIManager>();

        
    }
    
   
    public void CheckRespawn()
    {
        if (currentCheckpoint == null)
        {
            if (uiManager != null)
                uiManager.OnGameOver();
            else
                Debug.LogError("UIManager está nulo ao tentar chamar OnGameOver!");

            return;
        }

        transform.position = currentCheckpoint.position;

        if (playerHealth != null)
            playerHealth.Respawn();
        else
            Debug.LogError("PlayerHealth está nulo ao tentar chamar Respawn!");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.tag == "CheckPoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.Instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("Apear");
        }
    }
}


    
