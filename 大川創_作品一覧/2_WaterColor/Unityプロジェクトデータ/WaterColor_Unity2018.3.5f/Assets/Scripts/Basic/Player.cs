using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーに付けるScript
/// 主にプレイヤーの移動とアニメーションの切り替えを行う
/// </summary>
public class Player : MonoBehaviour
{
	private enum ACTION_TYPE {
		ACTION_TYPE_WALK,
		ACTION_TYPE_STOP,
	};

	public enum ANIM_TYPE {
		ANIM_TYPE_WALK,
		ANIM_TYPE_BOTHERED,
		ANIM_TYPE_WAIT,
		ANIM_TYPE_GLAD,
		ANIM_TYPE_SURPRISE,
        ANIM_TYPE_SPECIAL,
	};

	[SerializeField] private float _walk_speed = 300.0f;
	[SerializeField] private ReactionEffectGenerator _reactionEffect = null;
	[SerializeField] private List<GameObject> _player_animes = new List<GameObject>();
	
	private GameObject _clone = null;

	private Rigidbody2D _rigidbody2D = null;
	private ACTION_TYPE _action_type = ACTION_TYPE.ACTION_TYPE_WALK;
	private float _time = 0.0f;
	private string loopEnd = null;

	private void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();

		//アニメーションの初期設定
		loopEnd = _player_animes[(int)ANIM_TYPE.ANIM_TYPE_GLAD].name;
		PlayerAnimationChange(ANIM_TYPE.ANIM_TYPE_WALK);
	}

	private void Update()
	{
		//プレイヤー移動
		switch (_action_type) {
			case ACTION_TYPE.ACTION_TYPE_WALK:
				Walk();
				break;

			case ACTION_TYPE.ACTION_TYPE_STOP:
				Stop();
				break;
		}
	}

	//移動制御
	private void Walk()
	{
		_rigidbody2D.velocity = new Vector2(_walk_speed, _rigidbody2D.velocity.y);
	}

	private void Stop()
	{
		_rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
	}

	//アニメーション切り替え
	public void PlayerAnimationChange(ANIM_TYPE _changeAnim)
	{
		ResetAnimation();

		switch (_changeAnim) {
			case ANIM_TYPE.ANIM_TYPE_WALK:
				SetWalkAnimation();
				break;

			case ANIM_TYPE.ANIM_TYPE_BOTHERED:
				SetBotheredAnimation();
				break;

			case ANIM_TYPE.ANIM_TYPE_WAIT:
				SetWaitAnimation();
				break;

			case ANIM_TYPE.ANIM_TYPE_GLAD:
				SetGladAnimation();
				break;

			case ANIM_TYPE.ANIM_TYPE_SURPRISE:
				SetSurpriseAnimation();
				break;

            case ANIM_TYPE.ANIM_TYPE_SPECIAL:
                SetSpecialAnimation();
                break;
		}
	}

	/*リアクション系のアニメーションはInstantiateで再生を行うため、
	   WALK～WAITまでのactiveをfalseにする*/
	private void ResetAnimation()
	{
		foreach (GameObject player_anime in _player_animes) {
			if (player_anime.name == loopEnd) {
				break;
			}
			player_anime.SetActive(false);
		}
	}

	/*アニメーション切り替え
      (SpriteStudioでの切り替え方法がわからなかったため、GameObjectのactive状態で切り替えを行う)*/
	
	//歩き
	private void SetWalkAnimation()
	{
		_player_animes[(int)ANIM_TYPE.ANIM_TYPE_WALK].SetActive(true);
		_action_type = ACTION_TYPE.ACTION_TYPE_WALK;
	}

	//悩み
	private void SetBotheredAnimation()
	{
		_reactionEffect.ReactionGenerate(ReactionEffectGenerator.REACTION_TYPE.REACTION_TYPE_QUESTION);
		_player_animes[(int)ANIM_TYPE.ANIM_TYPE_BOTHERED].SetActive(true);
		_action_type = ACTION_TYPE.ACTION_TYPE_STOP;
	}

	//直立
	private void SetWaitAnimation()
	{
		_player_animes[(int)ANIM_TYPE.ANIM_TYPE_WAIT].SetActive(true);
		_action_type = ACTION_TYPE.ACTION_TYPE_STOP;
	}

	/*リアクション系のアニメーションはactive状態の切り替えで行うと
	  即座にリアクションを行わない場合があったため、Instantiateで再生する*/
	
	//喜び
	private void SetGladAnimation()
	{
		_reactionEffect.ReactionGenerate(ReactionEffectGenerator.REACTION_TYPE.REACTION_TYPE_GLAD);
		_clone = (GameObject)Instantiate(_player_animes[(int)ANIM_TYPE.ANIM_TYPE_GLAD], gameObject.transform.position, Quaternion.identity);
		_clone.transform.parent = gameObject.transform;
		_action_type = ACTION_TYPE.ACTION_TYPE_STOP;
	}

	//驚き
	private void SetSurpriseAnimation()
	{
		_reactionEffect.ReactionGenerate(ReactionEffectGenerator.REACTION_TYPE.REACTION_TYPE_SURPRISE);
		_clone = (GameObject)Instantiate(_player_animes[(int)ANIM_TYPE.ANIM_TYPE_SURPRISE], gameObject.transform.position, Quaternion.identity);
		_clone.transform.parent = gameObject.transform;
		_action_type = ACTION_TYPE.ACTION_TYPE_STOP;
	}

	//特殊（ステージクリア時などのリアクション）
    private void SetSpecialAnimation()
    {
        _clone = (GameObject)Instantiate(_player_animes[(int)ANIM_TYPE.ANIM_TYPE_SPECIAL], gameObject.transform.position, Quaternion.identity);
        _clone.transform.parent = gameObject.transform;
        _action_type = ACTION_TYPE.ACTION_TYPE_WALK;
    }

	//Collider関連
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Barrier") {
			PlayerAnimationChange(ANIM_TYPE.ANIM_TYPE_BOTHERED);
		}

	}
}
