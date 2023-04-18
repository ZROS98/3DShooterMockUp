using System.Collections;
using ShooterMockUp.Player;
using UnityEngine;

namespace ShooterMockUp.Envirement
{
    public class MovingPlatform : MonoBehaviour
    {
        [field: SerializeField]
        private Rigidbody CurrentRigidbody { get; set; }
        [field: SerializeField]
        public Transform StartPoint { get; set; }
        [field: SerializeField]
        public Transform EndPoint { get; set; }
        [field: SerializeField]
        public float Speed { get; set; }

        private Vector3 NextPosition { get; set; }

        protected virtual void Start ()
        {
            NextPosition = StartPoint.position;
            StartCoroutine(MovePlatform());
        }

        protected virtual void OnCollisionEnter (Collision other)
        {
            if (other.gameObject.TryGetComponent(out PlayerController playerController))
            {
                playerController.transform.SetParent(transform);
            }
        }

        protected virtual void OnCollisionExit (Collision other)
        {
            if (other.gameObject.TryGetComponent(out PlayerController playerController))
            {
                playerController.transform.SetParent(null);
            }
        }

        private IEnumerator MovePlatform ()
        {
            while (true)
            {
                MoveForward();
                MoveBackward();
                CurrentRigidbody.MovePosition(Vector3.MoveTowards(CurrentRigidbody.position, NextPosition, Speed * Time.deltaTime));

                yield return null;
            }
        }

        private void MoveForward ()
        {
            if (CurrentRigidbody.position == EndPoint.position)
            {
                NextPosition = StartPoint.position;
            }
        }

        private void MoveBackward ()
        {
            if (CurrentRigidbody.position == StartPoint.position)
            {
                NextPosition = EndPoint.position;
            }
        }
    }
}