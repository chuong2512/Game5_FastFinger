using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastFinger
{
    public class RotateTheObstacle : MonoBehaviour
    {
        [SerializeField]
        private float rotationSpeed = 0;

        void Start()
        {
            if (rotationSpeed == 0)
                rotationSpeed = Random.Range(50, 100);
        }
        void Update()
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + rotationSpeed * Time.deltaTime);
        }
    }
}
