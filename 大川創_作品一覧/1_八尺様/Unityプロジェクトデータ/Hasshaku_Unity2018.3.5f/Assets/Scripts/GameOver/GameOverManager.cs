using UnityEngine;

/// <summary>
/// チュートリアルのクリア状況によってゲームオーバーシーンで表示する内容を切り替える
/// </summary>
public class GameOverManager : MonoBehaviour
{
	private GameObject _gameOver = null;
	private GameObject _tutorialGameOver = null;

	private bool _tutorial = false;

	private void Awake()
	{
		//Canvasの情報取得
		_gameOver = GameObject.Find("GameOver");
		_tutorialGameOver = GameObject.Find("TutorialGameOver");

		//チュートリアルが終わっているかを確認
		_tutorial = TutorialSceneManager.TutorialFinish;
	}

	private void Start()
	{
		//Canvasのactiveを一度falseにする
		_gameOver.SetActive(false);
		_tutorialGameOver.SetActive(false);

		if (_tutorial) {
			_gameOver.SetActive(true);
		}
		else {
			_tutorialGameOver.SetActive(true);
		}
	}
}
