using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gem : MonoBehaviour, IIColectible
{
    [SerializeField] private AudioClip gemSound;
    [SerializeField] private GameObject portal;
    [SerializeField] private AudioClip portalSound;

    public void Collect()
    {
        SoundManager.instance.PlaySound(gemSound);
        StartCoroutine(Portal());
        
      
       
    }
    
    IEnumerator Portal()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.instance.PlaySound(portalSound);
        portal.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        portal.SetActive(false);
    }
}
