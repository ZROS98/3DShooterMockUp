using UnityEngine;

namespace ShooterMockUp.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        [field: SerializeField]
        private WeaponSetup CurrentWeaponSetup { get; set; }
        
        public abstract void Shoot ();
    }
}