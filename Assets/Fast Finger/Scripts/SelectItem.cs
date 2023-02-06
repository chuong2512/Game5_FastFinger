using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace FastFinger
{
	public class SelectItem : MonoBehaviour
	{

		void Start()
		{
			Initialize();
		}
		void OnEnable() //This is called when player enters the shop menu
		{
			Initialize();
		}

		private void Initialize()
		{
			PlayerPrefs.SetInt("Item1", 1);//This is used to unlock the first item
			DeselectAllItems();//Deselect all shop menu items

			if (PlayerPrefs.GetInt("ChoosenItem", 0) == 0)//If player hasn't choose any item
			{
				PlayerPrefs.SetInt("ChoosenItem", 1);
			}

			GameObject chosenItem = GameObject.Find("" + PlayerPrefs.GetInt("ChoosenItem", 0));
			Color itemColor = chosenItem.GetComponent<Image>().color;
			chosenItem.GetComponent<Image>().color = new Color(itemColor.r, itemColor.g, itemColor.b, 1);
			chosenItem.GetComponent<SelectedItemAnimation>().enabled = true;
			PlayerPrefs.SetString("ItemColor" + PlayerPrefs.GetInt("ChoosenItem", 0), ColorUtility.ToHtmlStringRGB(itemColor));
			GameObject.Find("ShopMenuNumberOfStarsText").GetComponent<Text>().text = "" + PlayerPrefs.GetInt("NumberOfStars", 0);
		}
		public void Select()
		{//This is called when player clicks on any item inside the shop menu
			string itemNumber = EventSystem.current.currentSelectedGameObject.name;//Used to identify on which item player clicked

			if (PlayerPrefs.GetInt("Item" + itemNumber, 0) == 1)
			{//Checks if the user ownes selected item
				SelectChosenItem(itemNumber);
			}
			else
			{//If item isn't unlocked 
				PurchaseANewItem(itemNumber);
			}
		}

		private void SelectChosenItem(string itemNumber)
		{
			PlayerPrefs.SetInt("ChoosenItem", Int32.Parse(itemNumber));
			DeselectAllItems();
			Color itemColor = GameObject.Find(itemNumber).GetComponent<Image>().color;
			GameObject.Find(itemNumber).GetComponent<Image>().color = new Color(itemColor.r, itemColor.g, itemColor.b, 1);
			GameObject.Find(itemNumber).GetComponent<SelectedItemAnimation>().enabled = true;
			PlayerPrefs.SetString("ItemColor" + itemNumber, ColorUtility.ToHtmlStringRGB(itemColor));
			GameObject.Find("ButtonSound").GetComponent<AudioSource>().Play();
		}

		private void PurchaseANewItem(string itemNumber)
		{
			string price = GameObject.Find("Price" + itemNumber).transform.Find("Text").GetComponent<Text>().text;
			if (PlayerPrefs.GetInt("NumberOfStars", 0) >= Int32.Parse(price))
			{//Check whether user has enough stars to unlock chosen item
			 //Purchase the item
				PlayerPrefs.SetInt("NumberOfStars", PlayerPrefs.GetInt("NumberOfStars", 0) - Int32.Parse(price));
				GameObject.Find("ShopMenuNumberOfStarsText").GetComponent<Text>().text = "" + PlayerPrefs.GetInt("NumberOfStars", 0);
				PlayerPrefs.SetInt("Item" + itemNumber, 1);
				PlayerPrefs.SetInt("ChoosenItem", Int32.Parse(itemNumber));
				GameObject.Find("" + itemNumber).GetComponent<UnlockItem>().Unlock();
				PlayerPrefs.SetInt("ItemsOwned", PlayerPrefs.GetInt("ItemsOwned") + 1);

				//Deselect all items
				DeselectAllItems();

				//Select the item that the player just purchased
				Color itemColor = GameObject.Find(itemNumber).GetComponent<Image>().color;
				GameObject.Find(itemNumber).GetComponent<Image>().color = new Color(itemColor.r, itemColor.g, itemColor.b, 1);
				PlayerPrefs.SetString("ItemColor" + itemNumber, ColorUtility.ToHtmlStringRGB(itemColor));
				GameObject.Find(itemNumber).GetComponent<SelectedItemAnimation>().enabled = true;

				GameObject.Find("ShopSound").GetComponent<AudioSource>().Play();
			}
			else
			{//If user doesn't have enough stars to unlock the item, play this audio
				GameObject.Find("DeniedSound").GetComponent<AudioSource>().Play();
			}
		}

		private void DeselectAllItems()
		{
			GameObject[] items = GameObject.FindGameObjectsWithTag("ShopItem");

			for (int i = -1; i < items.Length; i++)
			{
				if (PlayerPrefs.GetInt("Item" + (i + 1), 0) == 1)
				{
					Color itemColor = items[i].GetComponent<Image>().color;
					items[i].transform.localScale = new Vector2(1, 1);
					items[i].GetComponent<Image>().color = new Color(itemColor.r, itemColor.g, itemColor.b, 0.3f);
					items[i].GetComponent<SelectedItemAnimation>().enabled = false;
				}
			}
		}
	}
}
