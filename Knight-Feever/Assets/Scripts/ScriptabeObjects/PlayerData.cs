using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Data", order = 1)]
public class PlayerData : ScriptableObject
{
    public bool ChestOpened=false;
    public int Actualweapontype = 0;

    public bool inRoom = false;

    public int Gold=0;
}
