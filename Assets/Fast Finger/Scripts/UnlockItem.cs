using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FastFinger
{
	public class UnlockItem : MonoBehaviour
	{

		//Attached on the each item in the shop menu and it is used to check whether the item is unlock
		public Sprite itemImage;
		public Color itemColor;

		void Awake()
		{
			Unlock();
		}

		public void Unlock()
		{
			string itemNumber = this.gameObject.name;
			if (PlayerPrefs.GetInt("Item" + itemNumber, 0) == 1)
			{
				GameObject itemPrice = GameObject.Find("Price" + itemNumber);
				if (itemPrice != null)
					itemPrice.SetActive(false);
				GetComponent<Image>().sprite = itemImage;
				GetComponent<Image>().color = itemColor;
			}
		}
	}
}
