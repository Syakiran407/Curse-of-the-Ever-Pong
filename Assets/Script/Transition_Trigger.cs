using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition_Trigger : MonoBehaviour
{

    private TransitionManager transitionInstance;

    // Start is called before the first frame update
    void Start()
    {
        transitionInstance = TransitionManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RunCommand(int _index)
    {
        transitionInstance.RunCommand(_index);
    }

}
