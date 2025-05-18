using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour,IIColectible
{
    [SerializeField] private int points;
    [SerializeField] private AudioClip coinSound;
    private UIManager uiManager;

    
    private void Awake()
    {
        uiManager = FindAnyObjectByType<UIManager>();
       
    }
   
   public void Collect()
    {
        SoundManager.instance.PlaySound(coinSound);
        uiManager.OnAddPoints(points);
        Destroy(gameObject);
    }
}
