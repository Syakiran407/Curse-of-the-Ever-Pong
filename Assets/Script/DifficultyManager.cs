using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager instance = null;

    public GameObject player;
    private Vector3 initialPlayerPos;
    private Vector3 currentPlayerPos;
    private float currentRunDistance;

    public float difficultyThreshold = 20f;
    private float varyingThreshold = 1f;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    public void Initialise()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        varyingThreshold = 1f;
        difficultyThreshold = 20f;
    }

    void Update()
    {
        if (player != null)
        {
            currentPlayerPos = player.transform.position;
            currentRunDistance = (currentPlayerPos - initialPlayerPos).x; //Get horizontal component of current Run Distance Vector
        }
    }

     
    public void GetNewInitialPositionForDifficultyCheck()
    {
        initialPlayerPos = player.transform.position;
    }

    public void ResetDifficultyThreshold()
    {
        varyingThreshold = 1;
    }

    public bool CheckForDifficultyIncrease()
    {
        if (currentRunDistance > difficultyThreshold)
        {
            difficultyThreshold += (20 + 10*Mathf.Log(varyingThreshold));
            Debug.Log(difficultyThreshold);
            varyingThreshold++;
            return true;
        }

        else return false;
    }

}
