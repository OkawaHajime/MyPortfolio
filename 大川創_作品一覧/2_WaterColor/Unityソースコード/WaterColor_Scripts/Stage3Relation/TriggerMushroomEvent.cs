using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 最後のキノコイベントを開始させる
/// （リスが全色分の精を出現させる）
/// </summary>
public class TriggerMushroomEvent : MonoBehaviour
{
    [SerializeField] private GameObject _squirrel = null;
	[SerializeField] private List<GameObject> _animals = new List<GameObject>();
    [SerializeField] private List<ColorAbsorption> _lastSources = new List<ColorAbsorption>();
    private GameObject _player_Object = null;
    private Player _player = null;

    private void Awake()
    {
        _player_Object = GameObject.Find("Girl");
        _player = _player_Object.GetComponent<Player>();
        _squirrel.SetActive(false);
        foreach(ColorAbsorption lastsource in _lastSources){
            lastsource.gameObject.SetActive(false);
        }
    }

    //プレイヤーが来たら発動
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            StartEvent();
        }    
    }

    //負荷削減の目的で動物を削除、プレイヤーを止めリスを起動させる
    private void StartEvent()
    {
		foreach(GameObject animal in _animals) {
			Destroy(animal);
		}
        _player.PlayerAnimationChange(Player.ANIM_TYPE.ANIM_TYPE_WAIT);
        _squirrel.SetActive(true);
    }
}
