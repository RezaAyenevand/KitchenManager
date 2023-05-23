using Infrastructure.EventManagement;
using Infrastructure.ServiceLocating;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, Service
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
        ServiceLocator.Clear();
        ServiceLocator.Init();

        ServiceLocator.Register(this);
        ServiceLocator.Register(new BasicEventManager());
    }

    public void ChangeSceneToGamePlay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

    public void ChangeSceneToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
