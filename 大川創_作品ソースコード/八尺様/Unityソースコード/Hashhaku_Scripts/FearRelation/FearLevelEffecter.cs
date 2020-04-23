using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class FearLevelEffecter : MonoBehaviour
{
    [SerializeField]private FearManager _fearManager = null;
    [SerializeField]private Animator _volumeTest = null;
	[SerializeField]private GameObject _playerObject = null;
	[SerializeField]private GameObject _enemyObject = null;
	[SerializeField]private Transform _enemySpawnPoint = null;
	private GameObject _enemyClone = null;
	private GameObject _chaseEnemy = null;
    private GameObject _sceneChangeObject = null;
	private PlayerController _player = null;
	private ChangeTargetPoint _enemyScript = null;
    private SceneChangeEffectTrigger _sceneChange = null;
	private float _startSpeed = 0.0f;

	private void Awake()
	{
		_player = _playerObject.GetComponentInChildren<PlayerController>();
		_startSpeed = _player.WalkSpeed;

        _sceneChangeObject = GameObject.Find("SceneChangeEffectTrigger");
        _sceneChange = _sceneChangeObject.GetComponent<SceneChangeEffectTrigger>();
    }

	private void Start()
    {
        _fearManager.FearLevel
			.Subscribe(level => {
				FearLevelEffect(level);
			});
    }

	private void FearLevelEffect(FearManager.FEAR_LEVEL currentLevel)
	{
		switch (currentLevel) {
			case FearManager.FEAR_LEVEL.FEAR_LEVEL_1:
                FirstLevelEffect();
                break;

			case FearManager.FEAR_LEVEL.FEAR_LEVEL_2:
                SecondLevelEffect();
                break;

			case FearManager.FEAR_LEVEL.FEAR_LEVEL_3:
                ThirdLevelEffect();
                break;

			case FearManager.FEAR_LEVEL.FEAR_LEVEL_4:
                FourthLevelEffect();
                break;

            case FearManager.FEAR_LEVEL.PLAYER_DIE:
                PlayerDie();
                break;

		}
	}

	private void FirstLevelEffect()
	{
		if (_enemyClone) {
			Destroy(_enemyClone);
            _volumeTest.SetTrigger("FearLevelDown");
		}
	}

	private void SecondLevelEffect()
	{
        _player.WalkSpeed = _startSpeed;
		if (_enemyClone == null) {
			_enemyClone = Instantiate(_enemyObject, _enemySpawnPoint.position, Quaternion.identity);
			_enemyClone.name = _enemyObject.name;
			_enemyScript = _enemyClone.GetComponentInChildren<ChangeTargetPoint>();
            _volumeTest.SetTrigger("FearLevelUp");
		}
    }

	private void ThirdLevelEffect()
	{
		_player.WalkSpeed *= 0.8f;
		_player.PossibleRun = true;
		_enemyScript.ChasePlayer = false;
	}

	private void FourthLevelEffect()
	{
		_player.PossibleRun = false;
		_enemyScript.ChasePlayer = true;
	}

    private void PlayerDie()
    {
        Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		if(_player.gameObject.tag != "Gameover") {
			_player.gameObject.tag = "Gameover";
			_sceneChange.changeScene("GameOver");
		}
    }
}
