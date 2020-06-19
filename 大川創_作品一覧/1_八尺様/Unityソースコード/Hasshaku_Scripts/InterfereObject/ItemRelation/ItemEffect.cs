using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    [SerializeField] private Animator _animator = null;
    [SerializeField] private float _effectStartdistance = 0.0f;

    private GameObject _player = null;
    private float _distance = 0.0f;
    private Vector3 _playerTransform;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.Find("Player");
    }

    void Update() 
     {
        _playerTransform = _player.transform.position;

        _distance = Vector3.Distance(_playerTransform,transform.position);

        if (_distance < _effectStartdistance) {
            _animator.SetBool("find", true);
        } else {
            _animator.SetBool("find",false);
        }
    }
}
