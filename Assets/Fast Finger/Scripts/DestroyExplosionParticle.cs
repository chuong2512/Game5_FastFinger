using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastFinger
{
    public class DestroyExplosionParticle : MonoBehaviour
    {
        [SerializeField]
        private float time = 0.5f;
        void Start()
        {
            if (time == 0) time = 0.5f;
            Destroy(this.gameObject, time);
        }
    }
}
