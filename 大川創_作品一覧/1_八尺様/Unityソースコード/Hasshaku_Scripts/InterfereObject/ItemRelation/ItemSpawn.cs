using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
	[SerializeField]private GameObject _keyItem = null;
    private GameObject _itemHolder = null;

	private void Awake()
	{
        _itemHolder = GameObject.Find("SpawnItemHolder");
		foreach(Transform child in gameObject.transform) {
			var clone =  Instantiate(_keyItem, child.position, Quaternion.identity);
			clone.name = _keyItem.name;
            clone.transform.parent = _itemHolder.transform;
		}
	}
}
