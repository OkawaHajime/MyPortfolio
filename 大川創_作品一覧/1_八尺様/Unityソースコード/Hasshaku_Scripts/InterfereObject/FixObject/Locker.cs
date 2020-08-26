using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// InterfereObjectを継承
/// 調べた際に、プレイヤーをロッカーの中へ移動させ、敵からの追跡を終了させる
/// </summary>
public class Locker : InterfereObject
{
    private GameObject _lockerObject = null;
    private MeshCollider _lockerParentCollision = null;

	private GameObject _hidePoint = null;
    private Vector3 _hide = new Vector3();
    
	private Animator _lockerDoorAnim = null;
	private BoxCollider _lockerDoorCollition = null;

    private GameObject _enemyObject = null;
    private Enemy _enemy = null;
    private ChangeTargetPoint _enemyBrain = null;

    private GameObject _playerObject = null;
    private PlayerController _playerController = null;

    private GameObject _fearManagerObject = null;
    private FearManager _fearManager = null;
    private FearManager.FEAR_LEVEL _currentLevel = new FearManager.FEAR_LEVEL();

    [SerializeField] private AudioClip _locker = null;
    private AudioSource _audioSource = null;
	private Subject<Unit> _enemys = new Subject<Unit>();

	protected override void Initialize()
	{
		_playerObject = GameObject.Find("Player");
		_playerController = _playerObject.GetComponentInChildren<PlayerController>();

		_fearManagerObject = GameObject.Find("FearManager");
		_fearManager = _fearManagerObject.GetComponent<FearManager>();

		_lockerObject = transform.parent.gameObject;
		_hidePoint = _lockerObject.transform.GetChild(1).gameObject;
		_lockerParentCollision = _lockerObject.GetComponent<MeshCollider>();
		_lockerDoorCollition = GetComponent<BoxCollider>();
		_lockerDoorAnim = GetComponent<Animator>();

		LoadEnemyInfo();

		_audioSource = GetComponent<AudioSource>();
	}

	private void Start()
    {
        //恐怖度レベルの増減を監視
        _fearManager.FearLevel
            .Subscribe(level => {
                _currentLevel = level;
                if (_currentLevel == FearManager.FEAR_LEVEL.FEAR_LEVEL_2) {
                    LoadEnemyInfo();
                }
                if (_currentLevel == FearManager.FEAR_LEVEL.FEAR_LEVEL_4) {
                    ToDecideHidden();
                }
            });

        //プレイヤー隠れる際に移動させる位置を格納
        _hide = _hidePoint.transform.position;
	}

	//敵がステージ上にいたら情報取得
	private void LoadEnemyInfo()
	{
		_enemyObject = GameObject.FindGameObjectWithTag("Enemy");
		
		if (_enemyObject != null) {
			_enemy = _enemyObject.GetComponent<Enemy>();
			_enemyBrain = _enemyObject.GetComponentInChildren<ChangeTargetPoint>();
		}
	}

	//恐怖度レベルが４になった時に隠れていた場合、追ってこなくする
	private void ToDecideHidden()
	{
		if (_playerObject.tag == "HideArea") {
			_enemyBrain.ChasePlayer = false;
			_enemy.SetPatrolPoint();
		}
	}

	public override void ObjectAction()
	{
		//ロッカーから出る
		if (_playerObject.tag == "HideArea") {
			ExitLocker();
		}
		//ロッカーに入る
		else {
			HiddenLocker();
		}
	}

	//ロッカーに隠れる
	private void HiddenLocker()
	{
		//ロッカーアニメーションとSEを再生、Colliderの調整
		_lockerDoorAnim.SetTrigger("Hide");
        _audioSource.PlayOneShot(_locker);
        _lockerDoorCollition.isTrigger = true;
        _lockerParentCollision.enabled = false;
		
		//プレイヤーのタグを変更し追跡対象から外す、ロッカーの中に位置を移動
        _playerObject.tag = "HideArea";
        _hide.y = _playerObject.transform.position.y;
		_playerObject.transform.position = _hide;
		_playerObject.transform.rotation = gameObject.transform.rotation;

		//走り状態を解除してからキーボードを操作不能にする
		if (_playerController.Running) {
			_playerController.Running = false;
			_playerController.WalkSpeed /= _playerController.RunSpeed;
		}
		_playerController.enabled = false;
		
		//敵の追跡状態を解除
		if (_enemyObject != null) {
			if (_enemyBrain.Chasing) {
				_enemyBrain.PatrolMode();
				_enemy.SetPatrolPoint();
			}
			if (_enemyBrain.ChasePlayer) {
				_enemyBrain.ChasePlayer = false;
			}
		}
	}

	//ロッカーから出る
	private void ExitLocker()
	{
		//ロッカーアニメーションとSEを再生、Colliderの調整
		_lockerDoorAnim.SetTrigger("Hide");
        _audioSource.PlayOneShot(_locker);
		_lockerDoorCollition.isTrigger = false;
        _lockerParentCollision.enabled = true;
		
		//キーボードを操作可能にし、そのタイミングで左Shiftを押していたら走り状態に移行
        _playerController.enabled = true;
		if (Input.GetKey(KeyCode.LeftShift)) {
			_playerController.WalkSpeed *= _playerController.RunSpeed;
		}

		//プレイヤーのタグと位置を戻す
        _playerObject.tag = "Player";
        _playerObject.transform.position += _playerObject.transform.forward;
		
		//恐怖度段階が4の時、敵を追跡状態に
		if (_currentLevel == FearManager.FEAR_LEVEL.FEAR_LEVEL_4) {
			_enemyBrain.ChasePlayer = true;
		}
	}
}
