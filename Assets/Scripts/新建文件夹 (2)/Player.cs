using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour, INowPutBombNumMinuse {
	//获取Map脚本中的方法和对象
	Map _map;
	//炸弹预制
	public Bomb _bombPrefab;
	//玩家安放炸弹的初始威力
	public int _weiLi;
	//可以安放炸弹数量
	public  int bombnum; 

	/// <summary>
	/// 当前放置炸弹数
	/// </summary>
	public int _NowPutBombNum{ get{ return chushibombnum; } 
		set{ chushibombnum = value; 
			if (chushibombnum < 0)
				chushibombnum = 0;} }

	//比较
	public  int chushibombnum;
	void Start(){
		_weiLi = 2;
		bombnum = 3;
		chushibombnum = 0;
	}
	//安放炸弹
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (chushibombnum < bombnum) {
				DropBomb ();
				chushibombnum++;
			}
		}
	}
	/// <summary>
	/// 2017/11/10 Daryl
	/// 按下空格键让炸弹出现在就近的格子中
	/// </summary>
	public void DropBomb(){
		//获取map脚本
		_map = GameObject.FindObjectOfType<Map> ();
		if (_bombPrefab) {
				Bomb bomb = Instantiate (_bombPrefab);
				//让炸弹定位在玩家最近的格子里
				bomb.transform.position = 
				_map.GetNearGridPos (transform.position);
				bomb.InitData (_weiLi, this);
			//Debug.Log ("gjsgsdgs"+bomb.transform.position);
		}
	}

	public void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "jinbi") {
			if (_weiLi <4) {
				_weiLi = _weiLi + 1;
			} else {
				_weiLi = _weiLi;
			}
		}
		if (other.gameObject.tag == "bombnum") {
			bombnum = bombnum + 1;
		}
	}
}
	

