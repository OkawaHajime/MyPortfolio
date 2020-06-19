using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip _se_sword;
    AudioSource _audio_source;
    [SerializeField] private float _speed = 0.0f;
	[SerializeField] private float _speedMas = 2.1f;
	[SerializeField] private Animator _attackMotion = null;

    private void Start()
    {
        _audio_source = GetComponent<AudioSource>();
    }

    private void Update()
	{
		//左
		if (Input.GetKey(KeyCode.A)) {
			gameObject.transform.position += gameObject.transform.right * _speed * -1 * Time.unscaledDeltaTime;
		}
		//右
		if (Input.GetKey(KeyCode.D)) {
			gameObject.transform.position += gameObject.transform.right * _speed * Time.unscaledDeltaTime;
		}
		//前
		if (Input.GetKey(KeyCode.W)) {
			gameObject.transform.position += gameObject.transform.forward * _speed * Time.unscaledDeltaTime;
		}
		//後
		if (Input.GetKey(KeyCode.S)) {
			gameObject.transform.position += gameObject.transform.forward * _speed * -1 * Time.unscaledDeltaTime;
		}

		//走り
		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			_speed *= _speedMas;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift)) {
			_speed /= _speedMas;
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			_attackMotion.SetTrigger("Attack");
            _audio_source.PlayOneShot(_se_sword);
		}
	}
}
