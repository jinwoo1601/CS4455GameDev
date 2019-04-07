using Cinemachine;
using System;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    public enum InputChoice
    {
        KeyboardAndMouse,
    }

    [Serializable]
    public struct InvertSettings
    {
        public bool invertX;
        public bool invertY;
    }


    public Transform follow;
    public Transform lookAt;
    public CinemachineFreeLook keyboardAndMouseCamera;
    public InputChoice inputChoice;
    public InvertSettings keyboardAndMouseInvertSettings;
    public bool allowRuntimeCameraSettingsChanges;

    public CinemachineFreeLook Current
    {
        get { return   keyboardAndMouseCamera; }
    }

    void Reset()
    {
        Transform keyboardAndMouseCameraTransform = transform.Find("KeyboardAndMouseFreeLookRig");
        if (keyboardAndMouseCameraTransform != null)
            keyboardAndMouseCamera = keyboardAndMouseCameraTransform.GetComponent<CinemachineFreeLook>();


        EllenPlayerController playerController = FindObjectOfType<EllenPlayerController>();
        if (playerController != null)
        {
            follow = playerController.transform;

            lookAt = follow.Find("HeadTarget");

            if (playerController.cameraSettings == null)
                playerController.cameraSettings = this;
        }
    }

    void Awake()
    {
        UpdateCameraSettings();
    }

    void Update()
    {
        if (allowRuntimeCameraSettingsChanges)
        {
            UpdateCameraSettings();
        }
    }

    void UpdateCameraSettings()
    {
        keyboardAndMouseCamera.Follow = follow;
        keyboardAndMouseCamera.LookAt = lookAt;
        keyboardAndMouseCamera.m_XAxis.m_InvertInput = keyboardAndMouseInvertSettings.invertX;
        keyboardAndMouseCamera.m_YAxis.m_InvertInput = keyboardAndMouseInvertSettings.invertY;



        keyboardAndMouseCamera.Priority = inputChoice == InputChoice.KeyboardAndMouse ? 1 : 0;
    }
}
