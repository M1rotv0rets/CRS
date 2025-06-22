using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera orbitCamera;

    private void Start()
    {
        SwitchToMain(); // стартуем с основной камеры
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) // по кнопке C переключаем
        {
            bool isMainActive = mainCamera.gameObject.activeSelf;
            if (isMainActive)
                SwitchToOrbit();
            else
                SwitchToMain();
        }
    }

    public void SwitchToMain()
    {
        mainCamera.gameObject.SetActive(true);
        orbitCamera.gameObject.SetActive(false);
    }

    public void SwitchToOrbit()
    {
        mainCamera.gameObject.SetActive(false);
        orbitCamera.gameObject.SetActive(true);
    }
}
