using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
   [SerializeField] private float attackCooldown;
   [SerializeField] private Transform firePoint;
   [SerializeField] private GameObject[] arrows;
   [SerializeField] private AudioClip arrowSound;

    private float cooldownTimer;

    void Attack()
    {
        cooldownTimer = 0;

        int arrowIndex = FindArrows();
        if (arrowIndex == -1)
        {
            Debug.LogError("No available arrows");
            return;
        }

        GameObject arrow = arrows[arrowIndex];
    arrow.transform.position = firePoint.position;
    arrow.GetComponent<EnemyProjectiles>().ActivateProjectile();
    
    SoundManager.instance.PlaySound(arrowSound);
        
    }

    private int FindArrows()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }
}
