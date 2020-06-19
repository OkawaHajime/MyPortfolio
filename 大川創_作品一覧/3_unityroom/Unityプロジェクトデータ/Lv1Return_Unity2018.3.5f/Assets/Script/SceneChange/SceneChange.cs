using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void SceneChange0()
    {
        SceneManager.LoadScene("Title");
    }
    public void SceneChange1()
    {
        SceneManager.LoadScene("Main");
    }

    public void SceneChange2()
    {
        SceneManager.LoadScene("Result_clear");
    }

    public void SceneChange3()
    {
        SceneManager.LoadScene("Result_gameover");
    }
}
