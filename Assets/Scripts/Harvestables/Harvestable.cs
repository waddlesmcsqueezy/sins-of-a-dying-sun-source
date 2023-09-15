using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable : MonoBehaviour
{
    [SerializeField] private string _harvestableName;
    [SerializeField] private string _harvestableDescription;
    [SerializeField] private Color _harvestableColor;
    [SerializeField] private Item.EffectCategory _effect;
    [SerializeField] private float _effectAmount;

    public string HarvestableName => this._harvestableName;

    public string HarvestableDescription => this._harvestableDescription;

    public Color HarvestableColor => this._harvestableColor;

    Harvestable()
    {
        _harvestableName = "default_name";
        _harvestableDescription = "default_description";
        _harvestableColor = Color.white;

    }

    public Item HarvestEffect()
    {
        return new Item(_harvestableName, _harvestableDescription, _effect, _effectAmount);
    }
}
