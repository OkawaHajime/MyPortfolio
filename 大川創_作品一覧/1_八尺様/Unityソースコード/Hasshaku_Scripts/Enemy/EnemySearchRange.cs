using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の探知範囲
/// 探知範囲内でプレイヤーが走ると最後に走っていた地点まで移動する
/// </summary>
public class EnemySearchRange : MonoBehaviour
{
	private GameObject _playerObject = null;
	private Transform _playerTransform = null;

	private GameObject _enemyObject = null;
	private Enemy _enemy = null;

	private void Awake() 
	{
		_playerObject = GameObject.Find("Player");
		_playerTransform = _playerObject.GetComponent<Transform>();

		_enemyObject = transform.root.gameObject;
		_enemy = _enemyObject.GetComponent<Enemy>();
	}

	//Collider関連
	private void OnTriggerStay(Collider other) 
	{
		if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.LeftShift)) {
			_enemy.SetDestination(_playerTransform.position);
		}
	}
}
