using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastFinger
{
    public class ObstacleLeftRightMovement : MonoBehaviour
    {
        [SerializeField]
        private float startPosition;
        [SerializeField]
        private float endPosition;
        private bool shouldMoveLeft = false;
        [SerializeField]
        private float speed;
        void Start()
        {
            if (speed == 0)
            {
                speed = Random.Range(2, 5);
            }
        }

        void Update()
        {
            if (shouldMoveLeft)
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
                if (transform.position.x < startPosition)
                {
                    shouldMoveLeft = false;
                }
            }
            else
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
                if (transform.position.x > endPosition)
                {
                    shouldMoveLeft = true;
                }
            }

        }
    }
}
