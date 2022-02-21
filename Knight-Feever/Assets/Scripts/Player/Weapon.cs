using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject[] Projectiles;
    public Sprite[] Weapons;
    public GameObject shotEffect;
    public SpriteRenderer WeaponSprite;


    public PlayerData playerData;
    public float AttackRange = 0.5f;
    GameObject newbullet;
    Vector2 weaponDir;
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Weapons[playerData.Actualweapontype];
    }


    void Update()
    {
        //Check changement d'arme
        if (gameObject.GetComponent<SpriteRenderer>().sprite != Weapons[playerData.Actualweapontype])
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Weapons[playerData.Actualweapontype];
        }
        //Tir
        if (Input.GetKeyDown(KeyCode.Space) && Player.Instance.Mana > 0)
        {
            Player.Instance.Mana--;

            Vector2 weaponDir = Player.Instance.weaponDirection;
            Quaternion weaponRot = Quaternion.AngleAxis(Mathf.Atan2(weaponDir.y, weaponDir.x) * Mathf.Rad2Deg, Vector3.forward);

            Vector3 bulletSpawnPos = transform.position;
            bulletSpawnPos += new Vector3(weaponDir.x, weaponDir.y, 0) * 0.2f;

            newbullet = Instantiate(Projectiles[playerData.Actualweapontype], bulletSpawnPos, weaponRot);

            newbullet.GetComponent<Projectile>().Damage = playerData.Actualweapontype + 1;


            if (playerData.Actualweapontype != 0)
            {
                StartCoroutine(ShotEffect());
            }

        }
        else if (Input.GetKeyDown(KeyCode.Space) && Player.Instance.Mana < 0 && Player.Instance.closestEnnemy != null)
        {
            if (Vector2.Distance(transform.position, Player.Instance.closestEnnemy.transform.position) <= AttackRange && Player.Instance != null)
            {

                IDamageable damageable = Player.Instance.closestEnnemy.GetComponent<IDamageable>();


                if (damageable != null)
                {
                    damageable.TakeDamage(1);
                }

            }
        }
    }

    public void UpgradeWeapon()
    {
        playerData.Actualweapontype++;
    }
    IEnumerator ShotEffect()
    {
        shotEffect.transform.position = newbullet.transform.position;
        shotEffect.transform.eulerAngles = newbullet.transform.eulerAngles + new Vector3(0, 0, 180);
        shotEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        shotEffect.SetActive(false);
    }
}
