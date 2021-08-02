using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLogic : MonoBehaviour
{
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (collision.tag == "Player")
        {    
            Destroy(this.gameObject);
            Debug.Log("Trap Destoyed");
        }

    }
}
