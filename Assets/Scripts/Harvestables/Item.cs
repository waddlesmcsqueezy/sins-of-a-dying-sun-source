using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum EffectCategory
    {
        Energy,
        MaxEnergy,
        Health,
        MaxHealth,
        EngineSpeedMod,
        EnergyUsage,
        GeneratorSpeed,
        DrillLevel
    };     // health, battery, mining tool

    private string _itemName;
    private string _itemDescription;
    private float _effectAmount;       // +1000, +1, etc.
    private EffectCategory _effect;

    public string ItemName => _itemName;
    public string ItemDescription => _itemDescription;
    public EffectCategory Effect => _effect;
    public float EffectAmount => _effectAmount;
    
    public Item(string itemName, string itemDescription, EffectCategory effect, float effectAmount)
    {
        _itemName = itemName;
        _itemDescription = itemDescription;
        _effect = effect;
        _effectAmount = effectAmount;
    }
    
}
