using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BasicEngine : MonoBehaviour
{
    [SerializeField]
    private AudioSource _engineLoopAudioSource;
    [SerializeField]
    private AudioSource _engineStartAudioSource;
    [SerializeField]
    private AudioSource _engineStopAudioSource;

    [SerializeField]
    private List<ParticleSystem> _engineBubbleParticleSystem;

    [SerializeField]
    private float _engineStartTime = 2.0f;
    [SerializeField]
    private float _engineStopTime = 2.0f;

    private bool _isRunning;
    private bool _canToggleEngineFlag;

    public bool IsRunning => this._isRunning;
    public bool IsReceivingInput;


    //[SerializeField]
    //private float _horizontalSpeedMod = 10.0f;
    //[SerializeField]
    //private float _verticalSpeedMod = 5.0f;
    [SerializeField]
    private float _turboSpeedBonus = 5.0f;

    private float _engineSpeedMod = 1.0f;
    private float _baseMoveSpeed = 20.0f;

    [SerializeField] 
    private float _baseEnergyRate = 1.5f; // cubic meters per second

    [SerializeField] private float _energyGeneratorModifier = 10f; // multiplied by the _baseEnergyRate

    public float BaseEnergyRate => this._baseEnergyRate;
    
    private float _currentEnergyRate = 0;
    
    public float CurrentEnergyRate => this._currentEnergyRate;
    
    private float _energyRateChangeMod = 15f;

    [SerializeField] private EnergyManager _energySupply;

    public EnergyManager EnergySupply => this._energySupply;

    public bool IsTurbo;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var emitter in GameObject.FindGameObjectsWithTag("EngineBubbles"))
        {
            _engineBubbleParticleSystem.Add(emitter.GetComponent<ParticleSystem>());
        }
        
        _isRunning = false;
        _canToggleEngineFlag = true;
        IsReceivingInput = false;
        IsTurbo = false;
        _engineLoopAudioSource.pitch = 0.51f;
        _engineBubbleParticleSystem.ForEach(delegate(ParticleSystem particleSystem)
        {
            particleSystem.Stop();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRunning)
        {
            // SOUND
            if (_isRunning && !_engineLoopAudioSource.isPlaying)
            {
                _engineLoopAudioSource.Play();
            }

            switch (IsReceivingInput)
            {
                // SOUND PITCH MODIFIERS
                case true when IsTurbo && _engineLoopAudioSource.pitch < 1.25:
                case true when _engineLoopAudioSource.pitch < 1.0:
                    _engineLoopAudioSource.pitch += Time.deltaTime;
                    break;
                default:
                {
                    if (_engineLoopAudioSource.pitch > 0.5)
                    {
                        _engineLoopAudioSource.pitch -= Time.deltaTime;
                    }

                    break;
                }
            }


            switch (IsReceivingInput)
            {
                //OXYGEN CONSUMPTION CALCULATION
                case false when _currentEnergyRate < -(_baseEnergyRate * (_energyGeneratorModifier / 2)):
                    _currentEnergyRate += Time.deltaTime * _energyRateChangeMod;
                    break;
                case false when _currentEnergyRate >= -(_baseEnergyRate * (_energyGeneratorModifier / 2)):
                    _currentEnergyRate -= Time.deltaTime * _energyRateChangeMod;
                    break;
                case true when !IsTurbo && _currentEnergyRate < _baseEnergyRate * 2:
                    _currentEnergyRate += Time.deltaTime * _energyRateChangeMod;
                    break;
                case true when !IsTurbo && _currentEnergyRate >= _baseEnergyRate * 2:
                    _currentEnergyRate -= Time.deltaTime * _energyRateChangeMod;
                    break;
                case true when IsTurbo && _currentEnergyRate < _baseEnergyRate * 5:
                    _currentEnergyRate += Time.deltaTime * _energyRateChangeMod;
                    break;
                case true when IsTurbo && _currentEnergyRate >= _baseEnergyRate * 5:
                    _currentEnergyRate -= Time.deltaTime * _energyRateChangeMod;
                    break;
            }
            
            if (_currentEnergyRate < 0)
            {
                if (_energySupply.CanAddEnergy(-_currentEnergyRate))
                {
                    _energySupply.AddEnergy(-_currentEnergyRate);
                }
            }
            else
            {
                if (_energySupply.CanUseEnergy(_currentEnergyRate))
                {
                    _energySupply.UseEnergy(_currentEnergyRate);
                }
                else
                {
                    ToggleEngine();
                }
            }

        }
        else
        {
            _currentEnergyRate = -(_baseEnergyRate * (_energyGeneratorModifier));
            if (_currentEnergyRate < 0)
            {
                if (_energySupply.CanAddEnergy(-_currentEnergyRate))
                {
                    _energySupply.AddEnergy(-_currentEnergyRate);
                }
            }

        }
    }

    public void ToggleEngine()
    {
        if (_canToggleEngineFlag)
        {
            _canToggleEngineFlag = false;
            if (!IsRunning)
            {
                StartEngine();
            }
            else
            {
                StopEngine();
            }
        }
    }

    public void StartEngine()
    {
        _engineStartAudioSource.Play();

        _engineBubbleParticleSystem.ForEach(delegate (ParticleSystem particleSystem)
        {
            particleSystem.Play();
        });

        StartCoroutine(EngineStartTimer());
    }

    private IEnumerator EngineStartTimer()
    {
        yield return new WaitForSeconds(_engineStartTime);
        _engineLoopAudioSource.loop = true;
        _isRunning = true;
        _canToggleEngineFlag = true;
    }

    public void StopEngine()
    {
        //if (_engineLoopAudioSource.isPlaying)
        _engineLoopAudioSource.loop = false;
        _engineLoopAudioSource.Stop();
        _engineStopAudioSource.Play();

        _engineBubbleParticleSystem.ForEach(delegate (ParticleSystem particleSystem)
        {
            particleSystem.Stop();
        });

        StartCoroutine(EngineStopTimer());
    }

    private IEnumerator EngineStopTimer()
    {
        _isRunning = false;
        yield return new WaitForSeconds(_engineStopTime);
        _canToggleEngineFlag = true;
    }

    public float GetMoveSpeedMod()
    {
        float speed = 0;
        float tempSpeedBoosts = 0;

        if (IsRunning)
            speed += (_baseMoveSpeed * _engineSpeedMod);
        if (IsTurbo)
            tempSpeedBoosts += (_turboSpeedBonus * _engineSpeedMod);

        speed += tempSpeedBoosts;

        return speed;
    }

    public void IncreaseGeneratorSpeed(float units)
    {
        _energyGeneratorModifier += units;
    }

    public void IncreaseEngineSpeedMod(float units)
    {
        _engineSpeedMod += units;
    }

    public void DecreaseEnergyUsage(float units)
    {
        _baseEnergyRate -= units;
    }
}
