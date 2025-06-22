using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float xSpeed = 120f;
    [SerializeField] private float ySpeed = 120f;

    [SerializeField] private float yMinLimit = -20f;
    [SerializeField] private float yMaxLimit = 80f;

    private float x = 0f;
    private float y = 0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        if (Input.GetMouseButton(1)) // ПК: правая кнопка мыши
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);
        }

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0f, 0f, -distance) + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }
}
