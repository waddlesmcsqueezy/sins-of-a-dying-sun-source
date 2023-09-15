using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Klaxon : MonoBehaviour
{
    [SerializeField]
    private float _spinSpeed = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1), Time.deltaTime * _spinSpeed);

    }
}
