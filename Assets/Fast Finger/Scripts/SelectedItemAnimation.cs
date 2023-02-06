using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastFinger
{
	public class SelectedItemAnimation : MonoBehaviour
	{
		//This script will create a simple zoom in - zoom out animation on the object that is selected in the shop menu
		float scale = 1;
		bool up = true;

		void OnDisable()
		{
			scale = 1;
			GetComponent<RectTransform>().localScale = new Vector2(1, 1);
		}

		void Update()
		{
			if (up)
			{
				scale += Time.deltaTime / 5;
				if (scale >= 1.1f)
				{
					up = false;
				}
			}
			else
			{
				scale -= Time.deltaTime / 5;
				if (scale <= 1f)
				{
					up = true;
				}
			}
			GetComponent<RectTransform>().localScale = new Vector2(scale, scale);
		}
	}
}
