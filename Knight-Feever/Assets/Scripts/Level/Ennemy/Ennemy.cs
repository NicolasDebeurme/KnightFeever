using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public int health = 100;

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }
    public Rigidbody2D EnnemyBody;
    public float speed = 10.0f;
    [HideInInspector] public bool isMoving = true;

    List<Vector3> UsedlootPos=new List<Vector3>();
    public GameObject GoldLootPrefab;
    public GameObject ManaLootPrefab;
    [HideInInspector] public GameObject dropLootTarget;
    [HideInInspector] public bool isAttacking=false;

    private void Start()
    {
        dropLootTarget = GameObject.FindGameObjectWithTag("Player");
    }
    

    //Called at the end of spawning animation
    void StartMoving()
    {
        isMoving = false;
    }

    public void DistanceAttack(float Range, int Damage, GameObject projectile)
    {
        GameObject player = Player.Instance.gameObject;


        if (Vector2.Distance(transform.position, player.transform.position) <= Range && Player.Instance!=null)
        {

            if (projectile != null)
            {
                Vector2 projectileDir = player.transform.position - transform.position;
                Quaternion projectileRot = Quaternion.AngleAxis(Mathf.Atan2(projectileDir.y, projectileDir.x) * Mathf.Rad2Deg, Vector3.forward);

                Vector3 ProjectileSpawnPos = transform.position;
                ProjectileSpawnPos += new Vector3(projectileDir.x, projectileDir.y, 0) * 0.1f;

                GameObject projectileInstance = Instantiate(projectile, ProjectileSpawnPos, projectileRot);
                projectileInstance.GetComponent<Projectile>().Damage = Damage;

            }

        }
    }
    public void CloseAttack(float Range, int Damage)
    {
        GameObject player = Player.Instance.gameObject;


        if (Vector2.Distance(transform.position, player.transform.position) <= Range && Player.Instance!=null)
        {

            IDamageable damageable = player.GetComponent<IDamageable>();
            
            
            if(damageable != null)
            {
                damageable.TakeDamage(Damage);
            }

        }
    }
    public IEnumerator AttackDelay(float Time)
    {
        yield return new WaitForSeconds(Time);
        
        isAttacking =true;
    }

    public void Die()
    {
        int numberofloot=Random.Range(1,4);
        for (int i = 0; i < numberofloot; i++)
        {
            int typeofloot=Random.Range(0,2);

            GameObject Drop=null;

            Vector3 RandomPos=new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));

            while (UsedlootPos.Contains(RandomPos))
            {
                RandomPos = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            }
            UsedlootPos.Add(RandomPos);

            if(typeofloot==0)
                Drop = Instantiate(GoldLootPrefab, transform.position - Vector3.forward + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)), Quaternion.identity);
            else
                Drop = Instantiate(ManaLootPrefab, transform.position - Vector3.forward + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)), Quaternion.identity);

            Drop.GetComponent<Follow>().target = dropLootTarget;

            UsedlootPos.Clear();
        }

        Destroy(this.gameObject);
        
        LevelManager.Instance.actualnbennemies--;
    }
}
