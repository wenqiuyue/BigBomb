using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player1 : MonoBehaviour {
	//获取Map脚本中的方法和对象
	Map _map;
	//炸弹预制
	public Bomb1 _bombPrefab;
	//玩家安放炸弹的初始威力
	public int _weiLi;
	//可以安放炸弹数量
	public  int bombnum; 
	//比较
	[HideInInspector]
	public  int chushibombnum;
	public int shengmingnum;
	[SerializeField]
	GameObject _fail;
	[SerializeField]
	GameObject _player;
	public Text _mymoney;
	public Text _jsMoney;
	public Text _getmoney;
	public Text _kills;
	public Text _killboss;
	public Text _shengming;

	public int _getmoney1;//每局金币获得数量
	public int _kills1;//每局击杀小怪数量
	public int _killboss1;//每局击杀Boss数量
	void Start(){
		_weiLi = 2;
		bombnum = 1;
		chushibombnum = 0;
		shengmingnum = 4;
		_getmoney1 = 0;
		_kills1=1;
		_killboss1=0;
		_kills.text = _kills1.ToString ();
		_shengming.text = "X"+(shengmingnum-1).ToString();    
			
		
	}
	//安放炸弹
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (chushibombnum < bombnum) {
				DropBomb ();
				chushibombnum++;
			}
		}
		if (shengmingnum == 1) {
			_fail.SetActive (true);
			_player.SetActive (false);

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
				Bomb1 bomb = Instantiate (_bombPrefab);
				//让炸弹定位在玩家最近的格子里
				bomb.transform.position = 
				_map.GetNearGridPos (transform.position);
				bomb.InitData (_weiLi);
		}
	}

	public void OnTriggerEnter(Collider other){
		KeepData saveData = SaveData._Sav.GetSaveData ();
		if (other.gameObject.tag == "jinbi") {
			saveData._Gold++;
			_getmoney1++;
			SaveData._Sav.SaveGameData ();
			_mymoney.text = saveData._Gold.ToString ();
			_getmoney.text = _getmoney1.ToString ();
			SaveData._Sav.SaveGameData ();
		}
		if (other.gameObject.tag == "weili") {
			if (_weiLi <4) {
				_weiLi = _weiLi + 1;
			} else {
				_weiLi = _weiLi;
			}
		}

		if (other.gameObject.tag == "bombnum") {
			bombnum = bombnum + 1;
			//Destroy (other.gameObject);
		}
		if (other.gameObject.tag == "shengming") {
			shengmingnum = shengmingnum + 1;
			_shengming.text = "X"+(shengmingnum-1).ToString();
			//Destroy (other.gameObject);
		}
		if (other.gameObject.tag == "Bombxiaoguo") {
			shengmingnum--;
			_shengming.text = "X"+(shengmingnum-1).ToString();
			StartCoroutine(WaitAndPrint(1.0f));
			if (shengmingnum <= 0) {
				Destroy (gameObject);
			}
		}
	}
	IEnumerator WaitAndPrint(float waitTime)  
	{  
		transform.localPosition = new Vector2 (-3910f, -1780f); 
		yield return new WaitForSeconds(waitTime);  
		//等待之后执行的动作  
		transform.localPosition = new Vector2 (-391f, -178f); 

	} 
}
	

