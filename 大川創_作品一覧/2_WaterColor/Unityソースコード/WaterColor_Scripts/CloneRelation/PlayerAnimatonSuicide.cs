using UnityEngine;

public class PlayerAnimatonSuicide : CloneSuicide
{
	[SerializeField] private Player.ANIM_TYPE _set_anim = Player.ANIM_TYPE.ANIM_TYPE_WALK;
	private GameObject _player_Object = null;
	private Player _player = null;

	protected override void Initialize()
	{
		_player_Object = GameObject.FindGameObjectWithTag("Player");
		_player = _player_Object.GetComponent<Player>();
	}

	protected override void Testament()
	{
		_player.PlayerAnimationChange(_set_anim);
		base.Testament();
	}
}
