using UnityEngine;

/// <summary>
/// SEの再生を行う
/// </summary>
public class SoundEffectManager : MonoBehaviour
{
    public enum SOUND_TYPE
    {
        SOUND_TYPE_ATTACH,
        SOUND_TYPE_FAILUR,
        SOUND_TYPE_GET,

    };

    [SerializeField] private AudioClip _attachSound = null;
    [SerializeField] private AudioClip _failurSound = null;
    [SerializeField] private AudioClip _getSound = null;

    private AudioSource _audio = null;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    //特定のSEを再生する
    public void PlayBackSound(SOUND_TYPE _selectSound)
    {
        switch (_selectSound) {
            case SOUND_TYPE.SOUND_TYPE_ATTACH:
                _audio.PlayOneShot(_attachSound);
                break;

            case SOUND_TYPE.SOUND_TYPE_FAILUR:
                _audio.PlayOneShot(_failurSound);
                break;

            case SOUND_TYPE.SOUND_TYPE_GET:
                _audio.PlayOneShot(_getSound);
                break;
        }
    }
}
