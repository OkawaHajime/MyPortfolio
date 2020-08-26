using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// Escキーが押されたときに、ポーズ画面を表示する
/// ポーズ中は、プレイヤーと敵が動かないようにする
/// </summary>
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
        //敵がステージ上にいるか監視
        _fearManager.FearLevel
            .Subscribe(level => {
                if (level == FearManager.FEAR_LEVEL.FEAR_LEVEL_2) {
                    LoadEnemyInfo();
                }
            });

        _pauseDisplay.SetActive(false);
    }

    //エネミーが生み出されたときにエネミーの情報取得
    private void LoadEnemyInfo()
    {
        if (_enemyObject != null) {
            _enemyObject = GameObject.FindWithTag("Enemy");
        }
    }

    //ポーズ画面を表示
    public void OpenPause()
    {
        _pauseDisplay.SetActive(true);
        
        //マウスカーソル表示
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        //特定のオブジェクトの動きを止める
        foreach (GameObject stopObject in _stopObjects) {
            stopObject.SetActive(false);
        }
        if (_enemyObject) {
            _enemyObject.SetActive(false);
        }
        _cameraController.enabled = false;
    }

    //ポーズ画面を非表示
    public void ClosePause()
    {
        _pauseDisplay.SetActive(false);

        //マウスカーソルを固定化
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //止めていたオブジェクトを再起
        foreach (GameObject stopObject in _stopObjects) {
            stopObject.SetActive(true);
        }
        if (_enemyObject) {
            _enemyObject.SetActive(true);
        }
        _cameraController.enabled = true;
    }

    /// <summary>
    /// 以下、ボタン用の関数
    /// </summary>

    //タイトルに戻る
    public void ReturnTitle()
    {
        _sceneChange.changeScene("Title");
    }

    //操作方法一覧を表示
    public void Operation()
    {
        _pauseDisplay.SetActive(false);
        _operation.SetActive(true);
    }

    //操作方法一覧を非表示
    public void CloseOperation()
    {
        _pauseDisplay.SetActive(true);
        _operation.SetActive(false);
    }
}
