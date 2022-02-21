using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    public enum TargetedLayer
    {
        Enemy=7,
        Player=8,
    };
    public int Damage=0;
    public TargetedLayer[] targetedLayer;
    public float bulletSpeed = 0.5f;

    LayerMask Hitablelayermask =1<<6;

    Vector3 PreviousPos;
    private void Start() {

        for(int item=0;item<targetedLayer.Length;item++)
        {
            Hitablelayermask = Hitablelayermask | (1 << (int)targetedLayer[item]);
        }

        PreviousPos = transform.position;
        
    }
    

    void Update()
    {
        PreviousPos = transform.position;
        transform.Translate(Vector3.right * Time.deltaTime * bulletSpeed);
        
        RaycastHit2D hit = Physics2D.Raycast(PreviousPos, transform.position - PreviousPos, Vector3.Distance(PreviousPos, transform.position), Hitablelayermask);
        
        if (hit.collider != null)
        {

            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            
            
            if(damageable != null)
            {
                damageable.TakeDamage(Damage);
            }
            Destroy(gameObject);
        }

    }
    
    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.gameObject.layer == 1<<8)
    //     {
    //         IDamageable damageable = other.GetComponent<IDamageable>();
    //         if(damageable != null)
    //         {
    //             damageable.TakeDamage(33);
    //         }
    //         Destroy(gameObject);
    //     }
    // }
}
