using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    private AudioSource _audiosource;
    [SerializeField]
    private AudioClip _se_enemy_die;

    private void Awake()
    {
        _audiosource = GetComponent<AudioSource>();
    }

    public void SEEnemyDie()
    {
        _audiosource.PlayOneShot(_se_enemy_die);
    }
}
