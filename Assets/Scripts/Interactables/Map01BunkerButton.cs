using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Map01BunkerButton : Interactable
{
    [SerializeField]
    private GameObject _door1;
    [SerializeField]
    private GameObject _door2;

    [SerializeField]
    private AudioSource _alarmSound;

    [SerializeField]
    private AudioSource _doorSound;

    [SerializeField] private List<GameObject> _lightsList;

    bool _hasActivated = false;

    private float _doorSpeed = 0.075f;

    private float _timer = 20.0f;

    private float _currentTime = 0.0f;

    public override void Interact()
    {
        Debug.Log("Interacting");
        if (!_hasActivated)
        {
            Debug.Log("Activated");
            StartCoroutine(OpenDoors());
            _hasActivated = true;

            _lightsList.ForEach(delegate (GameObject light)
            {
                light.GetComponent<Light2D>().color = Color.red;
            });
        }
    }

    private IEnumerator OpenDoors()
    {
        _alarmSound.Play();

        _doorSound.Play();

        Debug.Log("Coroutine");
        while (_currentTime < _timer)
        {
            _currentTime += Time.deltaTime;
            _door1.transform.position -= transform.up * Time.deltaTime * _doorSpeed;
            _door2.transform.position += transform.up * Time.deltaTime * _doorSpeed;
            yield return null;
        }

        _alarmSound.loop = false;

        yield return null;
    }
}
