using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameEventsListener 
{
    public interface IGameEventListener
    {
        void OnGameOver();
        void OnWin();
        void OnAddPoints(int points);
        void OnPause(bool isPaused);
    }
}
