using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    [SerializeField]
    private float _currentEnergy = 5000;
    [SerializeField]
    private float _maxEnergy = 5000;

    public float CurrentEnergy => _currentEnergy;
    public float MaxEnergy => _maxEnergy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseEnergy(float units)
    {
        if (CanUseEnergy(units))
            _currentEnergy -= units;

    }

    public void AddEnergy(float units)
    {
        if (CanAddEnergy(units))
            _currentEnergy += units;
        else _currentEnergy = _maxEnergy;
    }

    public bool CanUseEnergy(float units)
    {
        if (_currentEnergy - units < 0)
            return false;
        else return true;
    }

    public bool CanAddEnergy(float units)
    {
        if (_currentEnergy + units > _maxEnergy)
            return false;
        else return true;
    }

    public void IncreaseMaxEnergy(float units)
    {
        _maxEnergy += units;
    }
}
