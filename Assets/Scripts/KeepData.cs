using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KeepData{
	private int _gold=0;
	private int _skill;
	private float _slidery;
	private int _isUclockItemNum=1;
	private int _djnum=0;
	//装备道具个数
	public  int _Djnum {
		get{ return _djnum;}
		set{ _djnum = value;}
	}
	//拥有技能点个数
	public  int _Skill {
		get{ return _skill;}
		set{ _skill = value;}
	}
	//背包拖动条距离
	public float _Slidery{
		get{return _slidery;}
		set{_slidery = value;}
	}
	//拥有金币个数
	public  int _Gold {
		get{ return _gold;}
		set{ _gold = value;}
	}
	//已解锁道具个数
	public int _IsUclockItemNum {
		get{ return _isUclockItemNum; }
		set{ _isUclockItemNum = value; }
	}
	//技能点是否解锁
	public List<TestList> _IsUclockSkill;
	//已有道具id
	public List<string> _HaveItemName;
	//已有道具个数
	public List<int> _HaveItemId;
	//道具栏上是否有道具 -1为无道具，其他为有道具
	public List<int> _HaveItemOn;
	public List<int> _ItemId;
	//道具是否解锁
	public List<bool> _IsUclockItem;
	//主题信息 true为已解锁,false为未解锁
	public List<bool> _ZhuTiData;
	//关卡信息 true为已解锁,false为未解锁
	public List<bool> _GuanQiaData;



}
