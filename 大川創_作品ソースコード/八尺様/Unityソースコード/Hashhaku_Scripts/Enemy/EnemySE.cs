using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySE : MonoBehaviour
{

    public enum EnemyState {
        Patrol, Chase
    }


	[SerializeField] private AudioClip[ ] _footSteps = null;
	[SerializeField] private AudioClip[ ] _voices = null;
    [SerializeField] private float _minPitch = 0.0f;
    [SerializeField] private float _maxPitch = 0.0f;
    [SerializeField] private ChangeTargetPoint _enemyBrain = null;

    [HideInInspector] public float _footSoundDelay;
    [HideInInspector] public EnemyState _enemyState;

	private AudioSource _audioSource = null;
	
    private void Awake()
    {
		_audioSource = GetComponent<AudioSource>();
		StartCoroutine( EnemySounds( ) );
	}

	private void OnEnable()
	{
		StartCoroutine( EnemySounds( ) );
	}
	
	void Update()
    {
        if (!_enemyBrain.Chasing)
        {
            _enemyState = EnemyState.Patrol;
;        }
        else
        {
            _enemyState = EnemyState.Chase;
        }
    }

	//SE関係
	public IEnumerator EnemySounds( ) {
		while ( true ) {
			if(_enemyState == EnemyState.Patrol)
            {
                _audioSource.PlayOneShot(_voices[0],0.25f);
                _audioSource.PlayOneShot(_footSteps[0],1.5f);
                _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
                _footSoundDelay = 2.4f / 2;
            }
            if (_enemyState == EnemyState.Chase)
            {
                _audioSource.PlayOneShot(_voices[1],0.25f);
                _audioSource.PlayOneShot(_footSteps[0], 1.5f);
                _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
                _footSoundDelay = 2.4f / 4;
            }
			
			yield return new WaitForSeconds( _footSoundDelay );
		}
	}
}
