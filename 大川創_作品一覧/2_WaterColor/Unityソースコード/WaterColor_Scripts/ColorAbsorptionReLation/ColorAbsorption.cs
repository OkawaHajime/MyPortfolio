using UnityEngine;
using UnityEngine.UI;

public class ColorAbsorption : MonoBehaviour
{
	[SerializeField] private Sprite Absorb = null;
	[SerializeField] private GameObject DeathEffect = null;
	[SerializeField] private float speed = 2500.0f;

	private GameObject _playerObject = null;
	private Transform _target = null;
	
	private GameObject _mouseControllerObject = null;
	private MouseController _mouseController = null;
	
	private GameObject _nowColorObject = null;
	private Image _nowColor = null;
	
    private GameObject _seObject = null;
    private SoundEffectManager _se = null;
	
	private GameObject _sourceParentObject = null;
	private ColorPossession _sourceParent = null;
	
	private BoxCollider2D _sourceCollider = null;
	private Animator _sourceAnimator = null;
	private float step = 0.0f;
	
	private void Awake()
	{
		Initialise();
	}
	
	protected virtual void Initialise()
	{
		_playerObject = GameObject.Find("Girl");
		_target = _playerObject.GetComponent<Transform>();
		
		_mouseControllerObject = GameObject.Find("MouseController");
		_mouseController = _mouseControllerObject.GetComponent<MouseController>();
        
		_nowColorObject = GameObject.Find("NowColor");
		_nowColor = _nowColorObject.GetComponent<Image>();
		
		_seObject = GameObject.Find("SEManager");
        _se = _seObject.GetComponent<SoundEffectManager>();
		
		_sourceParentObject = transform.root.gameObject;
		_sourceParent = _sourceParentObject.GetComponent<ColorPossession>();
		
		_sourceCollider = GetComponent<BoxCollider2D>();
		_sourceAnimator = GetComponent<Animator>();
	}

	private void Start()
	{
		_sourceAnimator.enabled = true;
		enabled = false;
	}

	private void Update()
	{
		step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, _target.position, step);
	}

	public virtual void AbsorbStart()
	{
		_sourceAnimator.enabled = false;
		enabled = true;
        _se.PlayBackSound(SoundEffectManager.SOUND_TYPE.SOUND_TYPE_GET);
	}

	private void AbsorbEnd()
	{
		Instantiate(DeathEffect, gameObject.transform.position, Quaternion.identity);
		_nowColor.sprite = Absorb;
		_mouseController.enabled = true;
		_sourceCollider.enabled = false;
		_sourceParent.GetColor(gameObject);
        _se.PlayBackSound(SoundEffectManager.SOUND_TYPE.SOUND_TYPE_FAILUR);
	}

	public void TurnBack()
	{
		gameObject.SetActive(true);
		_sourceAnimator.enabled = true;
		_sourceAnimator.SetBool(gameObject.name, true);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "PlayerCenter") {
			AbsorbEnd();
		}
	}
}
