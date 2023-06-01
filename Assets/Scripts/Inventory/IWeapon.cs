namespace Inventory
{
    public interface IWeapon
    {
        public void Attack();
        public PlayerWeaponScriptableObjects GetWeaponInfo();
    }
}