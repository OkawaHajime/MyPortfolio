using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]private float _speed = 0.0f;

    private GameObject _playerObject = null;
	private Transform _playerTransform = null;
	private FluctFearTrigger _fearTrigger = null;
	
	private GameObject _fovControllerObject = null;
	private Transform _fovController = null;
	
	private GameObject _itemManagerObject = null;
	private ItemManager _itemManager = null;

    private GameObject _pauseManagerObject = null;
    private PauseManager _pauseManager = null;
	
	private Vector3 _squatSpeed = new Vector3();
	private float _time = 0.0f;
	private bool _possibleRun = true;

    public float WalkSpeed {
		get{
			return _speed;
		}
		set {
			_speed = value;
		}
	}

	public bool PossibleRun {
		get {
			return _possibleRun;
		}
		set {
			_possibleRun = value;
		}
	}

	private void Awake()
	{
		_playerObject = transform.root.gameObject;
		_playerTransform = _playerObject.GetComponent<Transform>();
		_fearTrigger = _playerObject.GetComponent<FluctFearTrigger>();
		
		_fovControllerObject = GameObject.Find("PlayerFovCamera");
		_fovController = _fovControllerObject.GetComponent<Transform>();
		
		_itemManagerObject = GameObject.Find("ItemManager");
		_itemManager = _itemManagerObject.GetComponent<ItemManager>();
		
        _pauseManagerObject = GameObject.Find("PauseCanvas");
        _pauseManager = _pauseManagerObject.GetComponent<PauseManager>();
		 
		_squatSpeed.y = 0.05f;
    }

	private void Update()
	{
        //左
        if (Input.GetKey(KeyCode.A)) {
			_playerTransform.position += _playerTransform.right * _speed * -1 * Time.unscaledDeltaTime;
		}
		//右
		if (Input.GetKey(KeyCode.D)) {
			_playerTransform.position += _playerTransform.right * _speed * Time.unscaledDeltaTime;
        }
		//前
		if (Input.GetKey(KeyCode.W)) {
			_playerTransform.position += _playerTransform.forward * _speed * Time.unscaledDeltaTime;
        }
		//後
		if (Input.GetKey(KeyCode.S)) {
			_playerTransform.position += _playerTransform.forward * _speed * -1 * Time.unscaledDeltaTime;
        }

		//走り
		if (_possibleRun) {
			if (Input.GetKeyDown(KeyCode.LeftShift)) {
				_speed *= 1.9f;
			}

			if (Input.GetKey(KeyCode.LeftShift)) {
				_time += GetDeltaTime.DeltaTime();
				if (_time > 2.0f) {
					_fearTrigger.RunTime(_time);
				}
			}

			if (Input.GetKeyUp(KeyCode.LeftShift)) {
				_speed /= 1.9f;
				_time = 0.0f;
			}

		}

		//しゃがみ
		if (Input.GetKey(KeyCode.Space)) {
			if (_fovController.localPosition.y > 1.0f) {
				_fovController.localPosition -= _squatSpeed;
			}
		}
		else {
			if (_fovController.localPosition.y < 2.0f) {
				_fovController.localPosition += _squatSpeed;
			}
		}

		//アイテム捨て
		if (Input.GetKeyDown(KeyCode.Q)) {
			_itemManager.DisposeItem();
		}

		//ポーズ
		if (Input.GetKeyDown(KeyCode.Escape)) {
            _pauseManager.OpenPause();
		}
	}
}
