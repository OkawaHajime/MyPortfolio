using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Locker : MonoBehaviour
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

	private void Awake()
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
                if (_currentLevel == FearManager.FEAR_LEVEL.FEAR_LEVEL_2){
                    LoadEnemyInfo();
                }
                if (_currentLevel == FearManager.FEAR_LEVEL.FEAR_LEVEL_4){
                    ToDecideHidden();
                }
            });

        //プレイヤー隠れる際に移動させる位置を格納
        _hide = _hidePoint.transform.position;
	}

	private void LoadEnemyInfo()
	{
		_enemyObject = GameObject.FindGameObjectWithTag("Enemy");
		
		if (_enemyObject != null) {
			_enemy = _enemyObject.GetComponent<Enemy>();
			_enemyBrain = _enemyObject.GetComponentInChildren<ChangeTargetPoint>();
		}
	}

	private void ToDecideHidden()
	{
		//恐怖度レベルが４になった時に隠れていた場合、追ってこなくする
		if (_playerObject.tag == "HideArea"){
			_enemyBrain.ChasePlayer = false;
			_enemy.SetPatrolPoint();
		}
	}

	public void HiddenLocker()
	{
		_lockerDoorAnim.SetTrigger("Hide");
		
        _audioSource.PlayOneShot(_locker);
		
        _lockerDoorCollition.isTrigger = true;
        _lockerParentCollision.enabled = false;
		
        _playerObject.tag = "HideArea";
        _hide.y = _playerObject.transform.position.y;
		_playerObject.transform.position = _hide;
		_playerObject.transform.rotation = gameObject.transform.rotation;
		_playerController.enabled = false;
		
		if (_enemyObject != null){
			if (_enemyBrain.Chasing) {
				_enemyBrain.PatrolMode();
				_enemy.SetPatrolPoint();
			}
			if (_enemyBrain.ChasePlayer) {
				_enemyBrain.ChasePlayer = false;
			}
		}
	}

	public void ExitLocker()
	{
		_lockerDoorAnim.SetTrigger("Hide");
		
        _audioSource.PlayOneShot(_locker);

		_lockerDoorCollition.isTrigger = false;
        _lockerParentCollision.enabled = true;
		
        _playerController.enabled = true;
        _playerObject.tag = "Player";
        _playerObject.transform.position += _playerObject.transform.forward;
		
		if (_currentLevel == FearManager.FEAR_LEVEL.FEAR_LEVEL_4) {
			_enemyBrain.ChasePlayer = true;
		}
	}
}
