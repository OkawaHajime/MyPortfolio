using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _operation = null;
    [SerializeField] private List<GameObject> _stopObjects = new List<GameObject>();
    [SerializeField] private FovAngleController _cameraController = null;

    private GameObject _fearManagerObject = null;
    private FearManager _fearManager = null;

    private GameObject _sceneChangeObject = null;
    private SceneChangeEffectTrigger _sceneChange = null;

    private GameObject _enemyObject = null;

    private GameObject _pauseDisplay = null;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _fearManagerObject = GameObject.Find("FearManager");
        _fearManager = _fearManagerObject.GetComponent<FearManager>();

        _sceneChangeObject = GameObject.Find("SceneChangeEffectTrigger");
        _sceneChange = _sceneChangeObject.GetComponent<SceneChangeEffectTrigger>();

        _pauseDisplay = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        _fearManager.FearLevel
            .Subscribe(level => {
                if (level == FearManager.FEAR_LEVEL.FEAR_LEVEL_2) {
                    LoadEnemyInfo();
                }
            });

        _pauseDisplay.SetActive(false);
    }

    private void LoadEnemyInfo()
    {
        //エネミーが生み出されたときにエネミーの情報取得
        if (_enemyObject != null) {
            _enemyObject = GameObject.FindWithTag("Enemy");
        }
    }

    public void OpenPause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        foreach (GameObject stopObject in _stopObjects) {
            stopObject.SetActive(false);
        }
        if (_enemyObject) {
            _enemyObject.SetActive(false);
        }
        _cameraController.enabled = false;

        _pauseDisplay.SetActive(true);
    }

    public void ClosePause()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        foreach (GameObject stopObject in _stopObjects) {
            stopObject.SetActive(true);
        }
        if (_enemyObject) {
            _enemyObject.SetActive(true);
        }
        _cameraController.enabled = true;

        _pauseDisplay.SetActive(false);
    }

    public void ReturnTitle()
    {
        _sceneChange.changeScene("Title");
    }

    public void Operation()
    {
        _pauseDisplay.SetActive(false);
        _operation.SetActive(true);
    }

    public void CloseOperation()
    {
        _pauseDisplay.SetActive(true);
        _operation.SetActive(false);
    }
}
