using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastFinger
{
    public class PlayerCollision : MonoBehaviour
    {
        private Menus menus;
        private AudioSource starCollectSound;

        void Start()
        {
            menus = GameObject.Find("GameManager").GetComponent<Menus>();
            starCollectSound = GameObject.Find("StarCollectSound").GetComponent<AudioSource>();
        }
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.name.Equals("StartOfTheArena")) return;
            if (col.gameObject.name.Equals("Star")) //If player collects the star
            {
                PlayerPrefs.SetInt("TotalNumberOfStars", (PlayerPrefs.GetInt("TotalNumberOfStars") + 1));
                PlayerPrefs.SetInt("NumberOfStars", (PlayerPrefs.GetInt("NumberOfStars") + 1));
                GameObject starExplosionParticle = col.gameObject.transform.Find("StarParticle").gameObject;
                starExplosionParticle.transform.parent = null;
                starExplosionParticle.SetActive(true);
                starCollectSound.Play();
                if (PlayerPrefs.GetInt("Vibration") == 1)
                {
                    Handheld.Vibrate();
                }
                Destroy(col.gameObject);
                return;
            }
            else
            {
                GameObject explosionParticle = transform.Find("ExplosionParticle").gameObject;
                explosionParticle.transform.parent = null;
                explosionParticle.SetActive(true);
                ParticleSystem.MainModule main = explosionParticle.GetComponent<ParticleSystem>().main;
                main.startColor = new ParticleSystem.MinMaxGradient(GetComponent<SpriteRenderer>().color);

                GameObject trailEffect = transform.Find("TrailEffect").gameObject;
                trailEffect.transform.parent = null;
                trailEffect.SetActive(true);
                trailEffect.GetComponent<DestroyExplosionParticle>().enabled = true;

                PlayerPrefs.SetInt("LastScore", (int)Vars.score);
                if (Vars.score > PlayerPrefs.GetInt("BestScore"))
                {
                    PlayerPrefs.SetInt("BestScore", (int)Vars.score);
                }

                GameObject.Find("GameManager").GetComponent<Menus>().GameOver();
                GameObject.Find("ExplosionSound").GetComponent<AudioSource>().Play();
                transform.Find("PointLight2D").GetComponent<DestroyPointLight>().enabled = true;
                transform.Find("PointLight2D").transform.parent = null;
                Destroy(this.gameObject);
            }
        }
    }
}
