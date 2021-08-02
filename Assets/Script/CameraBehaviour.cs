using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    private Camera mainCam;
    private GameObject player;
    
    void Start()
    {
        mainCam = Camera.main;
        player = FindObjectOfType<PlayerController>().gameObject;
    }


    void Update()
    {
        
    }
}
