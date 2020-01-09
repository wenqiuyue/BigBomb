using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DjShow : MonoBehaviour {
	[SerializeField]
	Pic _itemPrefab;
	[SerializeField]
	Transform _canvas;
	[SerializeField]
	Transform _DJ;
	[SerializeField] 
	Transform _DJL;
	[SerializeField]
	List<GameObject> _dj;
	[SerializeField]
	Button _btnPrefab;
	[SerializeField]
	GameObject btn;
	/// <summary>
	/// 技能点点亮图片
	/// </summary>
	public Sprite s2;
	/// <summary>
	/// 道具解锁图片
	/// </summary>
	public Sprite s1;
	//技能点个数图片
	public Text _skillpoint;
	//定义列数
	public static int colunm_count = 4;
	// 道具图片列表
	List<Pic> _itemPic;
	static float x;

	void Start()
	{    
		KeepData saveData = SaveData._Sav.GetSaveData ();
		Itemsave s = GameObject.FindObjectOfType<Itemsave> ();
		Debug.Log (saveData._Slidery);
		for (int q = 0; q < saveData._HaveItemOn.Count; q++) {
			GameObject dj = GameObject.Instantiate (btn);
			dj.transform.SetParent (_DJL);
			dj.GetComponent<Button> ().image.sprite = s._item [saveData._HaveItemOn[q]];
			dj.transform.GetComponent<RectTransform> ().anchoredPosition= new Vector2 (45+85*q , 44);
			_dj.Add (dj);

		}  
	

		for (int t = 0; t < saveData._IsUclockItem.Count; t++) {
			if (saveData._IsUclockItem[t] == true) {
				_dj [t].GetComponent<Button> ().image.sprite = null;
				_dj [t].GetComponent<Button> ().image.color = new Color (0, 0, 0, 0);
				//saveData._HaveItemOn [t] = 10;
				_dj[t].transform.SetParent (_DJ);

			}
		}

		for (int q = 0; q < saveData._HaveItemOn.Count; q++) {
			if (saveData._HaveItemOn [q] <10) {
				_dj [q].GetComponent<Button> ().image.sprite = s._item [saveData._HaveItemOn [q]];
				_dj [q].GetComponent<Button> ().image.color = new Color (1, 1, 1, 1);
			}
		}

		_itemPic = new List<Pic> (saveData._HaveItemId.Count);
		for (int i = 0; i < saveData._HaveItemId.Count; i++) {   
			Pic item = GameObject.Instantiate (_itemPrefab);
			item._Id = saveData._HaveItemId [i];

			//更新技能点
			Gx (item);
			item.GetComponentInChildren<Text> ().text = saveData._HaveItemName [saveData._HaveItemId [i]];
			item.Shuoming.text = ""+item._shuoming [saveData._HaveItemId [i]];
			item.pic.sprite = item._pic [saveData._HaveItemId[i]];
			item.transform.SetParent (_canvas);
			x = -196 * (Mathf.Pow (-1, (i % colunm_count)));
			saveData._Slidery = 118- 195 * (i / colunm_count);
			SaveData._Sav.SaveGameData ();
			item.transform.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (x, saveData._Slidery);
			//已解锁道具个数大于可装备道具个数
			item._Use.onClick.AddListener (delegate() {

				//道具装备个数小于道具解锁个数
				if (saveData._Djnum<saveData._IsUclockItemNum){
					if(item._Id<10){	
						Click (item._Id, item);

						saveData._Djnum++;
						SaveData._Sav.SaveGameData();
					}
				}
			});

			// 把道具图片加入列表
			_itemPic.Add (item);


			for (int t = 0; t < item._Skill.Count; t++) {
				SkillJs (t, item, item._Id);
			}

		  
	}
		for (int k = 0; k < _dj.Count; k++) {
			int index = k;
			if (saveData._IsUclockItem [k] == true) {
				_dj [k].GetComponent<Button> ().onClick.AddListener (delegate() {
					if (saveData._Djnum > 0) {
						DropItem (index);
					}
				});
			}
		}
		for (int k = 0; k < saveData._HaveItemOn.Count; k++) {
			int index = k;
			if (saveData._IsUclockItem [k] == true) {
				_dj[k].GetComponent<Button>().onClick.AddListener (delegate() {
					if (saveData._Djnum > 0) {
						DropItem (index);

					}

				});
			}

			// 装备栏为空处理
			if (saveData._HaveItemOn [k] == -1&&saveData._IsUclockItem[k]==true) {
				_dj[k].GetComponent<Button>().image.sprite = null;
			}
			// 有装备处理
			for (int j = 0; j < 10; j++) {

				if (saveData._HaveItemOn [k] == j) {
					_dj[k].GetComponent<Button>().image.sprite = s._item [j];
				
				}
			}

		}
	

	}



	/// 使用道具
	/// </summary>
	/// <param name="i">The index.</param>
	/// <param name="item">Item.</param>
	void Click(int i,Pic item)
	{
		KeepData saveData = SaveData._Sav.GetSaveData ();
		Itemsave s = GameObject.FindObjectOfType<Itemsave> ();
		saveData._HaveItemId.Remove(i);
		saveData._ItemId.Add (i);
		Destroy (item.gameObject);


		//道具栏显示道具
		if (i < 10) 
		{
			saveData._HaveItemOn [saveData._Djnum] = i;
			SaveData._Sav.SaveGameData ();
			if (saveData._HaveItemOn [saveData._Djnum] == i) {
				for (int q = 0; q < saveData._HaveItemOn.Count; q++) {
					if(saveData._HaveItemOn[q]<10)
						_dj [saveData._Djnum].GetComponent<Button> ().image.sprite = s._item [saveData._HaveItemOn [q]];
					_dj [saveData._Djnum].GetComponent<Button> ().image.color = new Color (1, 1, 1, 1);
					
				}
			}

		}
	}

	void Click2(int i,Pic item)
	{
		KeepData saveData = SaveData._Sav.GetSaveData ();
		Itemsave s = GameObject.FindObjectOfType<Itemsave> ();
		saveData._HaveItemId.Remove(i);
		saveData._ItemId.Add (i);
		Destroy (item.gameObject);


		//道具栏显示道具
		if (i < 10) 
		{
			saveData._HaveItemOn.Add (i);
			saveData._HaveItemOn.Reverse ();
			SaveData._Sav.SaveGameData ();
			_dj [1].GetComponent<Button> ().image.sprite = s._item [i];


	}
	}

	/*void Click3(int i,Pic item)
	{    int m = 0;
		KeepData saveData = SaveData._Sav.GetSaveData ();
		Itemsave s = GameObject.FindObjectOfType<Itemsave> ();
		saveData._HaveItemId.Remove(i);
		saveData._ItemId.Add (i);
		//Destroy (item.gameObject);
		item.gameObject.SetActive(false);

		//道具栏显示道具
		if (i < 10) 
		{
			saveData._HaveItemOn.Add (i);
			saveData._HaveItemOn.Reverse ();

			SaveData._Sav.SaveGameData ();

			GameObject dj = GameObject.Instantiate (btn);
			dj.transform.SetParent (_DJ);
			dj.GetComponent<Button> ().image.sprite = null;
			dj.transform.GetComponent<RectTransform> ().anchoredPosition= new Vector2 (75+150*saveData._Djnum , -50);
			_dj [m].GetComponent<Button> ().image.sprite = s._item [i];
			m++;
			_dj.Add (dj);
			_dj.Reverse ();

		}
	}
*/
	/// <summary>
	/// 技能加点
	/// </summary>
	/// <param name="t">T.</param>
	/// <param name="item">Item.</param>
	/// <param name="id">Identifier.</param>
	void SkillJs(int t,Pic item,int id)
	{
		KeepData saveData = SaveData._Sav.GetSaveData ();
			item._Skill [t].onClick.AddListener (delegate {
			if(saveData._IsUclockSkill[id]._a[t]==false){
				if(saveData._Skill>=1){
					saveData._Skill--;
				SaveData._Sav.SaveGameData();
				_skillpoint.text = saveData._Skill.ToString ();
				// 遍历该道具所有等级，解锁一个等级
				for(sbyte i = 0; i < saveData._IsUclockSkill[id]._a.Count; i++){
					// 已经解锁，继续循环
					if(saveData._IsUclockSkill[id]._a[i])
						continue;
					// 未解锁则解锁并退出
					saveData._IsUclockSkill[id]._a[i]=true;
					SaveData._Sav.SaveGameData();
					if(saveData._IsUclockSkill[id]._a[i])
					{
						item._Skill[i].image.sprite=s2;
					}

					// 更新所有道具图片
					UpdateAllItemPic();
					break;
				}
				}
			}});
	}
	/// <summary>
	/// 更新所有道具图片
	/// </summary>
	void UpdateAllItemPic(){
		KeepData saveData = SaveData._Sav.GetSaveData ();
		for(int i = 0; i <_itemPic.Count; i++){
			Gx (_itemPic[i]);
		}
	}
	/// <summary>
	/// 更新一个道具上所有技能点
	/// </summary>
	/// <param name="item">Item.</param>
	void Gx(Pic item)
	{  KeepData saveData = SaveData._Sav.GetSaveData ();
		for (int t = 0; t < item._Skill.Count; t++) 
		{   if(saveData._IsUclockSkill[item._Id]._a[t])
			item._Skill[t].image.sprite=s2;
		}
	}
	/// <summary>
	/// 卸下该栏装备
	/// </summary>
	/// <param name="i">The index.</param>
	void DropItem(int i){
		// 卸下空道具，退出
		if (_dj [i].GetComponent<Button>().image.sprite == null) {
			return;
		}

		KeepData saveData = SaveData._Sav.GetSaveData();
		// 卸下道具，图片为空
		//if (saveData._HaveItemOn [i] != 10 && saveData._IsUclockItem [i] == true) {
			saveData._HaveItemId.Add (saveData._HaveItemOn [i]);
			saveData._ItemId.Remove (saveData._HaveItemOn [i]);
			saveData._HaveItemOn.Remove (i);
			SaveData._Sav.SaveGameData ();

			_dj [i].GetComponent<Button> ().image.sprite = null;
		    _dj [i].SetActive (false);
			//Destroy (_dj [i]);
			saveData._Djnum--;
			SaveData._Sav.SaveGameData ();
			//}
		// 更新道具栏
		int z = saveData._HaveItemId.Count - 1;
		Pic item=GameObject.Instantiate(_itemPrefab);
		item._Id = saveData._HaveItemId [z];
		//更新技能点
		Gx (item);
		item.GetComponentInChildren<Text> ().text= saveData._HaveItemName [saveData._HaveItemId [z]];
		item.Shuoming.text = ""+item._shuoming [saveData._HaveItemId [i]];
		item.pic.sprite = item._pic [saveData._HaveItemId[i]];
		item.transform.SetParent (_canvas);
		x = -196 * (Mathf.Pow (-1, (z % colunm_count)));
		saveData._Slidery = 118 - 195 * (z/ colunm_count);
		SaveData._Sav.SaveGameData ();
		item.transform.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (x, saveData._Slidery);
		item._Use.onClick.AddListener (delegate() {
			//道具装备个数小于道具解锁个数
			if (saveData._Djnum<saveData._IsUclockItemNum){
				if(item._Id<10){	
					Click2(item._Id, item);

					saveData._Djnum++;
					SaveData._Sav.SaveGameData();
				}
			}
		});
		// 把道具图片加入列表
		_itemPic.Add (item);


		for(int t=0;t<item._Skill.Count;t++)
		{
			SkillJs (t, item,item._Id);
		}

		GameObject dj = GameObject.Instantiate (btn);
		dj.transform.SetParent (_DJ);
		dj.GetComponent<Button> ().image.sprite = null;
		dj.GetComponent<Button> ().image.color = new Color (0, 0, 0, 0);
		dj.transform.GetComponent<RectTransform> ().anchoredPosition= new Vector2 (75+150*saveData._Djnum , -50);
		_dj.Add (dj);
		_dj.Reverse ();
	}


}