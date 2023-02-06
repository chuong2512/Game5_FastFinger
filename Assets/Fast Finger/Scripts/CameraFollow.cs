using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastFinger
{
    public class CameraFollow : MonoBehaviour
    {
        public GameObject player;

        void LateUpdate()
        {
            if (player == null) return;
            transform.position = new Vector3(0, player.transform.position.y + 3, -10);
        }
    }
}
