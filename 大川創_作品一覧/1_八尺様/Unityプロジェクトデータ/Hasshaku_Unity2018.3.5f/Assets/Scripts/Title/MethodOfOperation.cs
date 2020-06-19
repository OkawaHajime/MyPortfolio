using UnityEngine;

public class MethodOfOperation : MonoBehaviour
{
	[SerializeField] private GameObject _titleUI = null;
	[SerializeField] private GameObject _operationUI = null;

	public void OpenWindow()
	{
		_titleUI.SetActive(false);
		_operationUI.SetActive(true);
	}

	public void CloseWindow()
	{
		_titleUI.SetActive(true);
		_operationUI.SetActive(false);
	}
}
