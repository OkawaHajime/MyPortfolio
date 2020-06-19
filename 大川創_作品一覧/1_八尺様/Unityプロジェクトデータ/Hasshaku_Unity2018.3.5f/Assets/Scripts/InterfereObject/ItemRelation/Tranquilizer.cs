using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public override void ItemDrop()
	{
		if (_use) {
			Destroy(gameObject);
		}
		else {
			base.ItemDrop();
		}
	}

	public override void ItemEffect()
	{
		base.ItemEffect();
		
		_use = true;
		_fearManager.DownFear();
		_itemManager.DisposeItem();
	}
}
