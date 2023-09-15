using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : MonoBehaviour
{
    [SerializeField]
    private string _dockName;

    [SerializeField] public GameObject StationMenuPrefab;

    private BoxCollider2D _boxCollider;

    public string DockName => this._dockName;
    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DockPlayer()
    {
        //_boxCollider.enabled = false;
        

    }

    public void UndockPlayer()
    {
        //_boxCollider.enabled = true;
    }
}
