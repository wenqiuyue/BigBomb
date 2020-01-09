using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KeepDaTa{
	//金币
	public long _Gold {
		get;
		set;
	}
	//已有道具id
	public List<string> _HaveItemId;
	//已有道具个数
	public List<int> _HaveItemNum;
	//道具等级
	public List<int> _HaveItemDengji;
	//道具是否解锁
	public List<bool> _IsUclockItem;
	//主题信息 0为未解锁、1为未通过、2为通过
	public List<int> _ZhuTiData;
	//关卡信息 0为未解锁、1为未通过、2为通过
	public List<int> _GuanQiaData;


}
