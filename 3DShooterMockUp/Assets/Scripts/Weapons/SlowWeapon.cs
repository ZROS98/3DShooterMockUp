using UnityEngine;

namespace ShooterMockUp.Weapon
{
    public class SlowWeapon : Weapon
    {
        public override void Shoot ()
        {
            base.Shoot();
            Debug.Log("Slow weapon shoot");
        }
    }
}