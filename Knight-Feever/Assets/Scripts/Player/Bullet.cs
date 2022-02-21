using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 0.5f;

    int Hitablelayermask = 1 << 7 | 1 << 6;

    Vector3 PreviousPos;

    private void Start()
    {
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
                damageable.TakeDamage(33);
            }
            Destroy(gameObject);
        }

    }
}
