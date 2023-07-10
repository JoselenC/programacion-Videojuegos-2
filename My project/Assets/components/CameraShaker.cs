using System;
using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker instance;
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;
    private float movementTime;
    private float totalMoveTime;
    private float initialIntensity;

    private void Awake()
    {
        instance = this;
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cinemachineBasicMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void MoveCamera(float intensity, float frequency, float time)
    {
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        _cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;
        initialIntensity = intensity;
        movementTime = time;
        totalMoveTime = time;
    }

    private void Update()
    {
        if (movementTime > 0)
        {
            movementTime -= Time.deltaTime;
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                Mathf.Lerp(initialIntensity, 0, 1 - (movementTime / totalMoveTime));
        }
    }
}
