using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drill : Equipment
{
    [SerializeField] private AudioSource _startAudioSource;
    [SerializeField] private AudioSource _loopAudioSource;
    [SerializeField] private AudioSource _stopAudioSource;

    [SerializeField]
    private float _damage = 5.0f;
    public float Damage => _damage;

    private bool _inUse;
    private bool _isRunning;
    public bool InUse => _inUse;

    private bool _touchingDestructable = false;
    public Destructable _availableDestructable = null;

    void Update()
    {
        if (_level > 0 && _inUse && !_isRunning)
        {
            StartCoroutine(RunDrill());
        }

        if (_level > 0 && _isRunning && _touchingDestructable)
        {
            if (_level >= _availableDestructable.Level)
                _availableDestructable.DamageThis(Time.deltaTime * _damage);
        }
    }

    private IEnumerator RunDrill()
    {
        _isRunning = true;

        _startAudioSource.Play();
        while(!_startAudioSource.isPlaying)
            yield return null;

        _loopAudioSource.Play();

        while (_inUse)
            yield return null;

        _loopAudioSource.Stop();
        _stopAudioSource.Play();
        _isRunning = false;

        yield return null;
    }

    public void TurnOn()
    {
        _inUse = true;
    }

    public void TurnOff()
    {
        _inUse = false;
    }

    public void SetDamage(float units)
    {
        _damage = units;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Destructable")
        {
            _availableDestructable = other.GetComponent<Destructable>();
            
            _touchingDestructable = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        _touchingDestructable = false;
        _availableDestructable = null;
    }
}
