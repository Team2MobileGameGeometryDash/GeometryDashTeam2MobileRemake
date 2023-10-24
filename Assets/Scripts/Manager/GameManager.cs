using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public ObserverPattern<GameEventEnum.GameEvent> ObserverPatternGame;
    protected override void Awake()
    {
        base.Awake();

        ObserverPatternGame = new ObserverPattern<GameEventEnum.GameEvent>();

    }


   



}
