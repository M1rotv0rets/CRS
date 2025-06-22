using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSwitcher : MonoBehaviour
{
    public GameObject[] cars;               // Машины (весь объект)
    public MonoBehaviour[] carControllers;  // Скрипты управления

    public Camera orbitCam;                 // Сама камера OrbitCam

    private int currentCarIndex = 0;

    void Start()
    {
        ActivateCar(currentCarIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            currentCarIndex++;
            if (currentCarIndex >= cars.Length)
                currentCarIndex = 0;

            ActivateCar(currentCarIndex);
        }
    }

    void ActivateCar(int index)
    {
        for (int i = 0; i < cars.Length; i++)
        {
            bool isActive = (i == index);

            // Включаем/выключаем управление
            carControllers[i].enabled = isActive;

            // Если машина активная, сбрасываем её Rigidbody (чтобы не подлетала)
            Rigidbody rb = cars[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (isActive)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }

            // Если машина активная — наводим камеру OrbitCamera
            if (isActive && orbitCam != null)
            {
                orbitCam.GetComponent<OrbitCamera>().target = cars[i].transform;
            }
        }

        Debug.Log("Управляем машиной: " + cars[index].name);
    }
}
