using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public Button PauseButton;
    public Button ResumeButton;
    public ObserverPattern<GameEventEnum.PlayerGameEvent> ObserverPatternPlayer;
    public ObserverPattern<GameEventEnum.GameEvent> ObserverPatternGame;
    protected override void Awake()
    {
        base.Awake();

        ObserverPatternPlayer = new ObserverPattern<GameEventEnum.PlayerGameEvent>();
        ObserverPatternGame = new ObserverPattern<GameEventEnum.GameEvent>();

    }


    //da sistemare
    public void Pause()
    {
        //Debug.Log("premo");
        if (Time.timeScale == 0) Time.timeScale = 1;
        else Time.timeScale = 0;

    }

  
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }



}
