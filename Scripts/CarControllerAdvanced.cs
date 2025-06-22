using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public enum DriveType { FWD, RWD, AWD }

    [Header("Wheel Colliders")]
    public WheelCollider[] wheelColliders; // 0 - FL, 1 - FR, 2 - RL, 3 - RR

    [Header("Wheel Meshes")]
    public Transform[] wheelMeshes; // 0 - FL, 1 - FR, 2 - RL, 3 - RR

    [Header("Settings")]
    public DriveType driveType = DriveType.RWD;
    public float motorForce = 1500f;
    public float brakeForce = 3000f;
    public float handbrakeForce = 5000f;
    public float maxSteerAngle = 30f;
    public float maxReverseSpeed = 20f;

    [Header("Center of Mass")]
    public Transform centerOfMass;

    private Rigidbody rb;
    private Quaternion[] initialRotations;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (centerOfMass != null)
            rb.centerOfMass = centerOfMass.localPosition;

        initialRotations = new Quaternion[wheelMeshes.Length];
        for (int i = 0; i < wheelMeshes.Length; i++)
            initialRotations[i] = wheelMeshes[i].localRotation;
    }

    private void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void HandleMotor()
    {
        float verticalInput = Input.GetAxis("Vertical");

        bool isReversing = Vector3.Dot(rb.velocity, transform.forward) < -0.1f;

        float appliedMotorTorque = motorForce * verticalInput;
        float speed = rb.velocity.magnitude * 3.6f;

        // Ограничение скорости задним ходом
        if (verticalInput < 0 && isReversing && speed > maxReverseSpeed)
        {
            appliedMotorTorque = 0f;
        }

        // Применение тяги
        ApplyDriveTorque(appliedMotorTorque);

        // Ручник
        bool handbrake = Input.GetKey(KeyCode.Space);
        if (handbrake)
        {
            wheelColliders[2].brakeTorque = handbrakeForce; // RL
            wheelColliders[3].brakeTorque = handbrakeForce; // RR
        }
        else
        {
            wheelColliders[2].brakeTorque = 0f;
            wheelColliders[3].brakeTorque = 0f;
        }
    }

    private void ApplyDriveTorque(float torque)
    {
        switch (driveType)
        {
            case DriveType.FWD:
                wheelColliders[0].motorTorque = torque;
                wheelColliders[1].motorTorque = torque;
                break;
            case DriveType.RWD:
                wheelColliders[2].motorTorque = torque;
                wheelColliders[3].motorTorque = torque;
                break;
            case DriveType.AWD:
                foreach (var wc in wheelColliders)
                    wc.motorTorque = torque;
                break;
        }
    }

    private void HandleSteering()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float steer = horizontalInput * maxSteerAngle;

        wheelColliders[0].steerAngle = steer;
        wheelColliders[1].steerAngle = steer;
    }

    private void UpdateWheels()
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            wheelColliders[i].GetWorldPose(out Vector3 pos, out Quaternion rot);

            if (i < wheelMeshes.Length)
            {
                wheelMeshes[i].position = pos;
                wheelMeshes[i].rotation = rot * initialRotations[i];
            }
        }
    }
}
