using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseChampion : MonoBehaviour
{
    public GameObject PlayerUI;
    private void Update() {

        if(PlayerUI.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
