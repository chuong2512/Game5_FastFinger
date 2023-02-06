using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeat : MonoBehaviour
{
    //This script will repeat the background infinitely
    [SerializeField]
    private GameObject background1 = null;
    [SerializeField]
    private GameObject background2 = null;
    public GameObject player;

    private Vector2 background1DefaultPosition;
    private Vector2 background2DefaultPosition;

    void Start()
    {
        background1DefaultPosition = background1.transform.position;
        background2DefaultPosition = background2.transform.position;
    }

    void Update()
    {
        if (player == null) return;
        if(player.transform.position.y > background1.transform.position.y)
        {
            float background2SpriteYSize = background2.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
            background2.transform.position = new Vector2(background1.transform.position.x, background1.transform.position.y + background2SpriteYSize);
        }

        if (player.transform.position.y > background2.transform.position.y)
        {
            float background1SpriteYSize = background1.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
            background1.transform.position = new Vector2(background2.transform.position.x, background2.transform.position.y + background1SpriteYSize);
        }
    }

    public void ResetBackgrounPosition()
    {
        background1.transform.position = background1DefaultPosition;
        background2.transform.position = background2DefaultPosition;
    }
}
