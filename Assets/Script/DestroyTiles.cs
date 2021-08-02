using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTiles : MonoBehaviour
{

    private Transform playerPosition;
    float minDistance = 2f;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        float dist = Vector2.Distance(this.gameObject.transform.position, playerPosition.position);
        Debug.Log(playerPosition.position);

        if (dist > minDistance)
        {
            StartCoroutine(Destroy());
        }
    }

    private IEnumerator Destroy()  
    {
        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }

}
