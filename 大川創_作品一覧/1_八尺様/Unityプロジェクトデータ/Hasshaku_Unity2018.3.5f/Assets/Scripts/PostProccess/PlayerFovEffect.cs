using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerFovEffect : MonoBehaviour
{
    [SerializeField] private Color _blindColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    [SerializeField] private float _grainStrength = 0.0f;

    private PostProcessVolume _postVolume = null;
    private PostProcessProfile _postProfile = null;
    private Vignette _blind = null;
    private Grain _noise = null;

    private void Awake()
    {
        _postVolume = GetComponent<PostProcessVolume>();
        _postProfile = _postVolume.profile;
        _blind = _postProfile.GetSetting<Vignette>();
        _noise = _postProfile.GetSetting<Grain>();
    }

    private void Update()
    {
        _blind.color.Override(_blindColor);
        _noise.intensity.Override(_grainStrength);
    }
}
