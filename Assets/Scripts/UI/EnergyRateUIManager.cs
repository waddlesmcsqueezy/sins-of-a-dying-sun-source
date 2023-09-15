using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyRateUIManager : MonoBehaviour
{
    private Text _textField;
    [SerializeField]
    private BasicEngine _engine;
    // Start is called before the first frame update
    void Start()
    {
        _textField = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _textField.text = Math.Round(_engine.CurrentEnergyRate, 1).ToString();
    }
}
