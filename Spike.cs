using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour,IDamageble
{
   [SerializeField] protected float damage;
    [SerializeField] private  AudioClip  hitSound;

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
}
