using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackArea : MonoBehaviour 
{
    GameObject _enemy_attack;
    EnemyMove _enemy_move;
    
	private void Start( ) 
	{
        _enemy_attack = transform.parent.gameObject;
        _enemy_move = _enemy_attack.GetComponent< EnemyMove >( );
    }

	//PlayerがAttackAreaに入ったときにモーションを変える
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			_enemy_move._attack_flag = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			_enemy_move._attack_flag = false;
		}
	}
}
