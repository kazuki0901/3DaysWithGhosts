using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChengeScene : MonoBehaviour
{
    public void GameSceneTransfer()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void TitelReturn()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void GameEnd()
    {
        Application.Quit();
    }
}
