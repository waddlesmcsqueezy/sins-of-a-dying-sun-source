using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AmbientLightChangeTrigger : MonoBehaviour
{
    [SerializeField] private float _transitionTime = 5;
    [SerializeField] private AmbientLight _lightSetting;
    private Light2D _ambientLight;

    // Start is called before the first frame update
    void Start()
    {
        _ambientLight = GameObject.Find("Ambient Light Controller").GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerControlScript>())
        {
            if (_ambientLight.gameObject.GetComponent<AmbientLightController>().CurrentLightSetting != _lightSetting.SettingName)
                StartCoroutine(ChangeLighting());
        }
    }

    private IEnumerator ChangeLighting()
    {
        _ambientLight.gameObject.GetComponent<AmbientLightController>().CurrentLightSetting = _lightSetting.SettingName;
        Color originColor = _ambientLight.color;
        float originIntensity = _ambientLight.intensity;
        float currentTime = 0;
        //float normalizedTime;
        float smoothMultiplier = 0.5f;

        while (currentTime <= _transitionTime)
        {
            currentTime += Time.deltaTime * smoothMultiplier;

            //normalizedTime = currentTime / _transitionTime;
            _ambientLight.color = Color.Lerp(originColor, _lightSetting.TargetColor, currentTime);
            _ambientLight.intensity = Mathf.Lerp(originIntensity, _lightSetting.TargetIntensity, currentTime);
            yield return null;
        }

        yield return null;
    }
}
