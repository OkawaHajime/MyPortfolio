using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public AudioClip _se_player_damage;
    AudioSource _audio_source;
    private GameObject _playerObject = null;
	private Player _player = null;
	private int _attack = 50;

    private void Awake()
	{
		_playerObject = GameObject.Find("Player");
		_player = _playerObject.GetComponentInChildren<Player>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			_player.Damage(_attack);


        }
	}
}
