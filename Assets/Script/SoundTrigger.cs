using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public List<AudioClip> clips;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerSound(float _localMultiplier)
    {
        AudioClip _randomSound = clips[Random.Range(0, clips.Count - 1)];

        SoundManager.instance.PlaySound(_randomSound, false, _localMultiplier);
    }

}
