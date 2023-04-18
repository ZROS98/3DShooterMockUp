using UnityEngine;

namespace ShooterMockUp.Weapon
{
    public class FastWeapon : Weapon
    {
        public override void Shoot ()
        {
            base.Shoot();
            Debug.Log("Fast weapon shoot");
        }
        
    }
}