using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastFinger
{
    public class AreaStarsGenerator : MonoBehaviour
    {
        void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            int enabledStars = 0;
            while (enabledStars < 5)
            {
                GameObject randomStar = transform.GetChild(Random.Range(0, transform.childCount)).gameObject;
                if (!randomStar.activeSelf)
                {
                    randomStar.SetActive(true);
                    enabledStars++;
                }
            }
        }
    }
}
