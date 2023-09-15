using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationAudioTrigger : MonoBehaviour
{
    private bool _playerInZone = false;

    [SerializeField] private AudioSource _alarmAudioSource;
    [SerializeField] private AudioSource _floodlightOnAudioSource;
    [SerializeField] private AudioSource _floodlightOffAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerInZone)
        {
            if (!_alarmAudioSource.isPlaying)
                _alarmAudioSource.Play();
            _alarmAudioSource.loop = true;
        }
        else
        {
            _alarmAudioSource.loop = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerInZone = true;
            _floodlightOnAudioSource.Play();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerInZone = false;
            _floodlightOffAudioSource.Play();
        }
    }
}
