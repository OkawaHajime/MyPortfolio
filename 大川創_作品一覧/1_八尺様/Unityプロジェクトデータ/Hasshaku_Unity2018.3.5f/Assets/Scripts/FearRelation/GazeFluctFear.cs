using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeFluctFear : MonoBehaviour
{
	[SerializeField]private Transform _enemy = null;
	[SerializeField]private Renderer _enemyRend = null;
	private GameObject _playerObject = null;
	private RaycastHit _hit = new RaycastHit();
	private FluctFearTrigger _fearTrigger = null;
	private float _lookTime = 0.0f;
	private bool _plusTime = false;

	private void Awake()
	{
		_playerObject = transform.root.gameObject;
		_fearTrigger = _playerObject.GetComponent<FluctFearTrigger>();
	}

	private void Update()
	{
		if (_enemyRend.isVisible) {
			Physics.Linecast(gameObject.transform.position, _enemy.position, out _hit);
			
			if (_hit.collider.gameObject.tag == "Enemy") {
				_lookTime += GetDeltaTime.DeltaTime();
				
				if(_lookTime > 1.0f) {
					_fearTrigger.LookEnemyTime(_lookTime);
				}

				if (!_plusTime) {
					_plusTime = true;
				}
			}
			else {
				if (_plusTime) {
					_plusTime = false;
					_lookTime = 0.0f;
				}
			}
		}
	}
}
