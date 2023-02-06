using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FastFinger
{
	//This script is used to give the player daily reward
	public class DailyGift : MonoBehaviour
	{

		[SerializeField]
		private Text giftTimer = null;
		[SerializeField]
		private GameObject giftButton = null;
		[SerializeField]
		private GameObject gift = null;
		[SerializeField]
		private Text numberOfStars = null;

		void Update()
		{
			if (PlayerPrefs.GetInt("numberOfDaysPlaying", 0) == 0)
			{
				PlayerPrefs.SetInt("numberOfDaysPlaying", 1);
				System.DateTime todaysDate = System.DateTime.Now;
				string strDate = todaysDate.ToString();
				PlayerPrefs.SetString("firstStartedPlaying", strDate);
				giftButton.SetActive(true);
			}
			else
			{
				System.DateTime datevalue1 = Convert.ToDateTime(PlayerPrefs.GetString("firstStartedPlaying", ""));
				System.DateTime datevalue2 = System.DateTime.Now;
				double hours = (datevalue2 - datevalue1).TotalHours;

				bool hasGift = false;
				while (hours / PlayerPrefs.GetInt("numberOfDaysPlaying", 1) >= 24)
				{//Checks whether is passed 24 or more hours since the player has collected the last gift
					PlayerPrefs.SetInt("numberOfDaysPlaying", (PlayerPrefs.GetInt("numberOfDaysPlaying") + 1));
					hasGift = true;//If this is true gift button will be set active and player will be able to press it to get the gift
				}

				if (hasGift)
				{
					giftButton.SetActive(true);
				}

				var ts = TimeSpan.FromHours(hours);
				string hoursToString = "" + (23 - ts.Hours);
				if ((23 - ts.Hours) < 10)
				{
					hoursToString = "0" + (23 - ts.Hours);
				}

				string minutesToString = "" + (59 - ts.Minutes);
				if ((59 - ts.Minutes) < 10)
				{
					minutesToString = "0" + (59 - ts.Minutes);
				}

				string secondToString = "" + (59 - ts.Seconds);
				if ((59 - ts.Seconds) < 10)
				{
					secondToString = "0" + (59 - ts.Seconds);
				}

				giftTimer.text = "NEXT GIFT: " + hoursToString + ":" + minutesToString + ":" + secondToString;
			}

		}

		public void GetGift()//When player clicks to get a daily gift
		{
			giftButton.SetActive(false);
			gift.SetActive(true);
			int giftReward = UnityEngine.Random.Range(10, 100);//Give the player random amount of 10-100 stars
			numberOfStars.text = "+" + giftReward;
			PlayerPrefs.SetInt("NumberOfStars", (PlayerPrefs.GetInt("NumberOfStars") + giftReward));
			GameObject.Find("ButtonSound").GetComponent<AudioSource>().Play();
			PlayerPrefs.SetInt("DailyGift", PlayerPrefs.GetInt("DailyGift") + 1);
		}
	}
}
