using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemy_prefab = null;
    [SerializeField] private float _spawn_interval = 3.0f;
    
	private GameObject _enemy = null;
    private float _timer = 0.0f;

	private void Start()
	{
		_enemy = Instantiate(_enemy_prefab, transform.position, transform.rotation);
		_enemy.name = _enemy_prefab.name;
	}

	private void Update()
    {
		if (_enemy == null) {
			_timer += Time.deltaTime;
			if (_spawn_interval <= _timer) {
			    _enemy = Instantiate(_enemy_prefab, transform.position, transform.rotation);
				_enemy.name = _enemy_prefab.name;
			    _timer = 0.0f;
			}
		}
    }
}
