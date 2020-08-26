using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// Itemを継承
/// 使用時、前方にまっすぐ伸びる光を表示
/// </summary>
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
		
		//アイテム切り替えの通知があった時に、ライトをOFFにする
		_itemManager.SwitchItem
			.Subscribe(_ => {
				_lightON = false;
				_lightEffect.enabled = false;
			});
	}

	//使用済みのものではなかった場合、拾ったらすぐライトを点ける
	public override void ObjectAction()
	{
		base.ObjectAction();

		if (_possibleUse != null) {
			StartCoroutine(_possibleUse);
		}
	}

	//使用済みではなかった場合、コルーチンを止めてから捨てる
	public override void ItemDrop()
	{
		if (_possibleUse != null) {
			StopCoroutine(_possibleUse);
		}
		base.ItemDrop();
	}

	//ライトの点灯
	public override void ItemEffect()
	{
		base.ItemEffect();
		
		//使用済みではなかった場合、ライトを点灯
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

	//使用時間の計測
	private IEnumerator UseTime()
	{
		while (true) {
			yield return new WaitWhile(() => !_lightON);
			_useTime += Time.unscaledDeltaTime;
			
			//使用時間の合計が一定以上になった時、ライトが点灯しないようにする
			if (_useTime > _batteryTimeSec) {
				_lightEffect.enabled = false;
				_lightUI.Switch(false);
				StopCoroutine(_possibleUse);
				_possibleUse = null;
			}
		}
	}

}
