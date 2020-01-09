using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreList : MonoBehaviour {
	
	public List<Text> _storelist;
	public List<Button> _buy;
	public Text _mymoney;
	public int j=0;
	void Start()
	{     
		for (j= 0; j < _buy.Count;j++) {
			Onclick (j);

		}
		    
	
	}
	public void Onclick(int j)
	{     KeepData saveData = SaveData._Sav.GetSaveData ();
		   _buy [j].onClick.AddListener (delegate() {
			if (int.Parse (_storelist [j].text) <= saveData._Gold) {
				saveData._Gold = saveData._Gold - int.Parse (_storelist [j].text);
				saveData._HaveItemId.Add(j);
			}
			_mymoney.text = saveData._Gold.ToString ();
			SaveData._Sav.SaveGameData();
		});

	}




}
