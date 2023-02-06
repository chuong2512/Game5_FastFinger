using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastFinger
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;
        private float velocity = 2f;
        private Camera cam;
        private float topMenuPos;
        private Vector2 point;
        private Vector2 startPoint;
        private Menus menus;
        private float scoreTemp;

        void Start()
        {
            cam = Camera.main;
            topMenuPos = Screen.height - Screen.height / 7;
            point = Vector2.zero;
            menus = GameObject.Find("GameManager").GetComponent<Menus>();
            scoreTemp = 0;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                startPoint.x = startPoint.x - rb.position.x;
            }

            if (Input.GetMouseButton(0) && Input.mousePosition.y < topMenuPos)
            {
                point = cam.ScreenToWorldPoint(Input.mousePosition);
                point.x = -(startPoint.x - point.x);
                if (point.x > 2.8f) point.x = 2.8f;
                if (point.x < -2.8f) point.x = -2.8f;
            }
            Vars.score += Time.deltaTime;
            if(Vars.score - scoreTemp > 0.5f)
            {
                menus.UpdateTheScoreUI();
                scoreTemp++;
            }
        }

        void FixedUpdate()
        {
            rb.MovePosition(new Vector2(Mathf.Lerp(rb.position.x, point.x, 0.3f), rb.position.y + velocity * Time.fixedDeltaTime));
            velocity += Time.fixedDeltaTime / 50;
        }
    }
}
