using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DJJieSuo : MonoBehaviour{

	[SerializeField]
	public List<Button> _itemBtnList;
	public Text t1;
	public List<GameObject> _map;
	public  Sprite sp;
	public Button b;


	void Start()
	{           
		KeepData saveData = SaveData._Sav.GetSaveData ();
		Itemsave s = GameObject.FindObjectOfType<Itemsave> ();
		for (int i = 1; i < saveData._IsUclockItem.Count; i++) {
			if (saveData._IsUclockItem [i]) {
				
				_itemBtnList [i].image.sprite = sp;
			}
		}
		for (int q = 0; q < saveData._ItemId.Count; q++) {
			if (saveData._HaveItemOn [q] == saveData._ItemId [q]) {
				_itemBtnList [q].image.sprite = s._item [saveData._ItemId [q]];
			}
		}
		JS ();
		for (int i = 0; i < _map.Count; i++) 
		{
			if (saveData._GuanQiaData[i]) 
			{   for (int j = 0; j < _map.Count; j++) {
					_map [j].SetActive (false);}
				_map [i].SetActive (true);
			}
		}
	}

	/// <summary>
	/// 道具按钮解锁
	/// </summary>
	public void ItemBtnUnlock(Button a){
		KeepData saveData = SaveData._Sav.GetSaveData ();
		if (saveData._IsUclockItem [saveData._IsUclockItemNum]==true)
		{
			a.image.sprite = sp;
			SpriteState state;
			state.pressedSprite = sp;
			state.highlightedSprite = sp;

		}
	
	}
	public void OnClick()
	{  KeepData saveData = SaveData._Sav.GetSaveData ();
		ItemBtnUnlock (_itemBtnList [saveData._IsUclockItemNum]);
	}
	public void JS(){
		SaveData sav = GameObject.FindObjectOfType<SaveData> ();
		KeepData dj= sav.GetSaveData ();
		b.onClick.AddListener (delegate() {
			dj._IsUclockItem [dj._IsUclockItemNum] = true;
			sav.SaveGameData();
			if (dj._IsUclockItemNum < 5) {
				OnClick ();
				dj._IsUclockItemNum++;
				sav.SaveGameData();

			}
		});

	}

}
