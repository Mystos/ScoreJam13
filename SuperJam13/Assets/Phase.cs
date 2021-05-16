using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Phase", menuName = "WaveSystem/Phase")]
public class Phase : ScriptableObject
{
    [Header("Location 1")]
    public List<GameObject> enemyToSpawnLocation1;

    [Header("Location 2")]
    public List<GameObject> enemyToSpawnLocation2;

    [Header("Location 3")]
    public List<GameObject> enemyToSpawnLocation3;

    [Header("Location 4")]
    public List<GameObject> enemyToSpawnLocation4;

    [Header("Location 5")]
    public List<GameObject> enemyToSpawnLocation5;
}
