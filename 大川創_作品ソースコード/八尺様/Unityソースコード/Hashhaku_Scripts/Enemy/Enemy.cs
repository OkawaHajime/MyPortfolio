using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;

public class Enemy : MonoBehaviour
{
	[SerializeField] private float _walkSpeed = 10.0f;
	[SerializeField] private float _angularSpeed = 200.0f;
	[SerializeField] private float _turnAngle = 45.0f;
	[SerializeField] private float _turnAnguleSpeed = 1000.0f;
	[SerializeField] private float _stopDistance = 0.1f;
    [SerializeField] private AudioClip gameover = null;
	
	private GameObject _sceneChangeTriggerObject = null;
	private SceneChangeEffectTrigger _sceneChangeTrigger = null;
	
	private GameObject _patrolObject = null;
    private List<Transform> _patrolPoint = new List<Transform>();
	private Transform _point = null;
	
	private NavMeshAgent _agent = null;
	private ChangeTargetPoint _enemyBrain = null;
	
	private Subject<Unit> _enemySpawn = new Subject<Unit>();
	private Vector3 _move = new Vector3();
	private Vector3 _destination = new Vector3();
	private Vector3 _targetCorner = new Vector3();
	private float _speed = 0.0f;
	private float _rotate = 0.0f;
	private float _rotateMax = 0.0f;
	private float _angle = 0.0f;
	public Subject<Unit> EnemySpawn {
		get {
			return _enemySpawn;
		}
	}


    private AudioSource audioSource = null;

    public Vector3 LastCorner {
		get {
			if(_agent.pathPending || _agent.path.corners.Length == 0.0f) {
				return _destination;
			}
			return _agent.path.corners[_agent.path.corners.Length - 1];
        }
	}

	public float DistanceXZ(Vector3 src, Vector3 dst){
		src.y = dst.y;
		return Vector3.Distance(src, dst);
	}

	public bool IsReached {
		get {
			return DistanceXZ(LastCorner, gameObject.transform.position) <= _stopDistance;
		}
	}

	public float Speed {
		get { 
			return _walkSpeed; 
		}
		set { 
			_walkSpeed  = value; 
		}
	}

	private void Awake()
	{
		_sceneChangeTriggerObject = GameObject.Find("SceneChangeEffectTrigger");
		_sceneChangeTrigger = _sceneChangeTriggerObject.GetComponent<SceneChangeEffectTrigger>();
		
        _patrolObject = GameObject.Find("PatrolPoints");
        foreach (Transform child in _patrolObject.transform){
            _patrolPoint.Add(child);
        }
		
		_agent = GetComponent<NavMeshAgent>();
		
		_enemyBrain = gameObject.GetComponentInChildren<ChangeTargetPoint>();

        audioSource = GetComponent<AudioSource>();
    }

	private void Start()
	{
		_enemySpawn.OnNext(Unit.Default);

		//NavMeshAgentの移動、回転の無効化
		_agent.speed = 0.0f;
		_agent.angularSpeed = 0.0f;
		_agent.acceleration = 0.0f;

		Random.InitState(System.DateTime.Now.Millisecond);
		SetPatrolPoint();
	}

	private void Update()
	{

		if (_agent.pathPending) {
			_move.Set(0.0f, 0.0f, 0.0f);
		}
		else {
			//次の目標座標を確認
			_targetCorner = LastCorner;
			_speed = _walkSpeed * Time.unscaledDeltaTime;
			for (int i = 0; i < _agent.path.corners.Length; i++) {
				_targetCorner = _agent.path.corners[i];
				if(DistanceXZ(_targetCorner, gameObject.transform.position) >= _speed) {
					break;
				}
			}

			//移動方向と速度の算出
			_move = _targetCorner - gameObject.transform.position;
			_move.y = 0.0f;
			_rotate = _angularSpeed * Time.deltaTime;

			//角度と移動設定
			if (!IsReached) {
				_angle = Vector3.SignedAngle(gameObject.transform.forward, _move, Vector3.up);

				if(Mathf.Abs(_angle) > _turnAngle) {
					_rotateMax = _turnAnguleSpeed * Time.deltaTime;
					_rotate = Mathf.Min(Mathf.Abs(_angle), _rotateMax);
					gameObject.transform.Rotate(0.0f, _rotate * Mathf.Sign(_angle), 0.0f);
					_move = Vector3.zero;
					_speed = 0.0f;
				}
				else {
					if(_move.magnitude < _speed) {
						_speed = _move.magnitude;
						_rotate = _angle;
						gameObject.transform.Rotate(0.0f, _angle, 0.0f);
					}
					else {
						_rotate = Mathf.Min(Mathf.Abs(_angle), _rotate);
						gameObject.transform.Rotate(0.0f, _rotate * Mathf.Sign(_angle), 0.0f);
					}

					_move = gameObject.transform.forward * _speed;
				}
			}
			else {
				//次の巡回ポイントの設定
				_speed = 0.0f;
				_move = LastCorner - gameObject.transform.position;
				_move.y = 0.0f;

				if (_enemyBrain.Chasing) {
					_enemyBrain.PatrolMode();
				}
				SetPatrolPoint();
			}

            //移動
			gameObject.transform.position += _move;
		}
	}

	public void SetPatrolPoint()
	{
		_point = _patrolPoint[Random.Range(0, _patrolPoint.Count)];
		SetDestination(_point.position);
	}

	public void SetDestination(Vector3 position)
	{
		_destination = position;
		_agent.SetDestination(position);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player") {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
            audioSource.PlayOneShot(gameover,0.5f);
			collision.gameObject.tag = "Gameover";
			_sceneChangeTrigger.changeScene("GameOver");
		}
	}

    
}

