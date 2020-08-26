using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 恐怖値の上昇条件を設定、条件に当てはまったら恐怖値を上昇させる
/// </summary>
public class FluctFearTrigger : MonoBehaviour
{
    [SerializeField] private Animator _volume = null;
	private Subject<int> _rangeFearSubject = new Subject<int>();
	private Subject<float> _runFearSubject = new Subject<float>();
	private Subject<float> _enemyRangeSubject = new Subject<float>();
	private Subject<float> _lookEnemySubject = new Subject<float>();
	private int _fluctNumeric = 1;
	private float _keepTime = 0.0f;
	private bool _heal = false;

	//購読可能
	public IObservable<int> RangeFluctFear {
		get {
			return _rangeFearSubject;
		}
	}

	public IObservable<float> RunTimeFluctFear {
		get {
			return _runFearSubject;
		}
	}

	public IObservable<float> EnemyRangeFluctFear {
		get {
			return _enemyRangeSubject;
		}
	}

	public IObservable<float> LookEnemyFluctFear {
		get {
			return _lookEnemySubject;
		}
	}

	//監視開始
	private void Start()
	{
		StartCoroutine(OnTimeFear());
	}

	//灯りの有無による値変動
	private IEnumerator OnTimeFear() {
		while (true) {
			yield return new WaitForSeconds(3);
			_rangeFearSubject.OnNext(_fluctNumeric);

		}
	}

	//走った時間をそのままOnNext
	public void RunTime(float runTime)
	{
		_runFearSubject.OnNext(runTime);
	}

	//敵を見ている時間をそのままOnNext
	public void LookEnemyTime(float lookTime)
	{
		_lookEnemySubject.OnNext(lookTime);
	}

    private void OnTriggerEnter(Collider other)
    {
		//敵範囲内時の画面エフェクト
        if(other.gameObject.tag == "EnemyFearArea") {
            _volume.SetTrigger("GrainOn");
        }

		//灯り内
		if (other.gameObject.tag == "HealArea") {
			_fluctNumeric *= -1;
		}
	}

    private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "EnemyFearArea") {
			_keepTime += GetDeltaTime.DeltaTime();
			if (_keepTime > 2.0f) {
				_enemyRangeSubject.OnNext(_keepTime);
			}
		}

	}

	private void OnTriggerExit(Collider other)
	{
		//灯り外
		if (other.gameObject.tag == "HealArea") {
			_fluctNumeric *= -1;
		}

		//敵範囲外
		if (other.gameObject.tag == "EnemyFearArea") {
			_keepTime = 0.0f;
            _volume.SetTrigger("GrainOff");
        }

    }
}
