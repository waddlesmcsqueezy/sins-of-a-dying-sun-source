using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelGaugeUI : MonoBehaviour
{
    [SerializeField] private FuelManager _fuelTank;

    private RectTransform _gaugeForeground;
    // Start is called before the first frame update
    void Start()
    {
        _gaugeForeground = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        _gaugeForeground.sizeDelta = new Vector2(_gaugeForeground.rect.width, 300 * (_fuelTank.CurrentFuel / _fuelTank.MaxFuel));
    }
}
