using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class CameraController : MonoBehaviour
{
    private Transform camTr;

    private float xAxis, yAxis;

    [SerializeField]
    private float sensitivity;

    [SerializeField]
    private float xClamp;

    [SerializeField]
    private Transform playerTr;

    private void Awake()
    {
        camTr = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        yAxis += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        xAxis -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xAxis = Mathf.Clamp(xAxis, -xClamp, xClamp);

        camTr.localRotation = Quaternion.Euler(xAxis, yAxis, 0);
        playerTr.localRotation = Quaternion.Euler(0, yAxis, 0);
    }
}
