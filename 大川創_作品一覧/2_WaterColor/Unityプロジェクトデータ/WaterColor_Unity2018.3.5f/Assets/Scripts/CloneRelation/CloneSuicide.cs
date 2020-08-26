using System.Collections;
using UnityEngine;

/// <summary>
/// Instantiateによって生み出されたリアクションアニメーションの削除を行う親クラス
/// 削除のタイミングで行う処理は、このクラスを継承した子クラスで実装する
/// 子クラスのScriptを生み出されるアニメーションオブジェクトに付ける
/// </summary>
public class CloneSuicide : MonoBehaviour
{
	[SerializeField] private float grace = 0.0f;

	private void Awake()
	{
		Initialize();
	}

	private void Start()
	{
		StartCoroutine(Suicide(grace));
	}

	private IEnumerator Suicide(float _grace)
	{
		//アニメーションが終わるまで待つ
		yield return new WaitForSeconds(_grace);
		Testament();
	}

	/// <summary>
	/// 以下virtual関数
	/// 子クラスで実装する部分
	/// </summary>

	//情報取得と初期設定
	protected virtual void Initialize() { }

	//削除のタイミングで行う処理
	protected virtual void Testament()
	{
		Destroy(gameObject);
	}
}
