using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMng : Singleton<SceneMng>
{
    int sceneNum = 0;
    void OnEnable()
    {
        sceneNum = SceneManager.GetActiveScene().buildIndex;
    }
    public void ResetGame()
    {
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    //¾Û Exit
    public void QuitButtonPressed()
    {
        Application.Quit();
    }
    public void NextSceneGo()
    {
        Invoke("NextScene", 0.2f);
    }
    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void TitleGo()
    {
        SceneManager.LoadScene("Title");
    }
}
