using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDefense : MonoBehaviour
{
    private int _enemyHP = 50;
    private int _enemyDefense = 50;
    private static int _playerLevelDown = 1;
	public static int PlayerLevelDown {
		set {
			_playerLevelDown = value;
		}
	}

    private GameObject _semanagerObject = null;
    private SEManager _semanager = null;
    
    private GameObject _levelManagerObject = null;
    private LevelManager _levelManager = null;

    private void Awake()
    {
        _levelManagerObject = GameObject.Find("LevelManager");
        _levelManager = _levelManagerObject.GetComponent<LevelManager>();
        
        _semanagerObject = GameObject.Find("SEManager");
        _semanager = _semanagerObject.GetComponent<SEManager>();
    }

    public void Damage(int _playerAttack)
    {
        var damage = _enemyDefense - _playerAttack;
        
		if (damage <= 0) {
            _enemyHP += damage;
            if (_enemyHP <= 0) {
                _levelManager.LevelDown(_playerLevelDown);
                _semanager.SEEnemyDie();
                Destroy(gameObject);
            }
        }
    }
}
