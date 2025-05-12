using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
   [Header("Damage")]
   [SerializeField] private float fireDamage;


   [Header("FireTrap Timers")]
   [SerializeField] private float activationDelay;
   [SerializeField] private float activationTime;


    [SerializeField] private AudioClip fireSound;

    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool isTriggered;
    private bool isActive;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();    
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player playerDectected = collision.GetComponent<Player>();

        if (playerDectected != null)
        {
            if (!isTriggered)
            {
                StartCoroutine(ActivateFireTrap());
            }
            if (isActive)
            {
                
                collision.GetComponent<Health>().TakeDamage(fireDamage);
            }
        }
    }
    private IEnumerator ActivateFireTrap()
    {
        //Turn the sprite red to notify the player and trigger the trap
        isTriggered = true;
        spriteRend.color = Color.red;

        //Wait for delay,activate trap,turn on animation, return color back to normal
        yield return new WaitForSeconds(activationDelay);

        spriteRend.color = Color.white;
        isActive = true;
        anim.SetBool("Activated",true);
        SoundManager.instance.PlaySound(fireSound);
        //Wait until X seconds,deactivate trap and restart
        yield return new WaitForSeconds(activationTime);

        isActive = false;
        isTriggered = false;
        anim.SetBool("Activated", false);
    }

}
