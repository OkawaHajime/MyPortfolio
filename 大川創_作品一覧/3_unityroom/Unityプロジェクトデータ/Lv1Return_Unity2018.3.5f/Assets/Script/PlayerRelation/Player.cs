using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]　private Slider _hp_slider;
    [SerializeField]　private bool _hit = false;
    private int _hp = 100;
	private int _attack = 100;
	private int _difense = 100;
    public AudioClip _se_player_damage;
    AudioSource _audio_source;
    private GameObject _scenechange_obj;
    private SceneChange _scenechange_sri;

	private EnemyDefense _enemy = null;

    public int Attack {
        set {
            _attack = value;
        }
    }
	public int Difense {
		set {
			_difense = value;
		}
	}

    private void Start()
    {
        _audio_source = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        _scenechange_obj = GameObject.Find("SceneChange");
        _scenechange_sri = _scenechange_obj.GetComponent<SceneChange>();
    }

    public void Damage(int enemyAttack)
	{
		var _damage = _difense - enemyAttack;
		if (_damage <= 0) {
			_hp += _damage;
			_hp_slider.value = _hp;
            _audio_source.PlayOneShot(_se_player_damage);
            if (_hp <= 1) {
                _scenechange_sri.SceneChange3();
            }
		}
	}

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Space) && other.gameObject.tag == "Enemy") {
			_enemy = other.gameObject.GetComponentInChildren<EnemyDefense>();
			_enemy.Damage(_attack);
        }
    }
}
