using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Matches : Item
{
	[SerializeField]private int _use_restrictions = 3;

	private static bool _firstPick = false;

    private GameObject _mouseControllerObject = null;
	private MouseController _mouseController = null;

	private GameObject _use_restrictions_Object = null;
	private Text _use_restrictions_Text = null;

	private GameObject _matchObject = null;

	private Lamp _lamp = null;

	protected override void Initialize()
	{
		base.Initialize();

		_mouseControllerObject = GameObject.Find("MouseController");
		_mouseController = _mouseControllerObject.GetComponent<MouseController>();

		_use_restrictions_Object = GameObject.Find("UseRestrictions_text");
		_use_restrictions_Text = _use_restrictions_Object.GetComponent<Text>();
		
		_matchObject = gameObject.transform.GetChild(0).gameObject;
	}

	public override void ObjectAction()
	{
		base.ObjectAction();

		_matchObject.SetActive(false);

		_use_restrictions_Text.text = "（残り：" + _use_restrictions.ToString() + "本）";
		_use_restrictions_Object.SetActive(true);

		_firstPick = DisplayTutorial(_firstPick);
	}

	public override void ItemDrop()
	{
		if(_use_restrictions == 0) {
			Destroy(gameObject);
		}
		else {
			base.ItemDrop();
			_matchObject.SetActive(true);
		}
	}

	public override void ItemEffect()
	{
		base.ItemEffect();
		
		if (_mouseController.HitObject.collider != null &&
			_mouseController.HitObject.collider.gameObject.tag == "Lamp") 
		{
			_use_restrictions -= 1;
			_use_restrictions_Text.text = "（残り：" + _use_restrictions.ToString() + "本）";
			_use_restrictions_Object.SetActive(true);

			_lamp = _mouseController.HitObject.collider.gameObject.GetComponent<Lamp>();
			_lamp.Ignition();
            if (_use_restrictions == 0) {
				_itemManager.DisposeItem();
			}
		}
	}
}
