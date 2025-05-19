using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLoader : MonoBehaviour
{
 
   [SerializeField] private GameObject gameManagerPrefab;

   private void Awake()
   {
      if (GameManager.Instance == null)
      {
       Instantiate(gameManagerPrefab);
      }
   }
    
}
