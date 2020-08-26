using UnityEngine;

/// <summary>
/// 色の精を管理するオブジェクトに付ける
/// 保持している精の入れ替えと削除を行う
/// </summary>
public class ColorPossession : MonoBehaviour
{
	private GameObject _possessionColor = null;
	private ColorAbsorption _source = null;

	//色の精の非表示と入れ替え
	public void GetColor(GameObject possessionSource)
	{
		possessionSource.gameObject.SetActive(false);
		if (_possessionColor) {
			_source = _possessionColor.GetComponent<ColorAbsorption>();
			_source.TurnBack();
		}
		_possessionColor = possessionSource;
	}

	//正解だったときに保持している精を削除
	public void PossessionKill()
	{
		Destroy(_possessionColor.gameObject);
	}
}
