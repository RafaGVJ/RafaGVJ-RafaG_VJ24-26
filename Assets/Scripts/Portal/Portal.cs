using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
   [SerializeField] private UnityEvent entrouNoPortal;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player playerDectected = collision.GetComponent<Player>();
        if (playerDectected != null)
        {
            entrouNoPortal?.Invoke();
        }
    }

   
   
}
