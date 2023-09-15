using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAnimateController : MonoBehaviour
{
    public bool _idleRotate = true;
    public bool _idleRotateDir = true; // true = left, false = right
    public float IdleAnimateSpeed = 0.25f;
    public float IdleAnimateExtremity = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_idleRotate)
        {
            // rotate left
            if (_idleRotateDir)
            {
                transform.Rotate(0, (IdleAnimateSpeed * Time.deltaTime), 0);
            } 
            // rotate right
            else
            {
                transform.Rotate(0, -(IdleAnimateSpeed * Time.deltaTime), 0);
            }

            if (GetSignedRotation() >= IdleAnimateExtremity) _idleRotateDir = false;
            else if (GetSignedRotation() <= -IdleAnimateExtremity) _idleRotateDir = true;

            
        }
    }

    float GetSignedRotation()
    {
        if (transform.eulerAngles.y > 180)
        {
            return transform.eulerAngles.y - 360;
        }
        else
        {
            return transform.eulerAngles.y;
        }
    }
}
