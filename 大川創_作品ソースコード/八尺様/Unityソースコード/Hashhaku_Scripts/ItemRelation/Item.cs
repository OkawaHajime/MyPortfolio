using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	private GameObject _itemManagerObject = null;
	protected ItemManager _itemManager = null;

	protected GameObject _tutorialUI = null;

	private GameObject _spawnItemHolder = null;

	private GameObject _disposeTutorial = null;
	private Renderer _itemRend = null;
	private Collider _itemCollider = null;

	private void Awake()
	{
		Initialize();
	}

	protected virtual void Initialize()
	{
		_itemManagerObject = GameObject.Find("ItemManager");
		_itemManager = _itemManagerObject.GetComponent<ItemManager>();
		
		_tutorialUI = GameObject.Find("Tutorial");
		
		_spawnItemHolder = GameObject.Find("SpawnItemHolder");
		
		_itemRend = gameObject.GetComponent<Renderer>();
		_itemCollider = gameObject.GetComponent<Collider>();
	}

	public virtual void ItemPick()
	{
		gameObject.transform.parent = _itemManagerObject.transform;
		gameObject.transform.position = _itemManagerObject.transform.position;
        _itemRend.enabled = false;
		_itemCollider.enabled = false;
    }

    public virtual void ItemDrop() 
	{
		gameObject.transform.parent = _spawnItemHolder.transform;
		_itemRend.enabled = true;
		_itemCollider.enabled = true;
	}

	public virtual void ItemEffect() 
	{ 
		if (_disposeTutorial != null) {
			Destroy(_disposeTutorial);
		}
	}
	
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

	private IEnumerator DestroyTutorial(GameObject tutorial)
	{
		yield return new WaitForSeconds(3.0f);
		Destroy(tutorial);
	}
}
