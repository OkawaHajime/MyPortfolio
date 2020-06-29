using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TutorialSceneManager : MonoBehaviour
{
	[SerializeField] private GameObject _matchEndtext = null;
	[SerializeField] private GameObject _flashLightText = null;
	[SerializeField] private GameObject _clearText = null;

	private GameObject _playerObject = null;
	private PlayerController _playerController = null;

	private GameObject _initialEquipmentObject = null;
	private Item _initialEquipment = null;

	private GameObject _enemyObject = null;
	private ChangeTargetPoint _enemyBrain = null;

	private GameObject _barrier = null;

	private GameObject _lampObject = null;

	private GameObject _lightEffectObject = null;
	private Light _lightEffect = null;

	private GameObject _tranquilizerObject = null;
	private Tranquilizer _tranquilizer = null;

	private GameObject _keyItemObject = null;
	private KeyItem _keyItem = null;

	private static bool _tutorialFinish = false;
	public static bool TutorialFinish {
		get {
			return _tutorialFinish;
		}
	}

	private void Awake()
	{
		_playerObject = GameObject.Find("Player");
		_playerController = _playerObject.GetComponentInChildren<PlayerController>();
		
		_initialEquipmentObject = GameObject.Find("FlashLight");
		_initialEquipment = _initialEquipmentObject.GetComponent<Item>();

		_enemyObject = GameObject.Find("Enemy_tutorial");
		_enemyBrain = _enemyObject.GetComponentInChildren<ChangeTargetPoint>();
		
		_barrier = GameObject.Find("Barriers");
		
		_lampObject = GameObject.Find("Lamp_C");
		
		_lightEffectObject = GameObject.Find("Flash");
		_lightEffect = _lightEffectObject.GetComponent<Light>();
		
		_tranquilizerObject = GameObject.Find("Tranquilizer");
		_tranquilizer = _tranquilizerObject.GetComponent<Tranquilizer>();

		_keyItemObject = GameObject.Find("KeyItem");
		_keyItem = _keyItemObject.GetComponent<KeyItem>();
	}

	private void Start()
	{
		_tutorialFinish = false;
		
		_playerController.PossibleRun = false;
		
		_enemyBrain.ChasePlayer = true;
		_enemyObject.SetActive(false);
		
		_lampObject
			.ObserveEveryValueChanged(_ => _lampObject.tag)
			.Where(x => x == "Lamp_Burning")
			.Subscribe(_ => {
				TutorialStepTrigger("Lamp");
			});
		
		_flashLightText
			.ObserveEveryValueChanged(_ => _flashLightText.activeSelf)
			.Where(x => x)
			.Subscribe(_ => {
				_initialEquipment.ObjectAction();	
			});

		_lightEffect
			.ObserveEveryValueChanged(_ => _lightEffect.enabled)
			.Where(x => x)
			.First()
			.Subscribe(_ => {
				TutorialStepTrigger("FlashLight");	
			});
		
		_enemyObject
			.ObserveEveryValueChanged(_ => _enemyObject.activeSelf)
			.Skip(1)
			.Subscribe(_ => {
				TutorialStepTrigger("Enemy");
			});
		
		_tranquilizer
			.ObserveEveryValueChanged(_ => _tranquilizer.Used)
			.Where(x => x == true)
			.Subscribe(_ => {
				TutorialStepTrigger("Tranquilizer");
			});

		_keyItemObject
			.ObserveEveryValueChanged(_ => KeyItem.NowPossess)
			.Where(x => x == 1)
			.Subscribe(_ => {
				_clearText.SetActive(true);
			});
	}
	
	private void TutorialStepTrigger(string executed)
	{
		Destroy(_barrier.transform.GetChild(0).gameObject);
		
		switch (executed) {
			case "Lamp":
				_matchEndtext.SetActive(true);
				break;

			case "FlashLight":
				break;

			case "Enemy":
				if (_enemyObject.activeSelf) {
					_playerController.PossibleRun = true;
				}
				else {
					_tutorialFinish = true;
				}
				break;

			case "Tranquilizer":
				break;
		}
	}
}
