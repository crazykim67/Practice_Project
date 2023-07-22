using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraController : MonoBehaviour
{
    private Camera cam;

    private Transform camTr;

    private float xAxis, yAxis;
    [SerializeField]
    private float sensitivity;
    [SerializeField]
    private float xClamp, yClamp;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        camTr = cam.transform;
    }

    private void Update()
    {
        CameraUpdate();
    }

    private void CameraUpdate()
    {
        // 세로
        yAxis += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        // 가로
        xAxis -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xAxis = Mathf.Clamp(xAxis, -yClamp, yClamp);
        yAxis = Mathf.Clamp(yAxis, -xClamp, xClamp);

        camTr.localRotation = Quaternion.Euler(xAxis, yAxis, 0);
    }
}
