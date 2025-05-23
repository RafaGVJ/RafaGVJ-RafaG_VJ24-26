using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
   [SerializeField] private UnityEvent entrouNoPortal;
    [SerializeField] private int sceneNumber;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerDectected = collision.GetComponent<PlayerController>();
        if (playerDectected != null)
        {
            entrouNoPortal?.Invoke();
            SceneManager.LoadScene(sceneNumber);
        }
    }
}
