using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataSave1{
	public int _countmoney;//金币初始数量
	public  bool[] isItemContentUnlock = new bool[]{true, false, false, false, false };//道具栏初始值
	public int i = 1;//道具栏解锁个数
	public List<int> _itemIdList=new List<int>();//获取购买道具的值
	public List<Button> _gk = new List<Button> ();
	public  bool[] isGuanKaUnlock = new bool[]
	{   true,false,false,false,false,
	    false,false,false,false,false,
	    false,false,false,false,false,
	    false,false,false,false,false
	};//关卡是否解锁
	public  bool[] isZhuTiUnlock = new bool[]{true, true, false, false};//主题是否解锁
	public List<Pic> _itemlist =new List<Pic>();
	public float y;

	/// <summary>
	/// 创建单例
	/// </summary>
	private static DataSave1 instance;
	public static DataSave1 getInstance {
		get {
			if (instance == null) {
				instance = new DataSave1 ();
			}
			return instance;
		}
	}

}
