using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SpawnScript : MonoBehaviour
{
    public PlayableDirector wakeUp;

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
            wakeUp.Play();
        }
    }

    private void SpawnCharacterReset(PlayableDirector action)
    {
        if (action == wakeUp)
        {
            CharacterController charController = GetComponent<CharacterController>();
            PlayerController playerController = GetComponent<PlayerController>();
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            charController.enabled = true;
            playerController.enabled = true;
        }
    }
}
