using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip respawnSound;
    [SerializeField] private AudioClip portalSound;
    [SerializeField] private float startingHealth;
    [SerializeField] private GameObject portal;
   
    

    private float currentHealth;

    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        portal.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0,startingHealth);

        if (currentHealth > 0)
        {
            SoundManager.instance.PlaySound(hurtSound);
            anim.SetTrigger("Hit");
        }
        else
        {
            SoundManager.instance.PlaySound(deathSound);
            anim.SetTrigger("Dead");
            
           
        }
    }
    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }

    public void Respawn()
    {
        SoundManager.instance.PlaySound(respawnSound);
        StartCoroutine(Portal());
        AddHealth(startingHealth);
        anim.ResetTrigger("Dead");
        anim.Play("Idle_Finn");
       
       
    }
    IEnumerator Portal()
    {
        SoundManager.instance.PlaySound(portalSound);
        portal.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        portal.SetActive(false);
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
        
    }
}
