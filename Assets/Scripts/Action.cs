using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Action : MonoBehaviour {
	public Text _mymoney;
	public Text _jsMoney;
	public Text _getmoney;
	public Text _kills;
	public Text _killboss;
	 int _getmoney1;//每局金币获得数量
	 int _kills1;//每局击杀小怪数量
	int _killboss1;//每局击杀Boss数量
	void Start()
	{   
		_getmoney1 = 0;
        _kills1=0;
	    _killboss1=0;
	}
	void OnTriggerEnter2D(Collider2D coll)
	{
		KeepData saveData = SaveData._Sav.GetSaveData ();
		if (coll.gameObject.tag == "coin") 
		{   
			saveData._Gold++;
			_getmoney1++;
			SaveData._Sav.SaveGameData ();
			_mymoney.text = saveData._Gold.ToString ();
			_getmoney.text = _getmoney1.ToString ();
		}
		_mymoney.text =saveData._Gold.ToString ();
		SaveData._Sav.SaveGameData ();
	}



}
