using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCounter : ScriptableObject
{
    private int _levelCount;

    public int getLevelCount()
    {
        return _levelCount;
    }

    public void NewGame()
    {
        _levelCount = 1;
    }

    public void NextLevel()
    {
        _levelCount++;
    }
}
