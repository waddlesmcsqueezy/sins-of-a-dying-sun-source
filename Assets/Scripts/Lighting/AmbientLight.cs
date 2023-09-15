using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientLight : MonoBehaviour
{
    public string SettingName = "";
    [SerializeField] private float _targetIntensity = 1.0f;
    [SerializeField] private Color _targetColor = Color.white;

    public float TargetIntensity => _targetIntensity;
    public Color TargetColor => _targetColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
