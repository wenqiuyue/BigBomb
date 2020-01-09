using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DaoJu{
	public DaoJu(){}

	//道具id
	private string id;
	public string ID{
		get{return id; }
		set{id = value; }
	}

	//道具有效时间
	private int effectiveValue;
	public int EffectiveValue{
		get{return effectiveValue; }
		set{effectiveValue = value; }
	}
	//道具具体名字
	private string _itemName;
	public string _ItemName{
		get{return _itemName; }
		set{_itemName = value; }
	}
} 
public class DaoJu_xml : MonoBehaviour {
	//定义一个字典，将数据存入字典
	Dictionary<string,DaoJu> dic=new Dictionary<string, DaoJu>();
	// Use this for initialization
	void Start () {
		// 读取xml
		string xmlName = "DaoJu";

		XmlDocument xmlDoc;
		DataProcess.GetXmlData(xmlName + ".xml", out xmlDoc, false);

		// 读取所有结点
		XmlNodeList childList = xmlDoc.SelectSingleNode(xmlName).ChildNodes;

		// 读取数据
		for(short i = 0; i < childList.Count; i++){
			XmlElement child = (XmlElement)childList[i];
			DaoJu daoju = new DaoJu ();
			//Debug.LogFormat ("Id:{0} effective_value:{1}", child.GetAttribute("Id"), child.GetAttribute("effective_value"));
			daoju.ID = child.GetAttribute ("Id");
			daoju.EffectiveValue =int.Parse(child.GetAttribute ("effective_value"));
			daoju._ItemName = child.GetAttribute ("ItemName");

	//		Debug.Log (daoju._ItemName);

			//存储数据
			dic.Add(daoju.ID,daoju);

		}

		Data._DaoJuDic = dic;
		foreach (KeyValuePair<string,DaoJu> pair in dic) {
			Debug.Log (pair.Key + " " + pair.Value);

		}
	}
}
