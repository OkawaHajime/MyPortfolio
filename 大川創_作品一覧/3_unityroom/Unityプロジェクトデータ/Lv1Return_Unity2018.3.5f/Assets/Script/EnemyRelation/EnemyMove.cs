using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private GameObject TargetObject;
    private Vector3 _player_position;
    
	private Animator _animator;
    public bool _watch_flag = false;
    public bool _attack_flag = false;

    NavMeshAgent _navMeshAgent;

    void Awake( ) 
	{
		TargetObject = GameObject.Find("Player");
        _navMeshAgent = GetComponent< NavMeshAgent >( );
        _animator = GetComponent< Animator >( );
    }
	
    void Update( ) 
	{
        _player_position = TargetObject.transform.position;
        if ( _watch_flag == true ) {
            _navMeshAgent.SetDestination( _player_position );
            _animator.SetBool( "_run_flag", true );
        }

        if ( _attack_flag == true ) {
            _animator.SetBool( "_attack_flag", true );
        } 
		else {
            _animator.SetBool( "_attack_flag", false );
        }
    }
}
