using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float currentBreakForce;
    private float currenSteerAngle;
    private bool isBraking;

    [SerializeField] bool controllerTest = false;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private List<WheelCollider> wheelColliders;
    [SerializeField] private List<Transform> wheelTransforms;

    private void FixedUpdate()
    {
        if (controllerTest)
        {
            GetInput();
        }
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        foreach (var wheel in wheelColliders)
        {
            wheel.motorTorque = verticalInput * motorForce;
            Debug.Log(wheel.motorTorque);
        }
        
        currentBreakForce = isBraking ? breakForce : 0f;
        ApplyBraking();
    }


    private void ApplyBraking()
    {
        foreach (var wheel in wheelColliders)
        {
            wheel.brakeTorque = currentBreakForce;
        }
    }

    private void HandleSteering()
    {
        currenSteerAngle = maxSteerAngle * horizontalInput;
        wheelColliders[0].steerAngle = currenSteerAngle;
        wheelColliders[1].steerAngle = currenSteerAngle;
    }



    public void SetInputs(float forwardAmount, float turnAmount)
    {
        horizontalInput = turnAmount;
        verticalInput = forwardAmount;
    }

    private void UpdateWheels()
    {
        for (int i = 0; i < wheelColliders.Count; i++)
        {
            UpdateSingleWheel(wheelColliders[i], wheelTransforms[i]);
        }
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
