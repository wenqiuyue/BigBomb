using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDate : Grid{

	//当前移动步数
	public int _gCost{get;set;}

	//当前离玩家步数
	public int _hCost{get;set;}

	//理论移动步数
	public int  _theory{get;set;} 

	//实际移动步数
	public int _actual{get;set;}

	//权值
	//public int _fCost;
	public int _fCost{get{ return _gCost + _hCost; } }

	//public GridDate[,] grid;


	/*public GridDate GetGrid(){
		
	}*/

	//public GridDate(){

	//}

	/// <summary>
	/// 创建格子的构造方法
	/// </summary>
	public GridDate(int row, int column) : base(row, column){
	}

	//记录格子
	public GridDate _writeGrid;

	//判断此格子是否在列表中
	public static bool IsList(List<GridDate> gridList, int row, int column){
		for (int i = 0; i < gridList.Count; i++) {
			if (gridList[i].Rows == row && gridList[i].Columns == column) {
				return true;
			}

		}
		return false;
	}
		
	public override string ToString ()
	{
		return string.Format ("[GridDate: Row={0} Column={1} _theory={2}]", Rows, Columns, _theory);
	}
}


/// <summary>
/// 根据理论移动步数对格子进行升序排列
/// </summary>
public class SortGridDataByTheoryAsc : IComparer<GridDate>{
	public int Compare(GridDate x, GridDate y){
		return x._theory - y._theory;
	}
}
