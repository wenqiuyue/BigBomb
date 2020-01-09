using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy{
	public Enemy(){}

	//敌人id
	private string id;
	public string ID{
		get{return id; }
		set{id = value; }
	}

	//敌人个数
	private int number;
	public int Number{
		get{return number; }
		set{number = value; }
	}

	//敌人炸弹初始威力
	private int power;
	public int Power{
		get{return power; }
		set{power = value; }
	}

	//敌人初始生命值
	private int life;
	public int Life{
		get{return life; }
		set{life = value; }
	}

	//敌人初始速度
	private int speed;
	public int Speed{
		get{return speed; }
		set{speed = value; }
	}

	//敌人炸弹个数
	private int bomb;
	public int Bomb{
		get{return bomb; }
		set{bomb = value; }
	}

	//敌人反应速度
	private int reaction;
	public int Reaction{
		get{return reaction; }
		set{reaction = value; }
	}

	//敌人ItemGroup
	private string props;
	public string Props{
		get{return props; }
		set{props = value; }
	}
}
public class DiRen_xml : MonoBehaviour {

	Dictionary<string,Enemy> dic=new Dictionary<string, Enemy>();
	// Use this for initialization
	void Start () {
		// 读取xml
		string xmlName = "DiRen";

		XmlDocument xmlDoc;
		DataProcess.GetXmlData(xmlName + ".xml", out xmlDoc, false);

		// 读取所有结点
		XmlNodeList childList = xmlDoc.SelectSingleNode(xmlName).ChildNodes;

		Enemy enemy = new Enemy ();
		// 读取数据
		for(short i = 0; i < childList.Count; i++){
			XmlElement child = (XmlElement)childList[i];

			//Debug.LogFormat ("Id:{0} number:{1} initial_power:{2} nitial_life:{3} initial_speed:{4} bomb_number:{5} reaction_speed:{6} carry_props:{7}", child.GetAttribute("Id"), child.GetAttribute("number"), child.GetAttribute("initial_power"), child.GetAttribute("nitial_life"), child.GetAttribute("initial_speed"), child.GetAttribute("bomb_number"), child.GetAttribute("reaction_speed"), child.GetAttribute("carry_props"));
			enemy.ID = child.GetAttribute ("Id");
			enemy.Number = int.Parse (child.GetAttribute ("number"));
			enemy.Power = int.Parse (child.GetAttribute ("initial_power"));
			enemy.Life = int.Parse (child.GetAttribute ("nitial_life"));
			enemy.Speed = int.Parse (child.GetAttribute ("initial_speed"));
			enemy.Bomb = int.Parse (child.GetAttribute ("bomb_number"));
			enemy.Reaction = int.Parse (child.GetAttribute ("reaction_speed"));
			enemy.Props = child.GetAttribute ("carry_props");

			dic.Add (enemy.ID, enemy);
		}
		foreach (KeyValuePair<string,Enemy> pair in dic) {
			Debug.Log (pair.Key + "" + pair.Value);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
