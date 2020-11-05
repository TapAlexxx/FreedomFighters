using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _cameraSpeed;

    private Vector3 _axis;

    private void Update()
    {
        _axis.x = Input.GetAxis("Horizontal");
        _axis.y = Input.GetAxis("Vertical");
        _axis.z = Input.GetAxis("Mouse ScrollWheel") * -50;

        if (transform.position.x >= 37 && transform.position.x <= 240 && transform.position.y >= -167 && transform.position.y <= -147 && transform.position.z >= 127 && transform.position.z <= 163)
            transform.Translate(new Vector3(_axis.x, _axis.z, _axis.y) * _cameraSpeed * Time.deltaTime);

        if (transform.position.x < 37)
            transform.position = new Vector3(37, transform.position.y, transform.position.z);
        if (transform.position.x > 240)
            transform.position = new Vector3(240, transform.position.y, transform.position.z);
        if (transform.position.y < -167)
            transform.position = new Vector3(transform.position.x, -167, transform.position.z);
        if (transform.position.y > -147)
            transform.position = new Vector3(transform.position.x, -147, transform.position.z);
        if (transform.position.z < 127)
            transform.position = new Vector3(transform.position.x, transform.position.y, 127);
        if (transform.position.z > 163)
            transform.position = new Vector3(transform.position.x, transform.position.y, 163);
    }
}
