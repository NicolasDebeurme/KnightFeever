using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Activatable
{
    private void Awake()
    {
        if (playerData.ChestOpened)
            isAlreadyActivated = true;
    }
    private void Update()
    {
        if (_isclicked)
        {
            _isclicked = false;
            if (playerData.Gold >= 100)
            {
                activateButton.SetActive(false);
                spriteRenderer.sprite = activated;
                isAlreadyActivated=true;
                playerData.ChestOpened = true;
                Player.Instance.weapon.GetComponent<Weapon>().UpgradeWeapon();
                Player.Instance.Gold-=100;
                playerData.Gold-=100;

            }

        }
    }
}
