using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastFinger
{
    public class CameraSize : MonoBehaviour
    {
        void Start()
        {
            float screenAspectRatio = (float)Screen.width / (float)Screen.height;
            float orthographicSize = (float)(6 - (screenAspectRatio - 0.485f) * 11f);
            if (orthographicSize < 4) orthographicSize = 4;
            Camera.main.orthographicSize = orthographicSize;
        }
    }
}