using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FastFinger
{
    public class MenuFadeInFadeOutAnimation : MonoBehaviour
    {
        //Used to make a fade in - fade out animation when user replays the game or exits to the main menu
        public Image transitionImage;
        private bool up = true;
        private float alpha = 0;
        public int menu = 0;

        void OnEnable()
        {
            alpha = 0;
            transitionImage.raycastTarget = true;
        }

        void OnDisable()
        {
            transitionImage.raycastTarget = false;
        }

        void Update()
        {
            if (up)
            {
                alpha += Time.deltaTime * 3;
                if (alpha >= 1f)
                {

                    up = false;
                    if (menu == 0) //Checks which menu should be shown after the fade in-fade out effect
                    {
                        GetComponent<Menus>().StartTheGame();
                    }
                    else if (menu == 1)
                    {
                        GetComponent<Menus>().BackToTheMainMenu();
                    }
                    else if (menu == 2)
                    {
                        GetComponent<Menus>().Replay();
                    }

                }
            }
            else
            {
                alpha -= Time.deltaTime * 3f;
                if (alpha <= 0)
                {
                    up = true;
                    alpha = 0;
                    transitionImage.color = new Color(0, 0, 0, 0);
                    GetComponent<MenuFadeInFadeOutAnimation>().enabled = false;
                }
            }
            transitionImage.color = new Color(0, 0, 0, alpha);
        }
    }
}
