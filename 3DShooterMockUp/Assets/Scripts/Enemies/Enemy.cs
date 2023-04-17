using System.Collections;
using ShooterMockUp.Enemies.Data;
using UnityEngine;

namespace ShooterMockUp.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [field: SerializeField]
        private EnemySetup CurrentEnemySetup { get; set; }
        [field: SerializeField]
        private Renderer CurrentRenderer { get; set; }
        
        [field: SerializeField]
        private int HealthPoints { get; set; }
        private Coroutine ColorLerpCoroutine { get; set; }
        
        private const int HUNDRED_PERCENT = 100;
        private const int FIFTY_PERCENT = 50;

        public void HandleGettingDamage (int damagePoints)
        {
            HealthPoints -= damagePoints;
            UpdateColor();
        }

        protected virtual void Awake ()
        {
            Initialize();
        }

        private void Initialize ()
        {
            HealthPoints = CurrentEnemySetup.HealthPoints;
            CurrentRenderer.material.color = CurrentEnemySetup.HighHealthPointsLevelColor;
        }

        [ContextMenu("Start UpdateColorProcess")]
        private void UpdateColor ()
        {
            ColorLerpCoroutine = StartCoroutine(UpdateColorProcess());
        }

        private IEnumerator UpdateColorProcess ()
        {
            float elapsedTime = 0f;
            
            while (elapsedTime < CurrentEnemySetup.ColorLerpDuration)
            {
                CurrentRenderer.material.color = Color.Lerp(CurrentRenderer.material.color, GetLerpedColor(), elapsedTime / CurrentEnemySetup.ColorLerpDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            CurrentRenderer.material.color = GetLerpedColor();
            CheckIfAlive();
            StopCoroutine(ColorLerpCoroutine);
        }

        private Color GetLerpedColor ()
        {
            Color lerpedColor;
            float percentage = GetPercentageFromHealthPoints(HealthPoints);

            lerpedColor = percentage >= FIFTY_PERCENT ? Color.Lerp(CurrentEnemySetup.MediumHealthPointsLevelColor, CurrentEnemySetup.HighHealthPointsLevelColor, (percentage - FIFTY_PERCENT) / FIFTY_PERCENT) 
                : Color.Lerp(CurrentEnemySetup.LowHealthPointsLevelColor, CurrentEnemySetup.MediumHealthPointsLevelColor, percentage / FIFTY_PERCENT);

            return lerpedColor;
        }

        private float GetPercentageFromHealthPoints (float healthPoints)
        {
            float percentage = (healthPoints / CurrentEnemySetup.HealthPoints) * HUNDRED_PERCENT;
            return percentage;
        }

        private void CheckIfAlive ()
        {
            if (HealthPoints <= 0)
            {
                StopCoroutine(ColorLerpCoroutine);
                Destroy(gameObject);
            }
        }
    }
}