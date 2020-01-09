using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {
	[SerializeField]
	Transform _canvas;
	/// <summary>
	/// Count类及构造函数
	/// </summary>
	public class Count{
		public int minimum;
		public int maximum;
		//Count的构造方法
		public Count(int min , int max){
			minimum = min;
			maximum = max;
		}
	}
	Map _map;
	//定义行列
	private  int columns = 9;
	private  int rows = 14;
	//定义雪堆的最大数量及最小数量
	private Count xueduiCount = new Count (20,30);
	//障碍物的最大数量及最小数量
	private Count zhangaiwuCount = new Count (5, 10);
	//雪堆组
	public GameObject[] xueduis;
	//障碍物组
	public GameObject[] zhangaiwu;
	//定义list存取位置
	private  List <Vector2> gridpositions = new List<Vector2> ();
	/// <summary>
	/// 将行列值转化为位置传在list中
	/// </summary>
	void InitialiseList(){
		_map = GameObject.FindObjectOfType<Map> ();
		gridpositions.Clear ();
		for (int x = 1; x <columns - 1; x++) {
			for (int y = 2; y < rows - 1; y++) {
				gridpositions.Add (_map.GetGridPos(x,y));
				//Debug.Log (_map.GetGridPos (x, y));
				//gridpositions.Add (new Vector3 (y, x, 0f));
			}
		}
	}
	/// <summary>
	/// 预制体随机生成的位置范围
	/// </summary>
	Vector2 RandomPosition(){
		int randomIndex = Random.Range (0, gridpositions.Count);
		Vector2 randomPosition	= gridpositions [randomIndex];
		gridpositions.RemoveAt (randomIndex);
		return randomPosition;
	}
	/// <summary>
	/// 生成预制物体及其数量的范围
	/// </summary>
	void LayoutObjectAtRandom(GameObject[] tileArray,int min,int max){
		int objectCount = Random.Range (min, max + 1);
		for (int i = 0; i < objectCount; i++) {
			Vector2 randomPosition = RandomPosition ();
			GameObject tileChoice = tileArray [Random.Range (0, tileArray.Length)];
			Instantiate (tileChoice, randomPosition, Quaternion.identity);
		}
	}
	/// <summary>
	/// 对预制物体进行实例化
	/// </summary>

	public void SetUpScene (int level){
		InitialiseList ();
		LayoutObjectAtRandom (xueduis, xueduiCount.minimum, xueduiCount.maximum);
		LayoutObjectAtRandom (zhangaiwu, zhangaiwuCount.minimum, zhangaiwuCount.maximum);
	}

}
