using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class LevelManager : MonoBehaviour
{
	[SerializeField] private Text _levelUI = null;
	[SerializeField] private Text _attackUI = null;
	[SerializeField] private Text _difenseUI = null;
    private GameObject _scenechange_obj;
    private SceneChange _scenechange_sri;

    private enum PHASE
    {
        PHASE_1,
        PHASE_2
    }

    private int _level = 100;
    private Subject<int> _currentLevel = new Subject<int>();

    private GameObject _playerObject = null;
    private Player _player = null;

    private GameObject _enemyObject = null;
    private EnemyDefense _enemy = null;

    private void Awake()
    {
        _playerObject = GameObject.Find("Player");
        _player = _playerObject.GetComponentInChildren<Player>();
       
        _scenechange_obj = GameObject.Find("SceneChange");
        _scenechange_sri = _scenechange_obj.GetComponent<SceneChange>();
    }

    private void Start()
    {
        _currentLevel
            .Where(level => level <= 85)
            .First()
            .Subscribe(_ => {
                LevelEffect(PHASE.PHASE_1);
            });

        _currentLevel
            .Where(level => level <= 25)
            .First()
            .Subscribe(_ => {
                LevelEffect(PHASE.PHASE_2);
            });
    }

    public void LevelDown(int levelDown)
    {
        _level -= levelDown;
		_levelUI.text = _level.ToString();
        _currentLevel.OnNext(_level);
        if (_level <= 1) {
            _scenechange_sri.SceneChange2();
        }
    }

    private void LevelEffect(PHASE phase)
    {
        switch (phase) {
            case PHASE.PHASE_1:
				EnemyDefense.PlayerLevelDown = 3;
				_player.Attack = 75;
				_player.Difense = 45;

				_attackUI.text = "：普通";
				_difenseUI.text = "：普通";
                break;

            case PHASE.PHASE_2:
				EnemyDefense.PlayerLevelDown = 4;
				_player.Attack = 63;
				_player.Difense = 10;

				_attackUI.text = "：最弱";
				_difenseUI.text = "：最弱";
				break;
        }
    }
}
