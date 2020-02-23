﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ShootManager))]
public class ReplayManager : MonoBehaviour
{
	[Header("===== UI =====")]
	public GameObject			UIGroup;

	// 其他
	[Header("======= 互相溝通相關 =====")]
	private int					CurrentIndex = 0;													// 現在產生的個數
	public int					FocusIndex = -1;													// Focus 在第一個
	private List<GameObject>	UIGroupList = new List<GameObject>();								// 所有 UI 的 List
	private ShootManager		ShootM;

	private void Start()
	{
		UIGroup.SetActive(false);
		ShootM = this.GetComponent<ShootManager>();
	}

	// 其他人要產生物件時，所跑來的事件
	public void GenerateUIItem()
	{
		GameObject temp = GameObject.Instantiate(UIGroup);
		temp.SetActive(true);
		temp.transform.SetParent(UIGroup.transform.parent);

		// 拿底下所有的物件做更改
		Transform[] trans = temp.GetComponentsInChildren<Transform>(true);
		Text tempText = trans[1].gameObject.GetComponent<Text>();
		tempText.text = "n" + CurrentIndex.ToString();
		CurrentIndex++;

		// Focus 事件
		int tempIndex = CurrentIndex - 1;
		trans[0].GetComponent<Button>().onClick.AddListener(
			delegate ()
			{
				ReplayItemClick(tempIndex);
			}
		);

		// 增加播放的動作
		Button tempBtn = trans[2].gameObject.GetComponent<Button>();
		tempBtn.onClick.AddListener(
			delegate ()
			{
				ShootM.Replay(tempIndex);
			}
		);

		UIGroupList.Add(temp);

		// Focus 在最後一個
		FocusIndex = UIGroupList.Count - 1;
		ReFocusUI();
	}
	public void ReplayItemClick(int i)
	{
		FocusIndex = i;
		ReFocusUI();
	}

	//public void Save
	// Helper Function
	private void ReFocusUI()
	{
		for(int i = 0; i < UIGroupList.Count; i++)
		{
			Image temp = UIGroupList[i].GetComponent<Image>();
			if (i == FocusIndex)
			{
				temp.enabled = true;
				temp.color = new Color(0, 210.0f / 255, 1);
			}
			else
				temp.enabled = false;
		}
			
	}
}
