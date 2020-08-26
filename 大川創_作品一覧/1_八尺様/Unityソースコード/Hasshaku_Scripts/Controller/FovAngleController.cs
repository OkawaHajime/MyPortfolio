using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メインカメラに付けるScript
/// マウスでの視点操作を行う
/// </summary>
public class FovAngleController : MonoBehaviour
{
	[SerializeField] private float _rollSpeed = 1.0f;
	private GameObject _playerObject = null;
	private Transform _playerTransform = null;
	private Vector3 _playerAngle = new Vector3();
	private Vector3 _cameraAngle = new Vector3();
	private float _maxRotate = 70.0f;
	private float _tmpRotate = 0.0f;
	private float _startRotation = 0.0f;

	private void Awake()
	{
		//プレイヤーの親情報を取得
		_playerObject = transform.root.gameObject;
		_playerTransform = _playerObject.GetComponent<Transform>();

		//初期カメラアングルを保存
		_startRotation = gameObject.transform.rotation.eulerAngles.x;
	}

	private void Update()
	{
		//視野角回転
		_cameraAngle.x = Input.GetAxis("Mouse Y") * _rollSpeed * -1;
		_playerAngle.y = Input.GetAxis("Mouse X") * _rollSpeed;
		gameObject.transform.Rotate(_cameraAngle);
		_playerTransform.Rotate(_playerAngle);

		//縦方向の回転制限
		_tmpRotate += Input.GetAxis("Mouse Y") * _rollSpeed * -1;

		if (_tmpRotate >= _maxRotate) {
			gameObject.transform.localRotation = Quaternion.Euler(_startRotation + _maxRotate, 0.0f, 0.0f);
			_tmpRotate = _maxRotate;
		}

		if(_tmpRotate <= _maxRotate * -1) {
			gameObject.transform.localRotation = Quaternion.Euler(_startRotation - _maxRotate, 0.0f, 0.0f);
			_tmpRotate = _maxRotate * -1;
		}
	}
}
