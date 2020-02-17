using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MyGame/Create GameEvent", fileName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> eventistenrs = new List<GameEventListener>();

    public void Raise()
    {
        for(int i = 0; i < eventistenrs.Count; i++)
        {
            eventistenrs[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!eventistenrs.Contains(listener))
            eventistenrs.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        eventistenrs.Remove(listener);
    }
}
