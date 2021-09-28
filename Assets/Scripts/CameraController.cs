using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera vCam;
    private Transform hero;
    private CinemachineFramingTransposer composer;

    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        hero = vCam.Follow;
        composer = vCam.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Update()
    {
        if (hero.position.x < -4.5f)
        {
            // Camara no se debe mover
            composer.m_DeadZoneWidth = 2f;
        }else
        {
            composer.m_DeadZoneWidth = 0f;
        }
    }
}
