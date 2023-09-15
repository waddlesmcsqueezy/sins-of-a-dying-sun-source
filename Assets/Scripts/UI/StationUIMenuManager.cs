using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class StationUIMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _stationMainMenu;


    [SerializeField] private GameObject _assemblerMenu;
    [SerializeField] private GameObject _encyclopediaMenu;
    [SerializeField] private GameObject _refuelingMenu;
    [SerializeField] private GameObject _equipmentMenu;

    [SerializeField] public Button CloseButton;

    void Start()
    {
        _assemblerMenu.SetActive(false);
        _encyclopediaMenu.SetActive(false);
        _refuelingMenu.SetActive(false);
        _equipmentMenu.SetActive(false);
    }
}
