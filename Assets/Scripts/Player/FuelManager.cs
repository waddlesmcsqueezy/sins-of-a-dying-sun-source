using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelManager : MonoBehaviour
{
    [SerializeField]
    private float _currentFuel = 50000;
    [SerializeField]
    private float _maxFuel = 50000;

    public float CurrentFuel => this._currentFuel;
    public float MaxFuel => this._maxFuel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseFuel(float units)
    {
        if (CanUseFuel(units))
            _currentFuel -= units;
    }

    public void AddFuel(float units)
    {
        if (CanAddFuel(units))
            _currentFuel += units;
    }

    public bool CanUseFuel(float units)
    {
        if (_currentFuel - units < 0)
            return false;
        else return true;
    }

    public bool CanAddFuel(float units)
    {
        if (_currentFuel + units > _maxFuel)
            return false;
        else return true;
    }
}
