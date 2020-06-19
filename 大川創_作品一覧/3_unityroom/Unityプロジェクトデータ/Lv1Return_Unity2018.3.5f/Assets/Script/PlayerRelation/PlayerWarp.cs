using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarp : MonoBehaviour
{
    public AudioClip _se_warp;
    AudioSource _audio_source;
    private GameObject _warp1_p;
    private GameObject _warp2_p;
    private GameObject _warp3_p;
    private GameObject _fade_obj;
    private Fade _fade_sri;

    private void Start()
    {
        _audio_source = GetComponent<AudioSource>();
        _warp1_p = GameObject.Find("Warp1_p");
        _warp2_p = GameObject.Find("Warp2_p");
        _warp3_p = GameObject.Find("Warp3_p");
        _fade_obj = GameObject.Find("FadePanel");
        _fade_sri = _fade_obj.GetComponent<Fade>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Warp1")
        {
            _fade_sri._ok = false;
            StartCoroutine("Warp1");
            _audio_source.PlayOneShot(_se_warp);
        }

        if (collision.gameObject.tag == "Warp2")
        {
            _fade_sri._ok = false;
            StartCoroutine("Warp2");
            _audio_source.PlayOneShot(_se_warp);
        }

        if (collision.gameObject.tag == "Warp3")
        {
            _fade_sri._ok = false;
            StartCoroutine("Warp3");
            _audio_source.PlayOneShot(_se_warp);
        }
    }

    private IEnumerator Warp1()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.transform.position = _warp1_p.transform.position;
    }

    private IEnumerator Warp2()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.transform.position = _warp2_p.transform.position;
    }

    private IEnumerator Warp3()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.transform.position = _warp3_p.transform.position;
    }
}
