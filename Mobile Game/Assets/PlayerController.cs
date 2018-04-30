using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

public class PlayerController : MonoBehaviour
{
    private Camera PlayerCamera;
    private MouseLook PlayerMouseLook;

    // Use this for initialization
    void Start()
    {
        PlayerCamera = Camera.main;
        PlayerMouseLook.Init(transform, PlayerCamera.transform);
    }

    // Update is called once per frame
    void Update()
    {
        RotateAim();
    }

    void RotateAim()
    {
        PlayerMouseLook.LookRotation(transform, PlayerCamera.transform);
    }
}

