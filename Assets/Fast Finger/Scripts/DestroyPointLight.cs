using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyPointLight : MonoBehaviour
{
    UnityEngine.Rendering.Universal.Light2D light2D;

    void Start()
    {
        light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    void Update()
    {
        light2D.intensity -= Time.deltaTime * 1.5f;
        if (light2D.intensity < 0) Destroy(this.gameObject);
    }
}
