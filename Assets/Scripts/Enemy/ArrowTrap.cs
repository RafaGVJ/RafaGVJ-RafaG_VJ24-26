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
        arrows[FindArrows()].transform.position = firePoint.position;
       arrows[FindArrows()].GetComponent<EnemyProjectiles>().ActivateProjectile();
        SoundManager.Instance.PlaySound(arrowSound);
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
