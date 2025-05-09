using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gem : MonoBehaviour, IIColectible
{
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private GameObject portal;
    [SerializeField] private AudioClip portalSound;

    [SerializeField] private int sceneNumber;


    public void Collect()
    {
        SoundManager.instance.PlaySound(coinSound);
        StartCoroutine(Portal());
        StartCoroutine(GemCaught());
      
       
    }
    IEnumerator GemCaught()
    {
       
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneNumber);
        
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
