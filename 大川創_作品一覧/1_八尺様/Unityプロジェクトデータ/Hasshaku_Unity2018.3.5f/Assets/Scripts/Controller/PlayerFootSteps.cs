using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{

    public enum PlayerState
    {
        Idle, Walk, Run
    }

	[SerializeField] private AudioClip[ ] _footSteps = null;
	[SerializeField] private float _speed = 0.0f;
    [SerializeField] private float _minPitch = 0.0f;
    [SerializeField] private float _maxPitch = 0.0f;
  
	private AudioSource _audioSource = null;

	[HideInInspector] public float _footSoundDelay;
    [HideInInspector] public PlayerState _playerState;

    // Start is called before the first frame update
    void Start()
    {
		_audioSource = GetComponent<AudioSource>( );
		StartCoroutine( PlayerFootSound( ) );
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
            _playerState = PlayerState.Walk;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _playerState = PlayerState.Run;
            }
        }
        else
        {
            _playerState = PlayerState.Idle;
        }

		
	}

	//足音
	public IEnumerator PlayerFootSound( ) {
		while ( true ) {
			if(_playerState != PlayerState.Idle)
            {
                _audioSource.PlayOneShot(_footSteps[0]);
                _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
                _footSoundDelay = 2.6f / _speed;

            }

            if (_playerState == PlayerState.Run)
            {
                _audioSource.PlayOneShot(_footSteps[0]);
                _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
                _footSoundDelay = 2.4f / _speed;
            }

			yield return new WaitForSeconds( _footSoundDelay );
		}
	}
}
