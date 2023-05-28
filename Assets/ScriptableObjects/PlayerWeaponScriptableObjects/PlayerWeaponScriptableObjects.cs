using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Weapon Scriptable Object", fileName = "NewPlayerWeapon")]
public class PlayerWeaponScriptableObjects : ScriptableObject
{
    [SerializeField] float knockBackForce;
    public float KnockBackForce => knockBackForce;
    [SerializeField] private int weaponDamage;
    public int WeaponDamage => weaponDamage;
}
