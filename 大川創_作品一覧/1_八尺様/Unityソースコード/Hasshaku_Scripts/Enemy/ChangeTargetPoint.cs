using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の追跡判断を行う
/// </summary>
public class ChangeTargetPoint : MonoBehaviour 
{
	[SerializeField]private float _chaseMag = 1.2f;

    private GameObject _playerObject = null;
    private Transform _playerTransform = null;
    
    private GameObject _enemyObject = null;
    private Animator _enemyAnim = null;
    private Enemy _enemy = null;
    
    private GameObject _volumeObject = null;
    private Animator _volume = null;

    private GameObject _BGMStreamer = null;
    private GameBGM _gameBGM = null;
    
	private RaycastHit _hit = new RaycastHit();
    private bool _chase = false;
    private bool _fearLevelMax = false;
    private bool _look = false;

    public bool Chasing {
		get {
			return _chase;
		}
	}

	public bool ChasePlayer {
		get {
			return _fearLevelMax;
		}
		set {
			_fearLevelMax = value;
		}
	} 

	private void Awake()
	{
		//情報取得
		_playerObject = GameObject.Find("Player");
		_playerTransform = _playerObject.GetComponent<Transform>();

		_enemyObject = transform.root.gameObject;
		_enemy = _enemyObject.GetComponent<Enemy>();
		_enemyAnim = _enemyObject.GetComponent<Animator>();

        _volumeObject = GameObject.Find("PostVolume");
        _volume = _volumeObject.GetComponent<Animator>();
    }

	private void Update()
	{
		//恐怖度レベルによって分岐
		if (!_fearLevelMax) {
			//敵の視界内で、敵とプレイヤーの間に障害物がなければ追跡する
			if (_look) {
                Physics.Linecast(gameObject.transform.position, _playerTransform.position, out _hit);
				if (_hit.collider.gameObject.tag == "Player") {
					_enemy.SetDestination(_playerTransform.position);
					if (!_chase) {
						ChaseMode();
					}
				}
			}
		}
		else {
			//常にプレイヤーを追跡する
			_enemy.SetDestination(_playerTransform.position);
			if (!_chase) {
				ChaseMode();
			}
		}
	}

	//プレイヤー追跡状態
	private void ChaseMode()
	{
		_chase = true;
		_enemyAnim.SetBool("Chase", true);
        _volume.SetTrigger("StartChase");
		_enemy.Speed *= _chaseMag;
	}

	//ステージ巡回状態
	public void PatrolMode()
	{
		_chase = false;
		_enemyAnim.SetBool("Chase", false);
        _volume.SetTrigger("StopChase");
        _enemy.Speed /= _chaseMag;
	}

	//視界内に入る
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			_look = true;
		}
	}

	//視界内から出る
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			_look = false;
		}
	}
}
