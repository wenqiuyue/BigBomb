using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	//速度
	public float _speed;
	// 当前地图信息
	Map _map;
	//定义行列值
	Grid _grid = new Grid(2, 1);
	// Use this for initialization
	void Start () {
	     //获取Map脚本
		_map = GameObject.FindObjectOfType<Map> ();
		//玩家的初始位置在Map中的（2,1）位置
		transform.position = _map.GetGridPos (_grid);
		Debug.Log ( _map.GetGridPos (_grid));
	}

	/// <summary>
	/// 2017/11/10 Daryl
	/// 使用键盘上的WSAD让人物前后左右移动
	/// </summary>
	void Update () {
		// 0代表水平方向未移动，1代表向左移动，2代表向右移动
		byte isMoveLeft = 0;
		// 0代表竖直方向未移动，1代表向上移动，2代表向下移动
		byte isMoveUp = 0;
		Vector2 pos = transform.position;
		//w向上移动
		if (Input.GetKey (KeyCode.W)||Input.GetKey(KeyCode.UpArrow)) {
			pos.y += _speed;
			isMoveUp = 1;
		}
		//s向后移动
		if (Input.GetKey (KeyCode.S)||Input.GetKey(KeyCode.DownArrow)) {
			pos.y -= _speed;
			isMoveUp = 2;
		}
		//向左移动
		if (Input.GetKey (KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)) {
			pos.x -= _speed;
			isMoveLeft = 1;
		}
		//向右移动
		if (Input.GetKey (KeyCode.D)||Input.GetKey(KeyCode.RightArrow)) {
			pos.x += _speed;
			isMoveLeft = 2;
		}
		//格子值转换为坐标
		Vector2 nowGridPos = _map.GetGridPos (_grid);
		// 1如果向左判断是否移动、2如果向右判断是否移动
		if (isMoveLeft == 1) {
			//Debug.LogFormat ("{0} {1}", pos.x, nowGridPos.x);
			if (pos.x <= nowGridPos.x &&!_map.IsPassGrid (_grid.Rows, _grid.Columns - 1)) {
				pos.x = _map.GetGridPos(_grid).x;
			}
		}
		else if(isMoveLeft == 2){
			//Debug.LogFormat ("{0} {1}", pos.x, nowGridPos.x);
			if (pos.x >= nowGridPos.x && !_map.IsPassGrid (_grid.Rows, _grid.Columns + 1)) {
				pos.x = _map.GetGridPos(_grid).x;
			}
		}
		//// 1如果向上判断是否移动、2如果向下判断是否移动
		if (isMoveUp == 1) {
			//Debug.LogFormat ("{0} {1}", pos.y, nowGridPos.y);
			if (pos.y >= nowGridPos.y &&!_map.IsPassGrid (_grid.Rows - 1, _grid.Columns)) {
				pos.y = _map.GetGridPos(_grid).y;
			}
		}
		else if(isMoveUp == 2){
			//Debug.LogFormat ("{0} {1}", pos.y, nowGridPos.y);
			if (pos.y <= nowGridPos.y &&!_map.IsPassGrid (_grid.Rows + 1, _grid.Columns)) {
				pos.y = _map.GetGridPos(_grid).y;
			}
		}
		//将位置传入进行四舍五入后变为新的行列值
		Grid nowGrid = _map.GetGrid (transform.position);
		if (nowGrid != _grid) {
			_grid = nowGrid;
		}
		transform.position = pos;
	}
	}

