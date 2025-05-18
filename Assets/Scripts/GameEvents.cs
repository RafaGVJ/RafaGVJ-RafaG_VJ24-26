using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnGameOver;
    public static event Action OnWin;
    public static event Action<int> OnAddPoints;
    public static event Action<bool> OnPause;


    public static void TriggerGameOver() => OnGameOver?.Invoke();
    public static void TriggerWin() => OnWin?.Invoke();
    public static void TriggerAddPoints(int points) => OnAddPoints?.Invoke(points);
    public static void TriggerPause(bool status) => OnPause?.Invoke(status);
}
