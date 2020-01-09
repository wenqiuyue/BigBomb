using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGold : MonoBehaviour {
	//单例
	private static  SaveGold _sav;
	void Awak(){
		_sav = this;
	}
	public static SaveGold GetSav(){
		//如果_sav为空，则初始化_sav,否则直接返回_sav
		if (_sav == null) {
			GameObject obj = new GameObject ();
			_sav = obj.AddComponent<SaveGold> ();
		}
		return _sav;
	}
	KeepDaTa _save;

	//WanJia_xml wanjia = new WanJia_xml();
	KeepDaTa gold = new KeepDaTa ();
	const string _SAVE_NAME = "Save.xml";

	//初始化存档文件
	public void InitSaveData(){
		_save=new KeepDaTa();
		_save._IsUclockItem = new List<bool>();
		_save._HaveItemId = new List<string> ();
		_save._HaveItemNum = new List<int> ();
		_save._HaveItemDengji=new List<int>();
		_save._ZhuTiData = new List<int> ();
		_save._GuanQiaData = new List<int> ();


		_save._IsUclockItem.Add (false);
		_save._IsUclockItem.Add (false);
		_save._IsUclockItem.Add (false);
		_save._IsUclockItem.Add (false);
		SaveGameData ();

	}

	//获取存档文件，并初始化
	public KeepDaTa GetSaveDate (bool isInit = false){
		//强制初始化，并返回
		if(isInit){
			InitSaveData ();
			return _save;
		}
		// 如果save为null
		if (_save == null) {
			//如果save不存在，对save文件进行初始化
			if (!DataProcess.IsGameDataExist (_SAVE_NAME)) {
				Debug.Log ("不存在存档");
				InitSaveData (); 
				return _save;
			}
			//存在存档文件，读取并返回
			_save =LoadGameData();
			Debug.Log (_save._Gold);
			return _save;
		}  
		//如果存档已存在，直接返回
		return _save;

	}

	public void  SaveGameData(){
		//保存存档到文件
		DataProcess.SaveGameData(_save, typeof(KeepDaTa), _SAVE_NAME, false);
	}
	public KeepDaTa LoadGameData(){
		//读取文档
		KeepDaTa readGold = DataProcess.LoadGameData (typeof(KeepDaTa), _SAVE_NAME, false) as KeepDaTa;
		return readGold;
	}


	
	}

