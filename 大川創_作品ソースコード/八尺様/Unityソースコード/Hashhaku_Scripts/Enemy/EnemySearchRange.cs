using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private void OnTriggerStay(Collider other) 
	{
		if(other.gameObject.tag == "Player" && Input.GetKey(KeyCode.LeftShift)) {
			_enemy.SetDestination(_playerTransform.position);
		}
	}
}
