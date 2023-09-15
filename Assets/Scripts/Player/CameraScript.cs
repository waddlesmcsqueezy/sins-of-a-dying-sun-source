using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);

    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    private float orthoSize;

    [SerializeField]
    private float minSize = 1;
    [SerializeField]
    private float maxSize = 3;

    private float zoomSensitivity = 3.0f;
    private float zoomSmooth = 0.25f;

    private float zoomVelocity = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        orthoSize = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);


        float scrollAxis = Input.GetAxis("Mouse ScrollWheel");
        orthoSize -= scrollAxis * zoomSensitivity;
        orthoSize = Mathf.Clamp(orthoSize, minSize, maxSize);

        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, orthoSize, ref zoomVelocity, zoomSmooth);
    }
}
