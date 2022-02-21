using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : PlayerController, IDamageable
{
    public int health = 6;
    public int Mana = 100;
    public int Armor = 7;

    public int Health
    {
        get => health;
        set => health = value;
    }

    int isTouched = 0;
    public static Player Instance;

    public HealthBar healthBar;
    public ManaBar manaBar;
    public ArmorBar armorBar;

    [HideInInspector] public int Gold = 0;
    public Text GoldText;

    

    void Start()
    {
        weapon = Instantiate(weapon, transform.position, Quaternion.identity);
        weapon.transform.parent = gameObject.transform;

        Instance = this;
        healthBar.SetMaxHealth(health);
        manaBar.SetMaxMana(Mana);
        armorBar.SetMaxArmor(Armor);

        Gold = playerData.Gold;
        GoldText.text = Gold.ToString();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        direction = new Vector2(h, v);

        UpdateUI();

    }

    private void FixedUpdate()
    {
        FindClosestEnemy();

        if(Input.GetKeyDown(KeyCode.K))
        {
            direction.Normalize();
            PlayerBody.AddForce(direction * MoveForce*0.2f,ForceMode2D.Impulse);
        }

        if (direction.magnitude > 0.1f)
        {
            UpdateWeaponAngle();
            playerAnimator.SetBool("isMooving", true);

            if (m_coroutine == null)
            {
                m_coroutine = StartCoroutine(PlayFootStep());
            }

            direction.Normalize();
            PlayerBody.AddForce(direction * MoveForce);
        }
        else
        {            
            playerAnimator.SetBool("isMooving", false);

            if (m_coroutine != null)
            {
                StopCoroutine(m_coroutine);
                m_coroutine = null;
            }

        }
    }
    private void UpdateUI()
    {

        manaBar.SetMana(Mana);

        Gold = playerData.Gold;
        GoldText.text = Gold.ToString();

        if (isTouched == 0 && Armor < armorBar.slider.maxValue)
        {
            Armor++;
            armorBar.SetArmor(Armor);

            isTouched++;
            StartCoroutine(WaitBetweenRegen());
        }
    }
    public void TakeDamage(int Damage)
    {
        isTouched++;
        if (Armor-Damage >= 0)
        {
            Armor -= Damage;
            armorBar.SetArmor(Armor);
        }
        else
        {
            health -= Damage-Armor;
            healthBar.SetHealth(health);
        }

        if (Health <= 0)
        {
            Die();
        }

        StartCoroutine(WaitAndRegen());
    }
    IEnumerator WaitAndRegen()
    {
        yield return new WaitForSeconds(3);
        isTouched--;
    }
    IEnumerator WaitBetweenRegen()
    {
        yield return new WaitForSeconds(2);
        isTouched--;
    }
}
