using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour {
	
	//单例
	private static  SaveData _sav;
	public static SaveData _Sav{get{ return _sav; }}
	#region 生成道具个数
	TestList t0=new TestList();
	TestList t1=new TestList();
	TestList t2=new TestList();
	TestList t3=new TestList();
	TestList t4=new TestList();
	TestList t5=new TestList();
	TestList t6=new TestList();
	TestList t7=new TestList();
	TestList t8=new TestList();
	TestList t9=new TestList();
	TestList t10=new TestList();
	TestList t11=new TestList();
	TestList t12=new TestList();
	TestList t13=new TestList();
	TestList t14=new TestList();
	#endregion

	void Awake(){
		_sav = this;
	}
	public static SaveData GetSav(){
		//如果_sav为空，则初始化_sav,否则直接返回_sav
		if (_sav == null) {
			GameObject obj = new GameObject ();
			_sav = obj.AddComponent<SaveData> ();
		}
		return _sav;
	}
	KeepData _save;

	//WanJia_xml wanjia = new WanJia_xml();
	//KeepData gold = new KeepData ();
	const string _SAVE_NAME = "Save.xml";

	//初始化存档文件
	public void InitSaveData(){
		_save=new KeepData();
		_save._IsUclockItem = new List<bool>();
		_save._HaveItemName = new List<string> ();
		_save._HaveItemId = new List<int> ();
		_save._HaveItemOn=new List<int>(){10,11,11,11,11};
		_save._Slidery = new float ();
		_save._Skill = new int (); 
		_save._ItemId = new List<int> ();
		#region 技能点解锁
		t0._a = new List<bool> (){ false, false, false, false, false };
		t1._a = new List<bool> (){ false, false, false, false, false };
		t2._a = new List<bool> (){ false, false, false, false, false };
		t3._a = new List<bool> (){ false, false, false, false, false };
		t4._a = new List<bool> (){ false, false, false, false, false };
		t5._a = new List<bool> (){ false, false, false, false, false };
		t6._a = new List<bool> (){ false, false, false, false, false };
		t7._a = new List<bool> (){ false, false, false, false, false };
		t8._a = new List<bool> (){ false, false, false, false, false };
		t9._a = new List<bool> (){ false, false, false, false, false };
		t10._a = new List<bool> (){ false, false, false, false, false };
		t11._a = new List<bool> (){ false, false, false, false, false };
		t12._a = new List<bool> (){ false, false, false, false, false };
		t13._a = new List<bool> (){ false, false, false, false, false };
		t14._a = new List<bool> (){ false, false, false, false, false };
		_save._IsUclockSkill = new List<TestList> (){t0,t1,t2,t3,t4,t5,t6,t7,t8,t9,t10,t11,t12,t13,t14};
		#endregion
		#region 主题解锁
		_save._ZhuTiData = new List<bool>{true, false, false, false};
		#endregion
		#region 关卡解锁
		_save._GuanQiaData =new List<bool>
		{   true,false,false,false,false,
			false,false,false,false,false,
			false,false,false,false,false,
			false,false,false,false,false
		};
		#endregion
		#region 道具栏解锁
		_save._IsUclockItem=new List<bool>(){true,false,false,false ,false};
		#endregion
		#region 道具名
		_save._HaveItemName.Add ("慢慢胶");
		_save._HaveItemName.Add ("飞镖");
		_save._HaveItemName.Add ("陷阱");
		_save._HaveItemName.Add ("推手");
		_save._HaveItemName.Add ("幽灵");
		_save._HaveItemName.Add ("透视镜");
		_save._HaveItemName.Add ("保护罩");
		_save._HaveItemName.Add ("超级泡泡");
		_save._HaveItemName.Add ("寒冰");
		_save._HaveItemName.Add ("磁铁");
		_save._HaveItemName.Add ("鞋子");
		_save._HaveItemName.Add ("炸弹");
		_save._HaveItemName.Add ("威力");
		_save._HaveItemName.Add ("生命");
		SaveGameData ();
		#endregion

	}

	//获取存档文件，并初始化
	public KeepData GetSaveData (bool isInit = false){
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
		DataProcess.SaveGameData(_save, typeof(KeepData), _SAVE_NAME, false);
	}
	public KeepData LoadGameData(){
		//读取文档
		KeepData readGold = DataProcess.LoadGameData (typeof(KeepData), _SAVE_NAME, false) as KeepData;
		return readGold;
	}


	
	}

