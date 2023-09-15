using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StationLightingTrigger : MonoBehaviour
{
    [SerializeField] private List<GameObject> _lightsList;

    // Start is called before the first frame update
    void Start()
    {
        _lightsList.ForEach(delegate(GameObject light)
        {
            light.GetComponent<Light2D>().enabled = false;
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _lightsList.ForEach(delegate (GameObject light)
            {
                light.GetComponent<Light2D>().enabled = true;
            });
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _lightsList.ForEach(delegate (GameObject light)
            {
                light.GetComponent<Light2D>().enabled = false;
            });
        }
    }
}
