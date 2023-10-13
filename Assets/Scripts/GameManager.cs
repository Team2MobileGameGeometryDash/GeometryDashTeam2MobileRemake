using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
