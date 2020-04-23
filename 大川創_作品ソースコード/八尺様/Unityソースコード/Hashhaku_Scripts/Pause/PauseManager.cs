using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PauseManager : MonoBehaviour
{
    [SerializeField]private GameObject _operation = null;
    [SerializeField]private List<GameObject> _stopObjects = new List<GameObject>();
    [SerializeField]private FovAngleController _cameraController = null;

    private GameObject _fearManagerObject = null;
    private FearManager _fearManager = null;

    private GameObject _sceneChangeObject = null;
    private SceneChangeEffectTrigger _sceneChange = null;

    private GameObject _enemyObject = null;

    private GameObject _pauseDisplay = null;

    private void Awake()
    {
        //マウスカーソルロック
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //恐怖度管理者の情報取得
        _fearManagerObject = GameObject.Find("FearManager");
        _fearManager = _fearManagerObject.GetComponent<FearManager>();

        //シーン遷移管理者の情報取得
        _sceneChangeObject = GameObject.Find("SceneChangeEffectTrigger");
        _sceneChange = _sceneChangeObject.GetComponent<SceneChangeEffectTrigger>();

        //ポーズ画面の情報取得
        _pauseDisplay = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        //恐怖度段階の増減を監視
        _fearManager.FearLevel
            .Subscribe(level => {
                if (level == FearManager.FEAR_LEVEL.FEAR_LEVEL_2){
                    LoadEnemyInfo();
                }
            });

        //ポーズ画面非表示
        _pauseDisplay.SetActive(false);
    }

    private void LoadEnemyInfo()
    {
        //エネミーが生み出されたときにエネミーの情報取得
        if (_enemyObject != null){
            _enemyObject = GameObject.FindWithTag("Enemy");
        }
    }

    public void OpenPause()
    {
        //マウスカーソルロック解除
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        //ポーズ中、表示させたくない、機能させたくないオブジェクトを停止
        foreach (GameObject stopObject in _stopObjects){
            stopObject.SetActive(false);
        }
        if (_enemyObject){
            _enemyObject.SetActive(false);
        }
        _cameraController.enabled = false;

        //ポーズ画面表示
        _pauseDisplay.SetActive(true);
    }

    public void ClosePause()
    {
        //マウスカーソルロック
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //オブジェクトの停止解除
        foreach (GameObject stopObject in _stopObjects){
            stopObject.SetActive(true);
        }
        if (_enemyObject){
            _enemyObject.SetActive(true);
        }
        _cameraController.enabled = true;

        //ポーズ画面非表示
        _pauseDisplay.SetActive(false);
    }

    public void ReturnTitle()
    {
        _sceneChange.changeScene("Title");
    }

    public void Operation()
    {
        //ポーズ画面非表示にして、操作方法パネルを表示
        _pauseDisplay.SetActive(false);
        _operation.SetActive(true);
    }

    public void CloseOperation()
    {
        //ポーズ画面表示させ、操作方法パネルを非表示に
        _pauseDisplay.SetActive(true);
        _operation.SetActive(false);
    }
}
