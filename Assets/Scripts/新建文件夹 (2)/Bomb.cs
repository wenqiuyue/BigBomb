
using UnityEngine; 
using System.Collections; 
using System.Runtime.CompilerServices; 

/// <summary>
/// 当前放置炸弹数量减少接口
/// </summary>
public interface INowPutBombNumMinuse{
	/// <summary>
	/// 当前放置炸弹数
	/// </summary>
	int _NowPutBombNum{ get; set; }
}

public class Bomb : MonoBehaviour { 
	//爆炸效果预制
	public GameObject explosionPrefab;
	//是否生成爆炸效果
	private bool exploded = false;
	//定义levelmask层级
	public LayerMask levelMask;
	//炸弹效果遍历的范围
	public int j;

	/// <summary>
	/// 记录放置炸弹角色
	/// </summary>
	public INowPutBombNumMinuse _putBombPlayer; 

	/// <summary>
	/// 初始炸弹威力，记录放炸弹角色
	/// </summary>
	/// <param name="weiLi">Wei li.</param>
	/// <param name="player">Player.</param>
	public void InitData(int weiLi, INowPutBombNumMinuse putBombPlayer){
		_putBombPlayer = putBombPlayer;
		InitData (weiLi);
	}

	/// <summary>
	/// 炸弹效果的生成和方向判断
	/// </summary>
	void InitData(int weiLi) {
		j = weiLi;
		Invoke("Explode",3f);
		//Debug.Log ("shabiwengqiuyue"+transform.position);
	}
	/// <summary>
	/// 调用协程在前后左右四个方向生成爆炸效果
	/// </summary>
	void Explode() {
		GameObject obj= Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		StartCoroutine(CreateExplosions(Vector2.up)); 
		StartCoroutine(CreateExplosions(Vector2.right)); 
		StartCoroutine(CreateExplosions(Vector2.down)); 
		StartCoroutine(CreateExplosions(Vector2.left));
		exploded = true;
		//0.1秒后摧毁炸弹预制自身
		Destroy(gameObject,0.1f); 
		_putBombPlayer._NowPutBombNum--;
		//0.3秒后摧毁生成的爆炸效果
		Destroy (obj, 0.3f);
		}
	/// <summary>
	/// 爆炸效果链式反应
	/// </summary>
	public void OnTriggerEnter(Collider other) {
		if (!exploded && other.CompareTag("Bombxiaoguo")) { 
			CancelInvoke("Explode"); 
			Explode();
		}
	}
	/// <summary>
	/// 创建协程，生成爆炸效果
	/// </summary>
	private IEnumerator CreateExplosions(Vector2 direction) { 
		for (int i = 1; i < j; i++) {
			//记录当前位置
			Vector2 pos = transform.position;
			//生成射线
			RaycastHit hit;
			//射线出现的位置、方向、距离、层级
			if (!Physics.Raycast
				(transform.position, direction, out hit, (float)(60 * i), levelMask)) {
				GameObject obj1 = 
					Instantiate (explosionPrefab, pos + ((float)(60 * i) * direction),
						explosionPrefab.transform.rotation);
				Destroy (obj1, 0.3f); 
			}
			if (hit.collider != null) {
				if (hit.collider.tag == "baoxiang") {
					GameObject obj1 = Instantiate (explosionPrefab, hit.transform.position, Quaternion.identity);
					Destroy (obj1, 0.3f);
					break;
				}
			}

			yield return null; 
		} 
	} 
}


