using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// InterfereObjectを継承
/// マウスの右クリックをした時にアクションを行う親クラス
/// 子クラスでアイテムごとの効果を実装する
/// 子クラスのScriptを各アイテムに付ける
/// </summary>
public class Item : InterfereObject
{
	private GameObject _itemManagerObject = null;
	protected ItemManager _itemManager = null;

	protected GameObject _tutorialUI = null;

	private GameObject _spawnItemHolder = null;

	private GameObject _disposeTutorial = null;
	private Renderer _itemRend = null;
	private Collider _itemCollider = null;

	/// <summary>
	/// 以下、実装する部分
	/// </summary>

	//情報取得
	protected override void Initialize()
	{
		_itemManagerObject = GameObject.Find("ItemManager");
		_itemManager = _itemManagerObject.GetComponent<ItemManager>();

		_tutorialUI = GameObject.Find("Tutorial");

		_spawnItemHolder = GameObject.Find("SpawnItemHolder");

		_itemRend = gameObject.GetComponent<Renderer>();
		_itemCollider = gameObject.GetComponent<Collider>();
	}

	//アイテム取得
	public override void ObjectAction()
	{
		gameObject.transform.parent = _itemManagerObject.transform;
		gameObject.transform.position = _itemManagerObject.transform.position;
		_itemRend.enabled = false;
		_itemCollider.enabled = false;

		_itemManager.GetItem(gameObject);
	}

	//アイテム捨て
    public virtual void ItemDrop() 
	{
		gameObject.transform.parent = _spawnItemHolder.transform;
		_itemRend.enabled = true;
		_itemCollider.enabled = true;
	}

	//アイテムの効果
	public virtual void ItemEffect() 
	{ 
		if (_disposeTutorial != null) {
			Destroy(_disposeTutorial);
		}
	}
	
	//初取得時、アイテムに対応したチュートリアルを表示
	protected bool DisplayTutorial(bool firstPick)
	{
		if (!firstPick) {
			firstPick = true;

			foreach (Transform child in _tutorialUI.transform) {
				if (child.gameObject.name == gameObject.name) {
					_disposeTutorial = child.gameObject;
					child.gameObject.SetActive(true);
					StartCoroutine(DestroyTutorial(_disposeTutorial));
					break;
				}
			}
		}
		return firstPick;
	}

	//指定時間待機後、チュートリアルテキストを削除
	private IEnumerator DestroyTutorial(GameObject tutorial)
	{
		yield return new WaitForSeconds(3.0f);
		Destroy(tutorial);
	}
}
