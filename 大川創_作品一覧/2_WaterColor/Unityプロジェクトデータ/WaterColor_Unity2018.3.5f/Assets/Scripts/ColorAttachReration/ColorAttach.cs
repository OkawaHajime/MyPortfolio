using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タッチされたときの色によって正解か不正解を判断する親クラス
/// 正解、不正解時の処理は、このクラスを継承した子クラスで実装する
/// 子クラスのScriptを、色を付ける対象のオブジェクトに付ける
/// </summary>
public class ColorAttach : MonoBehaviour
{
	[SerializeField] private string CorrectColor = null;

	private GameObject _playerObject = null;
	protected Player _player = null;

	private GameObject _barrierObject = null;
	protected BarrierBreak _barrier = null;
    
	private GameObject _nowColorObject = null;
	protected Image _nowColor = null;

	private GameObject _colorSourceObject = null;
    protected ColorPossession _colorSource = null;
	
	private GameObject _effectGeneratorObject = null;
	private AttachEffectGenerator _effectGenerator = null;
    
	private GameObject _seObject = null;
    protected SoundEffectManager _se = null;
	
	protected BoxCollider2D box2D = null;
	protected SpriteRenderer rend = null;

	private void Awake()
	{
		Initialize();
	}

	//タッチ時にエフェクトとSEを再生する
	public void AttachStart()
	{
		if (_nowColor.sprite.name != "UIMask") { 
			box2D.enabled = false;
			_effectGenerator.EffectGenerate(gameObject.name);
            _se.PlayBackSound(SoundEffectManager.SOUND_TYPE.SOUND_TYPE_ATTACH);
            StartCoroutine(AttachTrigger());
		}
	}

	//正解か不正解で処理分岐
	private IEnumerator AttachTrigger()
	{
		//エフェクトと色が変わるタイミングを合わせるために、0.2秒待つ
		yield return new WaitForSeconds(0.2f);
		if (_nowColor.sprite.name == CorrectColor) {
			Regain();
		}
		else {
			Failure();
		}
	}

	/// <summary>
	/// 以下virtual関数
	/// 子クラスで実装する部分
	/// </summary>
 
	//情報取得
	protected virtual void Initialize()
	{
		_playerObject = GameObject.Find("Girl");
		_player = _playerObject.GetComponent<Player>();

		_barrierObject = GameObject.Find("Barrier");
		_barrier = _barrierObject.GetComponent<BarrierBreak>();
		
		_nowColorObject = GameObject.Find("NowColor");
		_nowColor = _nowColorObject.GetComponent<Image>();
        
		_colorSourceObject = GameObject.Find("Source");
        _colorSource = _colorSourceObject.GetComponent<ColorPossession>();
        
		_effectGeneratorObject = GameObject.Find("AttachEffectGenerator");
		_effectGenerator = _effectGeneratorObject.GetComponent<AttachEffectGenerator>();

		_seObject = GameObject.Find("SEManager");
        _se = _seObject.GetComponent<SoundEffectManager>();
		
		box2D = GetComponent<BoxCollider2D>();
		rend = GetComponent<SpriteRenderer>();
	}

	//正解時の処理
	protected virtual void Regain()
	{
        _colorSource.PossessionKill();
		_barrier.ChildKill();
		_player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_GLAD);
	}

	//不正解時の処理
	protected virtual void Failure() 
	{
        box2D.enabled = true;
    }

}
