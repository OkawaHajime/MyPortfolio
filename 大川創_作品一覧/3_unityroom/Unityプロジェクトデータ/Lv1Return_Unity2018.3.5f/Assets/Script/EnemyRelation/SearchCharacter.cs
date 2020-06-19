using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchCharacter : MonoBehaviour
{

    GameObject _enemy_watch;
    EnemyMove _enemy_move;

    // Start is called before the first frame update
    void Start( ) {
        //ここ？
        _enemy_watch = transform.parent.gameObject;
        _enemy_move = _enemy_watch.GetComponent< EnemyMove >( );
    }

    // Update is called once per frame
    void Update( ) {

    }

    private void OnTriggerStay( Collider col ) {
        if ( col.gameObject.tag == "Player" ) {
            _enemy_move._watch_flag = true;
        }
    }
}
