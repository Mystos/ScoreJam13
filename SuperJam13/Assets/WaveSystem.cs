using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public Wave wave;
    
    int currentPhase = -1;
    public int nbrOfEnemy = 0;

    public Transform location1;
    public Transform location2;
    public Transform location3;
    public Transform location4;
    public Transform location5;

    public GameScene nextLevel;

    private void Update()
    {
        if(nbrOfEnemy <= 0)
        {
            NextPhase();
        }
    }

    private void NextPhase()
    {
        currentPhase++;
        if(currentPhase <= wave.phaseList.Count -1)
        {
            if (wave.phaseList[currentPhase] != null)
            {
                if (wave.phaseList[currentPhase].enemyToSpawnLocation1 != null && location1 != null)
                {
                    foreach (GameObject enemy in wave.phaseList[currentPhase].enemyToSpawnLocation1)
                    {
                        nbrOfEnemy++;
                        Instantiate(enemy, new Vector3(location1.position.x + Random.Range(-.2f,.2f), location1.position.y + Random.Range(-.2f, .2f), 0), Quaternion.identity);
                    }
                }
                if (wave.phaseList[currentPhase].enemyToSpawnLocation2 != null && location2 != null)
                {
                    foreach (GameObject enemy in wave.phaseList[currentPhase].enemyToSpawnLocation2)
                    {
                        nbrOfEnemy++;
                        Instantiate(enemy, new Vector3(location2.position.x + Random.Range(-.2f, .2f), location2.position.y + Random.Range(-.2f, .2f), 0), Quaternion.identity);
                    }
                }
                if (wave.phaseList[currentPhase].enemyToSpawnLocation3 != null && location3 != null)
                {
                    foreach (GameObject enemy in location3)
                    {
                        nbrOfEnemy++;
                        Instantiate(enemy, new Vector3(location3.position.x + Random.Range(-.2f, .2f), location3.position.y + Random.Range(-.2f, .2f), 0), Quaternion.identity);
                    }
                }
                if (wave.phaseList[currentPhase].enemyToSpawnLocation4 != null && location4 != null)
                {
                    foreach (GameObject enemy in wave.phaseList[currentPhase].enemyToSpawnLocation4)
                    {
                        nbrOfEnemy++;
                        Instantiate(enemy, new Vector3(location4.position.x + Random.Range(-.2f, .2f), location4.position.y + Random.Range(-.2f, .2f), 0), Quaternion.identity);
                    }
                }
                if (wave.phaseList[currentPhase].enemyToSpawnLocation5 != null && location5 != null)
                {
                    foreach (GameObject enemy in wave.phaseList[currentPhase].enemyToSpawnLocation5)
                    {
                        nbrOfEnemy++;
                        Instantiate(enemy, new Vector3(location5.position.x + Random.Range(-.2f, .2f), location5.position.y + Random.Range(-.2f, .2f), 0), Quaternion.identity);
                    }
                }
            }
        }
        else
        {
            // The level endend
            // Load next level
            if(nextLevel != null)
            {
                SceneLoader.Instance.LoadLevel(nextLevel.sceneName);

            }
            else
            {
                SceneLoader.Instance.LoadMainMenu();

            }
        }
    }

    

}
