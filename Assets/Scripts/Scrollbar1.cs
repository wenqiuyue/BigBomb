using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrollbar1 : MonoBehaviour {
	public RectTransform _obj;
	public Scrollbar _s;
	void Update () {
		KeepData saveData = SaveData._Sav.GetSaveData ();
		if (Input.GetAxis ("Mouse ScrollWheel") != 0) 
		{
			_s.value = _s.value-(Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime*80);
		}
	}

	public void ChangeTranslateDJ() {
		KeepData saveData = SaveData._Sav.GetSaveData ();
		_obj.anchoredPosition = new Vector2(-17,-saveData._Slidery*_s.value);
		SaveData._Sav.SaveGameData ();
	}
	public void ChangeTranslateST() {
		_obj.anchoredPosition = new Vector2(-44,33+_s.value*500);
	}

}
