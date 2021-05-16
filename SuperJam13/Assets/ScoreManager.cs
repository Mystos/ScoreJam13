using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public int score;
    public TextMeshProUGUI text;
    internal List<int> modifier = new List<int>();

    // Update is called once per frame
    void Update()
    {
        text.text = "Score : " + score;
    }

    public void AddScore(int nbr)
    {
        score += nbr;
        foreach (int item in modifier)
        {
            score += item;
        }
        
    }
}
