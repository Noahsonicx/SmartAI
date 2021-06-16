using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoringSystem : MonoBehaviour
{
    public GameObject score;
    public static int theScore;


    void Update()
    {
        score.GetComponent<Text>().text = "SCORE: " + theScore;
    }
}