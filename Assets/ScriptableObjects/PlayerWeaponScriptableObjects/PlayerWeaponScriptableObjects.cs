using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Weapon Scriptable Object", fileName = "NewPlayerWeapon")]
public class PlayerWeaponScriptableObjects : ScriptableObject
{
    [SerializeField] private GameObject weaponPrefab;
    public GameObject WeaponPrefab => weaponPrefab;
    
    [SerializeField] private float weaponCooldown;
    public float WeaponCooldown => weaponCooldown;
    
    [SerializeField] float knockBackForce;
    public float KnockBackForce => knockBackForce;
    
    [SerializeField] private int weaponDamage;
    public int WeaponDamage => weaponDamage;
    
    [SerializeField] private float weaponRange;
    public float WeaponRange => weaponRange;
}
