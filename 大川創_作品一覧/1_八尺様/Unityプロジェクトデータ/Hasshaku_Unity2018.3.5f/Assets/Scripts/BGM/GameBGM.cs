using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通常時と敵追跡時でBGMを切り替える
/// </summary>
public class GameBGM : MonoBehaviour
{
    [SerializeField] private AudioClip _nomal = null;
    [SerializeField] private AudioClip _chase = null;
    
    private AudioSource _audioSource = null;

	private void Awake()
	{
        _audioSource = GetComponent<AudioSource>();
	}

	private void Start()
    {
        _audioSource.clip = _nomal;
        _audioSource.Play();
    }

    //通常時
    public void Nomal()
    {
        _audioSource.clip = _nomal;
        _audioSource.Play();
    }

    //敵追跡時
    public void Chase()
    { 
        _audioSource.clip = _chase;
        _audioSource.Play();
    }
}
