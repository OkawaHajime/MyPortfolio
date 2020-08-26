using UnityEngine;

/// <summary>
/// ハナカマキリのリアクションアニメーションの削除を行う
/// </summary>
public class FlowerMantisSuicide : CloneSuicide 
{
	[SerializeField] private GameObject _burstColor = null;
	
	private GameObject _playerObject = null;
	private Player _player = null;

	private GameObject _flowerMantisObject = null;
	private FlowerMantisAttach _flowerMantis = null;
	
	private GameObject _seObject = null;
	private SoundEffectManager _se = null;

	protected override void Initialize()
	{
		_playerObject = GameObject.FindGameObjectWithTag("Player");
		_player = _playerObject.GetComponent<Player>();
		
		_flowerMantisObject = GameObject.Find("FlowerMantis");
		_flowerMantis = _flowerMantisObject.GetComponent<FlowerMantisAttach>();
		
		_seObject = GameObject.Find("SEManager");
		_se = _seObject.GetComponent<SoundEffectManager>();
	}

	//削除のタイミングでエフェクトの生成、ハナカマキリの状態を元に戻す
	protected override void Testament()
	{
		_se.PlayBackSound(SoundEffectManager.SOUND_TYPE.SOUND_TYPE_FAILUR);

		Instantiate(_burstColor, gameObject.transform.position, Quaternion.identity);

		_player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_BOTHERED);
		_flowerMantis.SetAnimation(FlowerMantisAttach.FLOWERMANTIS.FLOWERMANTIS_INTIMIDATION);
		
		base.Testament();
	}
}
