using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "WaveSystem/Wave")]
public class Wave : ScriptableObject
{
    public List<Phase> phaseList;
}



