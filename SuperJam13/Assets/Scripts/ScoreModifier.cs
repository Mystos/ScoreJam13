using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreModifier : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        ScoreManager score =  FindObjectOfType<ScoreManager>();
        if(score != null) { 
            score.modifier.Add(350);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ScoreManager score = FindObjectOfType<ScoreManager>();
        if (score != null)
        {
            score.modifier.Remove(350);
        }
    }
}
