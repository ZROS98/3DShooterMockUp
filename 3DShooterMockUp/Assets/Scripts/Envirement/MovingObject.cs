using System.Collections;
using UnityEngine;

namespace ShooterMockUp.Envirement
{
    public class MovingObject : MonoBehaviour
    {
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

        private IEnumerator MovePlatform ()
        {
            while (true)
            {
                MoveForward();
                MoveBackward();
                transform.position = Vector3.MoveTowards(transform.position, NextPosition, Speed * Time.deltaTime);

                yield return null;
            }
        }

        private void MoveForward ()
        {
            if (transform.position == EndPoint.position)
            {
                NextPosition = StartPoint.position;
            }
        }

        private void MoveBackward ()
        {
            if (transform.position == StartPoint.position)
            {
                NextPosition = EndPoint.position;
            }
        }
    }
}