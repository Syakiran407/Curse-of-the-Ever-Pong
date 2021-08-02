using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public AudioClip damageSound;

    public int livesLeft = 3;
    public bool Death = false;

    public CinemachineVirtualCamera deathCamera;

    public Canvas paddleCanvas;
    public TMP_Text livesText;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        deathCamera.Priority = 0;
        InitialiseRound();

    }

    void Update()
    {
        
    }

    public void InitialiseRound()
    {
        deathCamera.Priority = 0;
        livesLeft = 3;
        Death = false;

        paddleCanvas.enabled = true;
        livesText.text = "Lives left: " + livesLeft;

        DifficultyManager.instance.Initialise();

    }

    public bool TakeDamage()
    {
        livesLeft--;
        livesText.text = "Lives left: " + livesLeft;
        SoundManager.instance.PlaySound(damageSound, false, 1f);

        if (livesLeft == 0 && !Death)
        {
            Debug.Log(this.name + ": GameOver");

            Death = true;

            //FindObjectOfType<PaddleBehaviour>().Death(); //Function is already called in PaddleBehaviour
            FindObjectOfType<PlayerController>().Death();
            deathCamera.Priority = 100;

            DisablePaddleCanvas();
            return true;
        }

        else return false;

    }

    void DisablePaddleCanvas()
    {
        paddleCanvas.enabled = false;
    }


}
