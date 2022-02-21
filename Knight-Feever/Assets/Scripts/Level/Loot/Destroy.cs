using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    public enum TypeofLoot{Gold, Mana};

    public TypeofLoot typeofLoot;
    public GameObject parent;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Destroy(parent);

            if(typeofLoot== TypeofLoot.Gold)
            {
                playerData.Gold++;
                Player.Instance.Gold++;
            }
                
            if(typeofLoot== TypeofLoot.Mana)
                Player.Instance.Mana++;
        }
    }
}
