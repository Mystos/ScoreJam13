using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class SpawnScript : MonoBehaviour
{
    public PlayableDirector wakeUp;
    public TextMeshProUGUI textHistoire;

    private void Start()
    {
        if(wakeUp != null)
        {
            wakeUp.stopped += SpawnCharacterReset;

        }
    }
    public void Spawn()
    {
        if (wakeUp != null)
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            if (playerController != null)
            {                
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                playerController.enabled = false;
            }
            wakeUp.Play();
        }
    }

    private void SpawnCharacterReset(PlayableDirector action)
    {
        if (action == wakeUp)
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            if (playerController != null)
            {
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                playerController.enabled = true;
            }

            WaveSystem wave = FindObjectOfType<WaveSystem>();
            if(wave != null)
            {
                wave.waveActivated = true;
            }
        }
    }
}
