using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _cameraSpeed;
    private float _axisX;
    private float _axisY;
    private float _axisZ;

    private void Update()
    {
        _axisX = Input.GetAxis("Horizontal");

        _axisY = Input.GetAxis("Vertical");

        _axisZ = Input.GetAxis("Mouse ScrollWheel") * -50;

        if (transform.position.x >= 37 && transform.position.x <= 240 &&
            transform.position.y >= -167 && transform.position.y <= -147 &&
            transform.position.z >= 127 && transform.position.z <= 163)
        {
            transform.Translate(new Vector3(_axisX, _axisZ, _axisY) * _cameraSpeed * Time.deltaTime);
        }

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
