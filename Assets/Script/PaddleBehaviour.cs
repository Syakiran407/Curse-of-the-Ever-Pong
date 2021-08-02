using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaddleBehaviour : MonoBehaviour
{
    public float BPM;
    private float startingBPM;
    private float internalClock;


    private float padRate = 1f;
    public float padMaxHeight = 2f;
    private float padHeight;

    public float padThreshold;
    public AudioClip padHitSound;

    public Slider test;
    private float initialBPMChangeValue = 2;

    private enum State
    {
        Ping,
        Pong,
        Ponged,
        Restarting,
        Death
    }

    private State thisState;

    void Start()
    {

        startingBPM = BPM;

        padThreshold *= padMaxHeight;

        ////////Testing purposes
        test.maxValue = padMaxHeight;
        ////////
        ///


        FindObjectOfType<PlayerMovement>().StopMovement();

        thisState = State.Restarting;
        internalClock = (float)AudioSettings.dspTime;
    }

    void Update()
    {
        padRate = BPM / 60;
        

        float _var = internalClock * padRate * Mathf.PI;
        padHeight = padMaxHeight * Mathf.Abs(Mathf.Sin(_var));

        ////////Testing purposes
        test.value = padHeight;
        ////////

        CheckPing();
        if (Input.GetKeyDown(KeyCode.J))
            CheckDuringPing();


        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    UpdateBPM(10,1);
        //}
        //
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    UpdateBPM(-10, 1);
        //}


        internalClock += Time.deltaTime;
    }


    public void Death()
    {
        BPM = 0;
        thisState = State.Death;
    }


    void CheckPing()
    {
        switch (thisState)
        {
            ////////Paddle Behaviour
            case State.Pong:
                if (padHeight <= padThreshold)
                {
                    thisState = State.Ping;
                }
                break;

            case State.Ping:
                if (padHeight >= padThreshold)
                {
                    WarningManager.instance.ChangeText("Hit paddle too late!");

                    FailedHit();
                    
                }
                    
                break;


            default: //Case State.Ponged
                if (padHeight >= padThreshold)
                    thisState = State.Pong;

                break;
            ///////

            case State.Restarting:
                BPM = 0;
                internalClock = 0;
                break;

            case State.Death:
                break;

        }
    }

    
    void CheckDuringPing()
    {
        if (padHeight <= padThreshold && thisState == State.Ping)
        {
            thisState = State.Ponged;
            SuccessfulHit();
        }

        else if (thisState == State.Restarting)
        {
            ///////Re-initialising player statistics after taking damage
            BPM = startingBPM;

            FindObjectOfType<DifficultyManager>().ResetDifficultyThreshold();
            FindObjectOfType<PlayerMovement>().ReinitialiseStats();
            thisState = State.Ponged;
        }

        else
        {
            WarningManager.instance.ChangeText("Hit Paddle too early!");
            FailedHit();
            Debug.Log("Oops at: " + padHeight);
        }
    }


    public void FailedHit()
    {
        if (GameManager.instance.TakeDamage())
            Death();

        else
        {
            FindObjectOfType<PlayerMovement>().StopMovement();
            thisState = State.Restarting;
        }
    }

  
    void SuccessfulHit()
    {
        SoundManager.instance.PlaySound(padHitSound,false, 1);

        //Update difficulty only on successful hit basis so it syncs well with player hits.
        if (DifficultyManager.instance.CheckForDifficultyIncrease())
        {
            float _changingBPMValue = 1f;
            UpdateBPM(_changingBPMValue, 2);
        }
    }

    public void UpdateBPM(float _delta, float _desiredTime)
    {
        //Set internal clock to beginning, to 'sync'
        internalClock = 0;

        //Change BPM and update padRate
        BPM += _delta;
        padRate = BPM / 60;

        

        float _desiredBPM = BPM + _delta;
        StartCoroutine(InterpolateBPM(_desiredBPM, _desiredTime));

   
    }

    public IEnumerator InterpolateBPM(float _desiredBPM, float _desiredTime)
    {
        float _initialBPM = BPM;
        float _finalTime = _desiredTime;
        float _currentTime = 0;
        Debug.Log("DesiredBPM: " + _desiredBPM);

        while (BPM != _desiredBPM)
        {

            BPM = Mathf.Lerp(_initialBPM, _desiredBPM, _currentTime / _finalTime);
            yield return new WaitForFixedUpdate();
            _currentTime += Time.deltaTime;
        }

    }


}
