using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    public GameObject[] popUps;
    private int popUpIndex;
    //public GameObject spawner;
    public float waitTime = 60f;
    //public PlayerController playerController;

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < popUps.Length; i++){
            if (i == popUpIndex){
                popUps[i].SetActive(true);
            } else {
                popUps[i].SetActive(false);
            }
        }

        if (popUpIndex == 0){
            if (Input.GetKeyDown(KeyCode.J)){
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.J) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 3)
        {
            if (waitTime <= 0)
            {
                popUpIndex++;
            } 
            else
            {
                waitTime -= Time.deltaTime;
            }
        } 
       /* else if (popUpIndex == 4)
        {

        }*/
    }
}
