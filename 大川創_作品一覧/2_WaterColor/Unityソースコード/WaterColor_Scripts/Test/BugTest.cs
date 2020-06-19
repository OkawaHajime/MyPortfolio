using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BugTest : MonoBehaviour
{
	[SerializeField]private string test;

	private void Awake()
	{
			SceneManager.LoadScene(test, LoadSceneMode.Additive);

	}
}
