using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class FlashLight : Item
{
	[SerializeField]private float _batteryTimeSec = 300.0f;

	private static bool _firstPick = false;
	private IEnumerator _possibleUse = null;
	private float _useTime = 0.0f;
	private bool _lightON = false;

    private GameObject _lightUIObject = null;
	private Flashlight_PRO _lightUI = null;

	private GameObject _lightEffectObject = null;
	private Light _lightEffect = null;

	protected override void Initialize()
	{
		base.Initialize();
		
		_lightEffectObject = GameObject.Find("Flash");
		_lightEffect = _lightEffectObject.GetComponent<Light>();

		_lightUIObject = GameObject.Find("PossessionItems");
		_lightUI = _lightUIObject.GetComponentInChildren<Flashlight_PRO>();
	}

	private void Start()
	{
		_possibleUse = UseTime();
		
		_lightEffect.enabled = false;
		
		_itemManager.SwitchItem
			.Subscribe(_ => {
				_lightON = false;
				_lightEffect.enabled = false;
			});
	}

	public override void ObjectAction()
	{
		base.ObjectAction();

		if (_possibleUse != null) {
			StartCoroutine(_possibleUse);
		}
	}

	public override void ItemDrop()
	{
		if (_possibleUse != null) {
			StopCoroutine(_possibleUse);
		}
		base.ItemDrop();
	}

	public override void ItemEffect()
	{
		base.ItemEffect();
		
		if (_possibleUse != null) {
			if (!_lightON) {
				_lightON = true;
				_lightEffect.enabled = true;
				_lightUI.Switch(true);
			}
			else {
				_lightON = false;
				_lightEffect.enabled = false;
				_lightUI.Switch(false);
			}
		}
	}

	private IEnumerator UseTime()
	{
		while (true) {
			yield return new WaitWhile(() => !_lightON);
			
			_useTime += Time.unscaledDeltaTime;
			
			if (_useTime > _batteryTimeSec) {
				_lightEffect.enabled = false;
				_lightUI.Switch(false);
				StopCoroutine(_possibleUse);
				_possibleUse = null;
			}
		}
	}

}
