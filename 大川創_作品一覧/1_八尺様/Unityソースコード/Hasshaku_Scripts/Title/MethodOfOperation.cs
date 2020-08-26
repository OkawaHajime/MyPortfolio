using UnityEngine;

/// <summary>
/// 操作方法パネルの表示切替
/// </summary>
public class MethodOfOperation : MonoBehaviour
{
	[SerializeField] private GameObject _titleUI = null;
	[SerializeField] private GameObject _operationUI = null;

	//表示
	public void OpenWindow()
	{
		_titleUI.SetActive(false);
		_operationUI.SetActive(true);
	}

	//非表示
	public void CloseWindow()
	{
		_titleUI.SetActive(true);
		_operationUI.SetActive(false);
	}
}
