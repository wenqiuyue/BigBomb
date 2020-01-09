using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStyl{
	public MapStyl(){}

	//主题
	private int zhuTi;
	public int ZhuTi{
		get{return zhuTi; }
		set{zhuTi = value; }
	}

	//关卡
	private int guanQia;
	public int GuanQia{
		get{return guanQia; }
		set{guanQia = value; }
	}

	//敌人个数
	private int dNumber;
	public int DNumber{
		get{return dNumber; }
		set{dNumber = value; }
	}

	//地图
	private string map;
	public string Map{
		get{return map;}
		set{map = value; }

	}

	//坐标对应的道具组
	private string itemGroup;
	public string ItemGroup{
		get{return itemGroup; }
		set{itemGroup = value; }
	}
}
public class Map_Xml : MonoBehaviour {



	//定义字典，将数据存入字典
	Dictionary<string,MapStyl> dic=new Dictionary<string, MapStyl>();

	// Use this for initialization
	void Start () {
		// 读取xml
		string xmlName = "Map";

		XmlDocument xmlDoc;
		DataProcess.GetXmlData(xmlName + ".xml", out xmlDoc, false);

		// 读取所有结点
		XmlNodeList childList = xmlDoc.SelectSingleNode("map").ChildNodes;
	
		// 读取数据
		MapStyl mapstyl;
		for(short i = 0; i < childList.Count; i++){
			XmlElement child = (XmlElement)childList[i];
			mapstyl = new MapStyl ();
			mapstyl.ZhuTi = int.Parse (child.GetAttribute ("zhu_ti"));
			mapstyl.GuanQia = int.Parse (child.GetAttribute ("GuanQia"));
			mapstyl.DNumber = int.Parse (child.GetAttribute ("diren_number"));
			mapstyl.Map = child.GetAttribute ("map");
			mapstyl.ItemGroup = child.GetAttribute ("ItemGroup");
			dic.Add (mapstyl.ZhuTi + "_" + mapstyl.GuanQia, mapstyl);
		}

		Data._MapDic = dic;
		foreach(KeyValuePair<string,MapStyl> pair in dic){
			Debug.Log (pair.Key + " " + pair.Value.Map);
		}
		MapStyl map1;
		dic.TryGetValue ("1_3", out map1);
		//以'|'分割字符串
		char[] separator = { '|' };
		//将字符串全部单个分割
		char[] comma = { ',','|' };

		string[] array =map1.Map.Split(separator);
		string[] commaArray =map1.Map.Split(comma);

		//将分割出的字符存入二维数组
		 string [,] two = new string[array.Length,commaArray.Length/array.Length];
		int k = 0;
		for (int h = 0; h < array.Length; h++) {
			for (int l = 0; l < commaArray.Length/array.Length; l++) {

				two [h,l] = commaArray [k];
				k++;	
				Debug.Log (two [h, l]);
			}
				
		}

		Debug.Log (two [2, 1]);

		return;
		int[] ia = new int[]{1, 2, 3, 4, 5};
		string sa = "";
		for (int i=0;i<ia.Length;i++){
			sa = sa + ia [i].ToString() + " ";

		}
		Debug.Log (sa);
	}
}

