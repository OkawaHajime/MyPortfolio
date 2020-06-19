using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBGM : MonoBehaviour
{
    [SerializeField] private AudioClip _nomal = null;
    [SerializeField] private AudioClip _chase = null;
    
    private AudioSource _audioSource = null;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _nomal;
        _audioSource.Play();
    }

    public void Nomal()
    {
        _audioSource.clip = _nomal;
        _audioSource.Play();
    }

    public void Chase()
    {

        _audioSource.clip = _chase;
        _audioSource.Play();
    }
}
