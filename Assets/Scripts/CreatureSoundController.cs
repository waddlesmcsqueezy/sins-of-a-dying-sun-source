using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreatureSoundController : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _idleAudioSources;


    [SerializeField] private int _timerMin = 30;
    [SerializeField] private int _timerMax = 50;

    private float _lastSoundTime = 0.0f;

    private float _currentRandomTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - _lastSoundTime >= _currentRandomTimer)
        {

            _lastSoundTime = Time.time;
            _currentRandomTimer = Random.Range(_timerMin, _timerMax);
            int randomSound = Random.Range(0, _idleAudioSources.Count);
            _idleAudioSources[randomSound].Play();
        }
    }
}
