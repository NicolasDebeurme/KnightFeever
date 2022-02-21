using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable: MonoBehaviour,IDamageable
{
    public GameObject NotDestroyed;
    public GameObject Destroyed;
    
    
    public int Health { get ; set ; }

    public void TakeDamage(int Damage)
    {
        NotDestroyed.SetActive(false);
        Destroyed.SetActive(true);
    }
}
