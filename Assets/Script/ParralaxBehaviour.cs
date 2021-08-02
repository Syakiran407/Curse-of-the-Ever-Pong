using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxBehaviour : MonoBehaviour
{
    private GameObject player;
    private Camera maincamera;

    public float ParallaxFactor;
    private Vector3 StartPos;
    private float length;
    private float height;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        maincamera = Camera.main;
        StartPos = transform.position;
        length = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        height = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
    }


    void FixedUpdate() //Fixed Update seems to work great for parallax effect - but I recall that when I implemented it for the infinite runner, it also seems to lead to jittering, so I'm not entirely sure as to why it does that.
    {
        //Tracking the player instead of the camera helps with some of the jittery-ness since rapid camera motion was possible (with the implementation of the camera offset behaviour)
        Vector3 effectivemoved = (player.transform.position) * ParallaxFactor;
        float remainingdistancex = (player.transform.position.x) * (1 - ParallaxFactor);

        transform.position = new Vector3(StartPos.x + effectivemoved.x, transform.position.y, transform.position.z);

        if (remainingdistancex > StartPos.x + length)
        {
            StartPos.x += 2 * length;
        }
        else if (remainingdistancex < StartPos.x - length)
        {
            StartPos.x -= 2 * length;
        }
    }
}

