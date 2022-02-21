using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject weapon;
    public float MoveForce = 20f;
    public Rigidbody2D PlayerBody;
    public Animator playerAnimator;
    public SpriteRenderer sprite;
    [HideInInspector] public Vector2 direction;
    [HideInInspector] public Vector2 weaponDirection;
    [HideInInspector] public Coroutine m_coroutine;
    [HideInInspector] public Ennemy closestEnnemy = null;
    Vector2 directionEnnemy;

    public Animator gameUIAnimator;
    


    private void Update() {
        
    }
    
    public IEnumerator PlayFootStep()
    {
        while (enabled)
        {
            AudioManager.Instance.PlaySound("footStep");
            yield return new WaitForSeconds(0.3f);
        }
    }
    public void UpdateWeaponAngle()
    {
        if (closestEnnemy != null)
        {
            directionEnnemy = closestEnnemy.transform.position - this.transform.position;
            float weaponRotation = Mathf.Atan2(directionEnnemy.y, directionEnnemy.x) * Mathf.Rad2Deg;
            
            if(directionEnnemy.x>0)
            {
                weapon.transform.rotation = Quaternion.AngleAxis(weaponRotation, Vector3.forward);
                weapon.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                weaponRotation=weaponRotation+180;
                weapon.transform.rotation = Quaternion.AngleAxis(weaponRotation, Vector3.forward);
                weapon.GetComponent<SpriteRenderer>().flipX = true;
            }
            
            sprite.flipX = directionEnnemy.x < 0; 
            directionEnnemy.Normalize();
            weaponDirection = directionEnnemy;
        }
        else
        {
            float weaponRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if(direction.x>0)
            {
                weapon.transform.rotation = Quaternion.AngleAxis(weaponRotation, Vector3.forward);
                weapon.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                weaponRotation = weaponRotation + 180;
                weapon.transform.rotation = Quaternion.AngleAxis(weaponRotation, Vector3.forward);
                weapon.GetComponent<SpriteRenderer>().flipX = true;
            }
            
            sprite.flipX = direction.x < 0;
            direction.Normalize();
            weaponDirection = direction;
        }
    }

    public void FindClosestEnemy()
    {
        Ennemy[] enemies = GameObject.FindObjectsOfType<Ennemy>();

        float distanceMin = Mathf.Infinity;

        closestEnnemy = null;

        foreach (Ennemy go in enemies)
        {
            float diff = (go.transform.position - this.transform.position).sqrMagnitude;
            if (diff < distanceMin)
            {
                closestEnnemy = go;
                distanceMin = diff;
            }
        }
    }

    public void Die()
    {
        playerData.inRoom = false;

        
        Camera.main.transform.parent = null;
        Destroy(this.gameObject);
        
        
        gameUIAnimator.SetTrigger("GameOver");

        AudioManager.Instance.PlayMusic("GameOver");
        LevelManager.Instance.isGameEnded =true;
    }

    public void Win()
    {
        playerData.inRoom = false;

        Camera.main.transform.parent = null;
        this.enabled=false;
        
        gameUIAnimator.SetTrigger("Win");

        AudioManager.Instance.PlayMusic("Win");

        LevelManager.Instance.isGameEnded=true;
    }
}
