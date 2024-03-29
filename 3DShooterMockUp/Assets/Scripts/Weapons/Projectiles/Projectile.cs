﻿using System.Collections;
using ShooterMockUp.Tools;
using ShooterMockUp.Utilities;
using ShooterMockUp.Weapon.Projectiles.Data;
using UnityEngine;

namespace ShooterMockUp.Weapon.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [field: Header(ProjectConstants.HEADER_REFERENCES)]
        [field: SerializeField]
        public Rigidbody CurrentRigidbody { get; set; }
        [field: SerializeField]
        public ProjectileType CurrentProjectileType { get; set; }
        [field: SerializeField]
        public ProjectileSetup CurrentProjectileSetup { get; set; }
        
        [field: Header(ProjectConstants.HEADER_SETTINGS)]
        [field: SerializeField]
        private LayerMask TargetLayers { get; set; }
        
        public ProjectilesPool CurrentProjectilesPool { get; set; }
        public int LocalDamage { get; set; }

        protected virtual void OnEnable ()
        {
            StartAutoDestroy();
        }
        
        private void StartAutoDestroy ( )
        {
            StartCoroutine(AutoDestroyProcess());
        }

        private IEnumerator AutoDestroyProcess ()
        {
            yield return new WaitForSeconds(CurrentProjectileSetup.TimeToAutoDestroy);
            CurrentProjectilesPool.ReturnObjectToPool(CurrentProjectileType, CurrentRigidbody);
            StopAllCoroutines();
        }

        private void OnCollisionEnter (Collision other)
        {
            CheckForEnemies();
            CurrentProjectilesPool.ReturnObjectToPool(CurrentProjectileType, CurrentRigidbody);
        }

        private void CheckForEnemies ()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, CurrentProjectileSetup.DamageRadius, TargetLayers);

            foreach (Collider currentCollider in colliders)
            {
                if (currentCollider.TryGetComponent(out Enemy.Enemy enemy))
                {
                    enemy.HandleGettingDamage(LocalDamage);
                }
            }
        }
    }
}