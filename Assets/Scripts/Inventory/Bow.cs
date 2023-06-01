using UnityEngine;
using Weapons;
namespace Inventory
{
    public class Bow : MonoBehaviour, IWeapon
    {
        [SerializeField] private PlayerWeaponScriptableObjects weaponInfo;
        
        public void Attack()
        {
            Debug.Log("Bow Attack");
        }
        
        public PlayerWeaponScriptableObjects GetWeaponInfo()
        {
            return weaponInfo;
        }

    }
}

