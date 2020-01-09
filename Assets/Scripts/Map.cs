 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	/*public static bool operator ==(Grid grid, Grid grid2){
		if (grid.Rows == grid2.Rows &&
		   grid.Columns == grid2.Columns)
			return true;
		    return false;
	}

	public static bool operator !=(Grid grid, Grid grid2){
		return !(grid == grid2);
	}*/
	//把Rows,Columns转化为字符串
	public override string ToString ()
	{
		return string.Format ("[Grid: Rows={0}, Columns={1}]", Rows, Columns);
	}

	/// <summary>
	/// 计算移动到目标格子理论花费步数
	/// </summary>
	public int CalcMoveCount(Grid target){
		return Mathf.Abs(target.Rows - Rows) + Mathf.Abs(target.Columns - Columns);
	}
}
public class Map : MonoBehaviour {
	//定义行
	public  int Rows = 9;
	//定义列
	public  int Columns = 14;
	//定义格子大小
	public float size = 60f;
	//雪块
	public GameObject xue;
	public GameObject diren;

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

	/// <summary>
	/// 能炸毁的障碍物数组
	/// </summary>
	[SerializeField]
	GameObject[] _obstacleArray;
	/// <summary>
	/// 不能炸毁的障碍物数组
	/// </summary>
	[SerializeField]
	GameObject[] _noBombObstacleArray;

	[SerializeField]
	Transform _canvas;
	//9行13列的二维数组
	public int[,]  map = new int[9,14] {
		{ 15,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 14 }, 
		{ 5, 1, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 9 }, 
		{ 5, 1, 3, 1, 1, 1, 2, 1, 1, 2, 1, 2, 1, 9 }, 
		{ 5, 1, 1, 3, 3, 3, 2, 1, 2, 3, 1, 3, 3, 9 }, 
		{ 5, 1, 1, 3, 3, 1, 3, 3, 1, 1, 3, 3, 2, 9 }, 
		{ 5, 1, 2, 3, 1, 1, 1, 1, 2, 1, 3, 2, 1, 9 }, 
		{ 5, 1, 1, 1, 3, 1, 1, 3, 3, 3, 1, 1, 3, 9 }, 
		{ 5, 1, 1, 3, 2, 1, 2, 1, 3, 1, 2, 1, 1, 9 },
		{ 13, 8,8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 12 }
	};

	string strPath = "";

	public int[,]  _Map{get{ 
			/*System.Text.StringBuilder bld = new System.Text.StringBuilder ();
			bld = new System.Text.StringBuilder ();
			for(int i = 0; i < _map.GetLength(0); i++){
				for(int j = 0; j < _map.GetLength(1); j++){
					if (j != 0)
						bld.Append (' ');
					bld.Append (_map[i, j]);
				}
				bld.Append ('\n');
			}

			string tempPath = bld.ToString ();
			if (tempPath != strPath) {
				strPath = tempPath;
				Debug.Log (strPath);
			}*/

			return map; 
		} set{ 
			map = value; 

		}}

	// Use this for initialization
	void Start () {
		for (int r = 0; r <Rows; r++) {
			for (int l = 0; l < Columns; l++) {
				// 不能炸毁的障碍
				if(map[r, l] == 2){
					GameObject obj = Instantiate (xue);
					obj.transform.position = GetGridPos(r, l);
					obj.transform.SetParent (_canvas);

					obj = Instantiate (_noBombObstacleArray[Random.Range(0, _noBombObstacleArray.Length)]);
					obj.transform.position = GetGridPos(r, l);
					obj.transform.SetParent (_canvas);
				}

				// 能炸毁的障碍物
				if(map[r, l] == 3){
					GameObject obj = Instantiate (xue);
					obj.transform.position = GetGridPos(r, l);
					obj.transform.SetParent (_canvas);

					obj = Instantiate (_obstacleArray[Random.Range(0, _obstacleArray.Length)]);
					obj.transform.position = GetGridPos(r, l);
					obj.transform.SetParent (_canvas);
				}

				//地面
				if (map [r,l] == 1) {
					GameObject obj = Instantiate (xue);
					obj.transform.position = GetGridPos(r, l);
					obj.transform.SetParent (_canvas);
				}
				if (map [r,l] == 6) {
					GameObject obj = Instantiate (xue);
					obj.transform.position = GetGridPos(r, l);
					obj.transform.SetParent (_canvas);
					GameObject obj5 = Instantiate (diren);
					obj5.transform.position = GetGridPos(r, l);
					obj5.transform.SetParent (_canvas);
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
	
		if (_Map [grid.Rows , grid.Columns] != 0 &&
			_Map [grid.Rows , grid.Columns] != 2&& 
			_Map [grid.Rows , grid.Columns] != 3&& 
			_Map [grid.Rows , grid.Columns] != 5&& 
			_Map [grid.Rows , grid.Columns] != 8&& 
			_Map [grid.Rows , grid.Columns] != 9&& 
			_Map [grid.Rows , grid.Columns] != 12&& 
			_Map [grid.Rows , grid.Columns] != 13&& 
			_Map [grid.Rows , grid.Columns] != 14
			&& 
			_Map [grid.Rows , grid.Columns] != 15)
			isPass = true;
		   return isPass;
	
	}

	/// <summary>
	/// 获取地图数组的复制
	/// </summary>
	/// <returns>The copy map.</returns>
	public int[,] GetCopyMap(){
		// 新建和地图一样大小的数组
		int[,] copyMap = new int[_Map.GetLength(0), _Map.GetLength(1)];
		// 把地图中所有格子付给新建地图
		for(int i = 0; i < _Map.GetLength(0); i++){
			for(int j = 0; j < _Map.GetLength(1); j++){
				copyMap [i, j] = _Map [i, j];
			}
		}
		return copyMap;
	}
}
