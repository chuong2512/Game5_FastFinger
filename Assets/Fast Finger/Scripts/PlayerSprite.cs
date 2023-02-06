using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastFinger
{
	public class PlayerSprite : MonoBehaviour
	{
		//This script will change the color and the sprite on the ball game objects the ball that player has chosen in the shop menu
		//This script is attached to the "Ball" game object
		public Sprite[] sprite;
		[SerializeField]
		private TrailRenderer trail;

		void Start()
		{
			LoadPlayerTexture();
		}

		public void LoadPlayerTexture()
		{
			SpriteRenderer sr;
			if (PlayerPrefs.GetInt("ChoosenItem", 0) == 0)
			{
				sr = GetComponent<SpriteRenderer>();
				sr.sprite = sprite[0];
				sr.color = new Color(1, 0.8970644f, 0, 1);
			}
			else
			{
				int choosenItem = (PlayerPrefs.GetInt("ChoosenItem", 0) - 1);
				sr = GetComponent<SpriteRenderer>();
				sr.sprite = sprite[choosenItem];
				Color color;
				ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("ItemColor" + (choosenItem + 1)), out color);
				sr.color = color;
			}
			trail.startColor = new Color(sr.color.r, sr.color.g, sr.color.b, 1);//Change the color of the trail effect
			trail.endColor = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
		}
	}
}
