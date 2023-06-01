using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private PlayerWeaponScriptableObjects weaponScriptableObjects;
    public PlayerWeaponScriptableObjects GetWeaponInfo => weaponScriptableObjects;
    
}
