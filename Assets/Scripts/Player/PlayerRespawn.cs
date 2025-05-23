using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    [SerializeField] private GameObject portalPrefab;
    
    private Vector3 portalPos = new Vector3(0f, 2f, 0f);

    private GameObject currentPortalInstance;
    private Transform currentCheckpoint;
    private PlayerHealth playerHealth;
    private UIManager uiManager;
    //private Animator animator;

    public Transform CurrentCheckpoint { get => currentCheckpoint; set => currentCheckpoint = value; }

    private void Awake()
    {
        if (playerHealth == null)
            playerHealth = GetComponent<PlayerHealth>();

        if (playerHealth == null)
            playerHealth = FindObjectOfType<PlayerHealth>();

        uiManager = FindObjectOfType<UIManager>();
        //animator = FindObjectOfType<Animator>();

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

        ShowPortal();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.tag == "CheckPoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("Apear");
        }
    }
    private void ShowPortal()
    {
        if (portalPrefab == null)
        {
            Debug.LogWarning("PortalPrefab não está atribuído!");
            return;
        }

       
        if (currentPortalInstance != null)
            Destroy(currentPortalInstance);

        
        Vector3 spawnPosition = transform.position + portalPos;

        
        Quaternion portalRotation = Quaternion.Euler(0, 0, 90);

        // Instancia o portal
        currentPortalInstance = Instantiate(portalPrefab, spawnPosition, portalRotation);

        
        StartCoroutine(DestroyPortalAfterSeconds(1f));
    }



    private IEnumerator DestroyPortalAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (currentPortalInstance != null)
            Destroy(currentPortalInstance);
    }
}



