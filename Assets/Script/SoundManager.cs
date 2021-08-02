using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    public float musicBGMVolume = 1;
    public float soundVolume = 1;


    [SerializeField] private AudioSource musicBGM;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        musicBGM = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Update Volume accordingly at all times
        musicBGM.volume = musicBGMVolume;
        
        LiveUpdateVolume(soundVolume);
    }

    public void UpdateMusicVolume(float _volume)
    {
        musicBGM.volume = _volume;
    }

    public void UpdateSoundVolume(float _volume)
    {
        LiveUpdateVolume(_volume);
    }

    public void LiveUpdateVolume(float _soundVolume)
    {
        foreach (AudioSource _source in GameObject.FindObjectsOfType<AudioSource>())
        {
            if (_source != musicBGM)
            {
                _source.volume = _soundVolume;
            }
        }
    }

    public void PlaySound(AudioClip _clip, bool _loop, float _localMultiplier) //Creates a new audio source everytime a play sound is requeste
    {
        AudioSource _source = gameObject.AddComponent<AudioSource>();

        _source.clip = _clip;
        _source.loop = _loop;
        _source.volume = soundVolume * _localMultiplier;
        _source.Play();

        if (!_loop) //Non-looped sound killed when ends.
            StartCoroutine(KillAudioSource(_source));
    }
    public IEnumerator KillAudioSource(AudioSource _source)
    {
        while (_source.isPlaying)
        {
            yield return new WaitForFixedUpdate();
        }

        Destroy(_source);
    }
}
