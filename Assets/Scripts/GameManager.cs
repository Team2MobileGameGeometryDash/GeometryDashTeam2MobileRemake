using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ObserverPattern<GameEventEnum.GameEvent> observerPattern;

    protected override void Awake()
    {
        base.Awake();

        observerPattern = new ObserverPattern<GameEventEnum.GameEvent>();


    }
}
