using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int pinsDown = 0;
    public static Mode mode = Mode.None;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}

[System.Serializable]
public enum Mode
{
    None,
    VR,
    PC
}