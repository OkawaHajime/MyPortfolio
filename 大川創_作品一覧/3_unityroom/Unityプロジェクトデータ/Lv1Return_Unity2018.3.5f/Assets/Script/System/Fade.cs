using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private Image _ren;
    private float _alpha = 1.0f;
    private float _fade = 0.01f;
    public bool _ok = true;

    private void Start()
    {
        _ren = GetComponent<Image>();
    }

    private void Update()
    {
        if (_ok)
        {
            if (_ren.color.a >= 0.0f)
            {
                _ren.color = new Color(0.0f, 0.0f, 0.0f, _alpha);
                _alpha -= _fade;
            }
        }
        if (!_ok)
        {
            if (_ren.color.a <= 1.0f)
            {
                _ren.color = new Color(0.0f, 0.0f, 0.0f, _alpha);
                _alpha += _fade;
            }
            else
            {
                _ok = true;
            }
        }
    }
}
