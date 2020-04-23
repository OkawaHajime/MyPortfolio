using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Timer : MonoBehaviour
{
    /// <summary>
    /// カウントダウンストリーム
    /// このObservableを各クラスがSubscribeする
    /// </summary>
    ///
	[SerializeField]private SceneChangeEffectTrigger _sceneChange = null;
    [SerializeField] private int _maxTime = 60;
    private IConnectableObservable<int> _countDownObservable;

    public IObservable<int> CountDownObservable
    {
        get
        {
            return _countDownObservable.AsObservable();
        }
    }


    private void Awake()
    {
        //60秒カウントのストリームを作成
        //PublishでHot変換
        _countDownObservable = CreateCountDownObservable(_maxTime).Publish();
    }

    private void Start()
    {
        //Start時にカウント開始
        _countDownObservable.Connect();
    }

    /// <summary>
    /// CountTimeだけカウントダウンするストリーム
    /// </summary>
    /// <param name="CountTime"></param>
    /// <returns></returns>
    private IObservable<int> CreateCountDownObservable(int CountTime)
    {
        return Observable
            .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1))
            .Select(x => (int)(CountTime - x))
            .TakeWhile(x => x > 0);
    }
}
