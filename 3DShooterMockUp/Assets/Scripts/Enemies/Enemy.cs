using System;
using ShooterMockUp.Enemies.Data;
using UnityEngine;

namespace ShooterMockUp.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [field: SerializeField]
        private EnemySetup CurrentEnemySetup { get; set; }
        
        public int HealthPoints { get; set; }

        public void HandleGettingDamage (int damagePoints)
        {
            HealthPoints -= damagePoints;
        }
        
        protected virtual void Awake ()
        {
            Initialize();
        }

        private void Initialize ()
        {
            HealthPoints = CurrentEnemySetup.HealthPoints;
        }
        
    }
}