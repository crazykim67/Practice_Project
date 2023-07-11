using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    /*private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;

    // Settings
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private void FixedUpdate() {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput() {
        // Steering Input
        horizontalInput = Input.GetAxis("Horizontal");

        // Acceleration Input
        verticalInput = Input.GetAxis("Vertical");

        // Breaking Input
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor() {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking() {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering() {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels() {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
        Vector3 pos;
        Quaternion rot; 
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }*/

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;

    private float currentSteerAngle;

    private float currentbreakForce;
    private bool isBreaking;

    [SerializeField]
    private bool isFront;
    [SerializeField]
    private bool isBack;

    [SerializeField]
    private float motorForce;
    [SerializeField]
    private float breakForce;
    [SerializeField]
    private float stopForce;
    [SerializeField]
    private float maxSteerAngle;

    [SerializeField]
    private WheelCollider flWheelCollider;
    [SerializeField]
    private WheelCollider frWheelCollider;
    [SerializeField]
    private WheelCollider rlWheelCollider;
    [SerializeField]
    private WheelCollider rrWheelCollider;

    [SerializeField]
    private Transform flWheelTr;
    [SerializeField]
    private Transform frWheelTr;
    [SerializeField]
    private Transform rlWheelTr;
    [SerializeField]
    private Transform rrWheelTr;

    private void FixedUpdate()
    {
        GetInout();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(flWheelCollider, flWheelTr);
        UpdateSingleWheel(frWheelCollider, frWheelTr);
        UpdateSingleWheel(rlWheelCollider, rlWheelTr);
        UpdateSingleWheel(rrWheelCollider, rrWheelTr);
    }

    private void UpdateSingleWheel(WheelCollider coll, Transform tr)
    {
        Vector3 pos;
        Quaternion rot;

        coll.GetWorldPose(out pos, out rot);
        tr.rotation = rot;
        tr.position = pos;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        flWheelCollider.steerAngle = currentSteerAngle;
        frWheelCollider.steerAngle = currentSteerAngle;
    }

    private void HandleMotor()
    {
        flWheelCollider.motorTorque = verticalInput * motorForce;
        frWheelCollider.motorTorque = verticalInput * motorForce;

        currentbreakForce = isBreaking ? breakForce : 0f;
        stopForce = !isFront && !isBack ? 400f : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        flWheelCollider.brakeTorque = currentbreakForce;
        frWheelCollider.brakeTorque = currentbreakForce;
        rlWheelCollider.brakeTorque = currentbreakForce;
        rrWheelCollider.brakeTorque = currentbreakForce;

        flWheelCollider.brakeTorque = stopForce;
        frWheelCollider.brakeTorque = stopForce;
        rlWheelCollider.brakeTorque = stopForce;
        rrWheelCollider.brakeTorque = stopForce;
    }

    private void GetInout()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);

        isBreaking = Input.GetKey(KeyCode.Space);
    
        isFront = Input.GetKey(KeyCode.W);
        isBack = Input.GetKey(KeyCode.S);
    }
}
