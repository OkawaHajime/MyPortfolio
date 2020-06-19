using UnityEngine;

public class BearSuicide : CloneSuicide
{
	[SerializeField] private GameObject _burstColor = null;
	
	private GameObject _bearObject = null;
	private BearAttach _bear = null;
	
	private GameObject _honeyObject = null;
	private HoneyAttach _honey = null;

	private GameObject _seObject = null;
	private SoundEffectManager _se = null;

	protected override void Initialize()
	{
		_bearObject = GameObject.Find("Bear");
		_bear = _bearObject.GetComponent<BearAttach>();

		_honeyObject = GameObject.Find("Honey");
		_honey = _honeyObject.GetComponent<HoneyAttach>();
		
		_seObject = GameObject.Find("SEManager");
		_se = _seObject.GetComponent<SoundEffectManager>();
	}

	protected override void Testament()
	{
		_se.PlayBackSound(SoundEffectManager.SOUND_TYPE.SOUND_TYPE_FAILUR);

		Instantiate(_burstColor, gameObject.transform.position, Quaternion.identity);

		if (!_honey.Attach_complete) {
			_bear.BearAnimationChange(BearAttach.BEAR.BEAR_NORMAL);
		}
		else {
			_bear.BearAnimationChange(BearAttach.BEAR.BEAR_SAD);
		}

		base.Testament();
	}
}
