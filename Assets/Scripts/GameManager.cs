using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ObserverPattern<GameEventEnum.GameEvent> ObserverPattern;

    protected override void Awake()
    {
        base.Awake();

        ObserverPattern = new ObserverPattern<GameEventEnum.GameEvent>();


    }
}
