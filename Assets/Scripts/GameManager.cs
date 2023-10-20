using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public ObserverPattern<GameEventEnum.PlayerGameEvent> ObserverPatternPlayer;
    public ObserverPattern<GameEventEnum.GameEvent> ObserverPatternGame;
    protected override void Awake()
    {
        base.Awake();

        ObserverPatternPlayer = new ObserverPattern<GameEventEnum.PlayerGameEvent>();
        ObserverPatternGame = new ObserverPattern<GameEventEnum.GameEvent>();

    }


   



}
