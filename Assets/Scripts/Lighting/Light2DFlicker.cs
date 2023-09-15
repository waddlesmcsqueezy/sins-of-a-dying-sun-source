using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light2DFlicker : MonoBehaviour
{
    
    private Light2D _targetLight;
    [SerializeField][Range(0.0f, 1.0f)] private float _minIntensity = 0.0f;
    [SerializeField][Range(0.0f, 1.0f)] private float _maxIntensity = 0.0f;
    [SerializeField]
    AnimationCurve _timeDistribution; // defined in inspector

    void Start()
    {
        _targetLight = GetComponent<Light2D>();
        StartCoroutine(LightFlicker());
    }

    IEnumerator LightFlicker()
    {
        while (true)
        {
            
            var newValue = Random.Range(_minIntensity, _maxIntensity);
            _targetLight.intensity = newValue;

            yield return new WaitForSeconds(WeightedRandom());
        }
    }

    public float WeightedRandom()
    {
        float v = Random.Range(0f, 1f);
        return _timeDistribution.Evaluate(v);
    }
}
