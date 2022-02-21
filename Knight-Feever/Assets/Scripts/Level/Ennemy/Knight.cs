using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Ennemy, IDamageable
{
    
    int randomMouvement = 0;
    Vector3 actualPlayerPos = new Vector3(0, 0, 0);

    public float RangeAttack = 2;

    public int AttackDamage =3;
    public float TimeBtwAttacks=2;

    private void Update()
    {
        if (Player.Instance != null)
        {
            Move();

            if(isAttacking)
            {
                isAttacking=false;
                CloseAttack(RangeAttack,AttackDamage);
                StartCoroutine(AttackDelay(TimeBtwAttacks));
            }
        }
    }
    public void Move()
    {
        if (!isMoving)
        {
            isMoving = true;

            randomMouvement = Random.Range(0, 4);

            actualPlayerPos = Player.Instance.transform.position;

            StartCoroutine(MoveTime());
        }

        if (randomMouvement == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.transform.RotateAround(actualPlayerPos, Vector3.forward, speed * Time.deltaTime);
        }
        else if (randomMouvement == 2)
        {
            EnnemyBody.AddForce((transform.position - actualPlayerPos) * speed);
        }
        else if (randomMouvement == 3)
        {
            EnnemyBody.AddForce(-(transform.position - actualPlayerPos) * speed);
        }
    }

    //Time between each mouvement
    IEnumerator MoveTime()
    {
        yield return new WaitForSeconds(3);
        isMoving = false;
        randomMouvement = 0;
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    

}
