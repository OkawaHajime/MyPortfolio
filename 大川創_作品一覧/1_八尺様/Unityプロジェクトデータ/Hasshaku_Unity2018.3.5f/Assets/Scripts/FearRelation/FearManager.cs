using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class FearManager : MonoBehaviour
{
    public enum FEAR_LEVEL
    {
        FEAR_LEVEL_1,
        FEAR_LEVEL_2,
        FEAR_LEVEL_3,
        FEAR_LEVEL_4,
        PLAYER_DIE,
    };
	
	[SerializeField] private FluctFearTrigger _fearTrigger = null;
	[SerializeField] private ParticleSystem _healEffect = null;
	[SerializeField] private int _rangeFear = 5;
	[SerializeField] private int _runTimeFear = 5;
	[SerializeField] private int _enemyRangeFear = 15;
	[SerializeField] private int _lookEnemyFear = 15;
    [SerializeField] AudioClip[] heartSounds = null;

    private Subject<FEAR_LEVEL> _fearLevelSubject = new Subject<FEAR_LEVEL>();
    private FEAR_LEVEL _currentLevel = FEAR_LEVEL.FEAR_LEVEL_1;
    private int _fearValue = 0;
    private int _levelFluct = 0;
    private AudioSource audioSource = null;

    public IObservable<FEAR_LEVEL> FearLevel
    {
        get{
            return _fearLevelSubject;
        }
    }

    private void Start()
    {
        //通常恐怖値
		_fearTrigger.RangeFluctFear
			.Subscribe(numeric => {
				UpFear(_rangeFear * numeric);
			});

        //走行恐怖値
		_fearTrigger.RunTimeFluctFear
			.Select(time => (int)time)
			.DistinctUntilChanged()
			.Where(time => time % 3 == 0)
			.Subscribe(time => {
				UpFear(_runTimeFear);
			});

        //エネミー索敵範囲内
		_fearTrigger.EnemyRangeFluctFear
			.Select(time => (int)time)
			.DistinctUntilChanged()
			.Where(time => time % 3 == 0)
			.Subscribe(time => {
				UpFear(_enemyRangeFear);
			});

        //エネミー視認
		_fearTrigger.LookEnemyFluctFear
			.Select(time => (int)time)
			.DistinctUntilChanged()
			.Where(time => time % 2 == 0)
			.Subscribe(time => {
				UpFear(_lookEnemyFear);	
			});

        audioSource = GetComponent<AudioSource>();
    }

    private void UpFear(int _plusNumeric)
    {
		if (_plusNumeric < 0) {
			_healEffect.Play();
		}

        _fearValue += _plusNumeric;

		if (_fearValue < 0) {
			_fearValue = 0;
		}

        //恐怖値上限に達した時、恐怖度段階を上昇させる
        if (_fearValue >= 100) {
            audioSource.PlayOneShot(heartSounds[0],1.5f);
            if ((int)_currentLevel < 4) {
                _fearValue = 0;
                _levelFluct += 1;
                _currentLevel = (FEAR_LEVEL)_levelFluct;
                _fearLevelSubject.OnNext(_currentLevel);
                Debug.Log(_currentLevel);
            }
            else {
                _fearValue = 100;
            }
        }
	}

	public void DownFear()
	{
		_healEffect.Play();

		if (_levelFluct != 0) {
            //恐怖度段階が１より大きい時に恐怖度段階を１段階下げる
			_fearValue = 50;
			_levelFluct -= 1;
			_currentLevel = (FEAR_LEVEL)_levelFluct;
			_fearLevelSubject.OnNext(_currentLevel);
			Debug.Log(_currentLevel);
		}
		else {
			if (_fearValue > 50) {
				_fearValue = 50;
			}
			else {
				_fearValue = 0;
			}
		}

        audioSource.PlayOneShot(heartSounds[1],1.5f);
	}
}
