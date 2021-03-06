﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrRig : MonoBehaviour
{
    public static VrRig instance = null;

    public Transform Headset { get { return headset; } }
    public VrController LeftController { get { return leftController; } }
    public VrController RightController { get { return rightController; } }

    [SerializeField]
    private Transform headset;
    [SerializeField]
    private VrController leftController;
    [SerializeField]
    private VrController rightController;

    private void Awake()
    {
        bool isVR = GameManager.mode == Mode.VR;
        gameObject.SetActive(isVR);

        VrHelper.SetEnabled(true);

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        leftController.Setup();
        rightController.Setup();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}