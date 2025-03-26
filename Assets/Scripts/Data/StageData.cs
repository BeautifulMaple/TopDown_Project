using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageInfo
{
    public int stageKey;
    public WaveData[] waves;

    public StageInfo(int stageKey, WaveData[] waves)
    {
        this.stageKey = stageKey;
        this.waves = waves;
    }
}

[System.Serializable]
public class WaveData
{

}