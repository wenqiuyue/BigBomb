 /*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random=UnityEngine.Random;
/// <summary>
/// 定义单个格子的行列值
/// </summary>
public class Grid{ 
	public int Rows{ get; set; }
	public int Columns{ get; set; }
	/// <summary>
	/// 创建格子的构造方法
	/// </summary>
	public Grid(int row, int column){
		Rows = row;
		Columns = column;
	}
	public static bool operator ==(Grid grid, Grid grid2){
		if (grid.Rows == grid2.Rows &&
		   grid.Columns == grid2.Columns)
			return true;
		    return false;
	}

	public static bool operator !=(Grid grid, Grid grid2){
		return !(grid == grid2);
	}
	//把Rows,Columns转化为字符串
	public override string ToString ()
	{
		return string.Format ("[Grid: Rows={0}, Columns={1}]", Rows, Columns);
	}
}
public class Map1 : MonoBehaviour {
	MapStyl maps = new MapStyl ();

	//定义行
	public  int Rows = 9;
	//定义列
	public  int Columns = 14;
	//定义格子大小
	public float size = 0.6f;
	//雪块

	public GameObject xue;

	public GameObject xue1;
	//外墙

	public GameObject wwalldm;

	public GameObject wwallleft;

	public GameObject wwallup;

	public GameObject wwalldown;

	public GameObject wwallright;

	public GameObject left;

	public GameObject right;

	public GameObject up;

	public GameObject down;
	[SerializeField]
	Transform _canvas;
	//9行13列的二维数组
	public int[,]  map = new int[9,14] {
		{ 15,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 14 }, 
		{ 5, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 9 }, 
		{ 5, 1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 9 }, 
		{ 5, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 9 }, 
		{ 5, 1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 9 }, 
		{ 5, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 9 }, 
		{ 5, 1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 9 }, 
		{ 5, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 7, 1, 9 },
		{ 13, 8,8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 12 }
	};
	/*public String[,] map1 = new string[9, 13]{ 
		{  },
		{},
		{},
		{},
		{},
		{},
		{},
		{},
		{}
		
	};*/

	// Use this for initialization
/*void Start () {
		
	for (int r = 0; r <Rows; r++) {
		for (int l = 0; l < Columns; l++) {
			//地面
			if (map [r,l] == 1) {
					GameObject obj = Instantiate (xue);
				obj.transform.position = GetGridPos(r, l);
				obj.transform.SetParent (_canvas);
			}
			if (map [r,l] == 7) {
					GameObject obj = Instantiate (xue1);
					obj.transform.position = GetGridPos(r, l);
					obj.transform.SetParent (_canvas);
			}
			//外墙
			if (map [r, l] == 5) {
					GameObject obj1 = Instantiate (wwalldm);
					obj1.transform.position = GetGridPos(r, l);
					obj1.transform.SetParent (_canvas);
					GameObject obj = Instantiate (wwallleft);
				obj.transform.position = GetGridPos(r, l);
					obj.transform.SetParent (_canvas);
			}
			if (map [r, l] == 8) {
					GameObject obj1 = Instantiate (wwalldm);
					obj1.transform.position = GetGridPos(r, l);
					obj1.transform.SetParent (_canvas);
					GameObject obj = Instantiate (wwallright);
					obj.transform.position = GetGridPos(r, l);
					obj.transform.SetParent (_canvas);
			}
			if (map [r, l] ==0) {
					GameObject obj1 = Instantiate (wwalldm);
					obj1.transform.position = GetGridPos(r, l);
					obj1.transform.SetParent (_canvas);
					GameObject obj = Instantiate (wwallup);
					obj.transform.position = GetGridPos(r, l);
					obj.transform.SetParent (_canvas);
			}
			if (map [r, l] == 9) {
					GameObject obj1 = Instantiate (wwalldm);
					obj1.transform.position = GetGridPos(r, l);
					obj1.transform.SetParent (_canvas);
					GameObject obj = Instantiate (wwalldown);
					obj.transform.position = GetGridPos(r, l);
					obj.transform.SetParent (_canvas);
			}
			//转角
			if (map [r, l] == 15) {
					GameObject obj1 = Instantiate (wwalldm);
					obj1.transform.position = GetGridPos(r, l);
					obj1.transform.SetParent (_canvas);
					GameObject obj = Instantiate (left);
					obj.transform.position = GetGridPos(r, l);
					obj.transform.SetParent (_canvas);
				}
			if (map [r, l] == 14) {
					GameObject obj1 = Instantiate (wwalldm);
					obj1.transform.position = GetGridPos(r, l);
					obj1.transform.SetParent (_canvas);
					GameObject obj = Instantiate (right);
					obj.transform.position = GetGridPos(r, l);
					obj.transform.SetParent (_canvas);
				}
            if (map [r, l] ==13) {
					GameObject obj1 = Instantiate (wwalldm);
					obj1.transform.position = GetGridPos(r, l);
					obj1.transform.SetParent (_canvas);
					GameObject obj = Instantiate (up);
					obj.transform.position = GetGridPos(r, l);
					obj.transform.SetParent (_canvas);
				}
            if (map [r, l] == 12) {
					GameObject obj1 = Instantiate (wwalldm);
					obj1.transform.position = GetGridPos(r, l);
					obj1.transform.SetParent (_canvas);
					GameObject obj = Instantiate (down);
					obj.transform.position = GetGridPos(r, l);
					obj.transform.SetParent (_canvas);
				}
			}
	}

			
}

	/// <summary>
	/// 地图的范围
	/// </summary>
	public Vector2 GetGridPos(int row,int column){
		return new Vector2(30+size * column ,30+(Rows-row-1)*size   );
	}
	/// <summary>
	/// 根据行列值获取格子中心位置
	/// </summary>
	public Vector2 GetGridPos(Grid grid){
		return GetGridPos(grid.Rows, grid.Columns);
	}
	/// <summary>
	/// 根据位置获取行列值
	/// </summary>
	public Grid GetGrid(Vector2 pos){ 
		//RoundToInt函数用于对炸弹的坐标参数四舍五入
		return new Grid(
			Rows - (Mathf.RoundToInt((pos.y-30) / size) + 1),
			Mathf.RoundToInt((pos.x-30) / size));
	}
	/// <summary>
	/// 根据传入位置，算出最近格子位置
	/// </summary>
	public Vector2 GetNearGridPos(Vector2 pos){
		return GetGridPos(GetGrid (pos));
	}

	/// <summary>
	/// 计算当前格子是否能够移动
	/// </summary>
	public bool IsPassGrid(int row, int column){
		return IsPassGrid (new Grid(row, column));
	}

	/// <summary>
	/// 计算当前格子是否能够移动
	/// </summary>
	public bool IsPassGrid(Grid grid){
		if (grid.Rows < 0 || grid.Rows >= Rows ||
			grid.Columns < 0 || grid.Columns  >= Columns)
			return false;
		bool isPass = false;
		//让二维数组为1的可以移动
	
		if (map [grid.Rows , grid.Columns] == 1)
			isPass = true;
		   return isPass;
	
	}

}
*/