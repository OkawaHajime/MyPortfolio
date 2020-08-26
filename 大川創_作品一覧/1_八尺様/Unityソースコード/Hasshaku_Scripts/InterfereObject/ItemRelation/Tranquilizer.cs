using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Itemを継承
/// 使用時、恐怖度を1段階下げる
/// </summary>
public class Tranquilizer : Item
{
	private GameObject _fearManagerObject = null;
	private FearManager _fearManager = null;

	private static bool _firstPick = false;
	private bool _use = false;
	public bool Used {
		get {
			return _use;
		}
	}

	protected override void Initialize()
	{
		base.Initialize();

		_fearManagerObject = GameObject.Find("FearManager");
		_fearManager = _fearManagerObject.GetComponent<FearManager>();
	}

	public override void ObjectAction()
	{
		base.ObjectAction();

		_firstPick = DisplayTutorial(_firstPick);
	}

	//使用済みの時オブジェクトを削除
	public override void ItemDrop()
	{
		if (_use) {
			Destroy(gameObject);
		}
		else {
			base.ItemDrop();
		}
	}

	//使用時にこのアイテムを捨てる
	public override void ItemEffect()
	{
		base.ItemEffect();
		
		_use = true;
		_fearManager.DownFear();
		_itemManager.DisposeItem();
	}
}
