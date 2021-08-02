using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;

    private GameObject player;
    private Vector3 startPos;
    private Vector3 currentPos;

    private float BPM;
    private float oldBPM;

    public float score;
    public float highestBPM;

    public TMP_Text scoreText;
    public TMP_Text BPMText;

    public TMP_Text highScoreText;
    public TMP_Text highestBPMText;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

    }

    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        startPos = player.transform.position;

        scoreText.text = "Score: " + score;
    }

    void Update()
    {
        BPM = FindObjectOfType<PaddleBehaviour>().BPM;

        currentPos = player.transform.position;         
        UpdateScore((currentPos - startPos).x);//Obtain horizontal component between the two position (only horizontal distance)

        if (BPM != oldBPM)
        {
            UpdateBPM(BPM);
        }

        oldBPM = FindObjectOfType<PaddleBehaviour>().BPM;
    }

    public void UpdateScore(float _value)
    {   
        if (_value > score)
        {
            score = Mathf.RoundToInt(_value);
            scoreText.text = "Score: " + score;
            highScoreText.text = "Score: " + score;
        }
          
    }   

    public void UpdateBPM(float _value)
    {
        BPMText.text = ""+Mathf.RoundToInt(_value);

        if (_value > highestBPM)
        {
            highestBPM = _value;
            highestBPMText.text = "Highest BPM: " + highestBPM;
        }
    }
}
