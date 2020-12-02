using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int pinsDown = 0;
    public static Mode mode = Mode.VR;

    public Light pinLight;
    Color[] lightColors = new Color[6];
    public int colorIndex = 0;

    public List<TouchCheck> pins;
    public List<PinData> pinDatas;

    public List<Ball> balls;
    public List<BallData> ballDatas;

    private void Start()
    {
        print(VrHelper.VRIsPresent());

        // mode = (VrHelper.VRIsPresent()) ? Mode.VR : Mode.PC;

        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < lightColors.Length; i++)
        {
            switch (i)
            {
                case 0:
                    lightColors[i] = Color.red;
                    pinLight.color = Color.red;
                    break;
                case 1:
                    lightColors[i] = Color.white;
                    break;
                case 2:
                    lightColors[i] = Color.yellow;
                    break;
                case 3:
                    lightColors[i] = Color.blue;
                    break;
                case 4:
                    lightColors[i] = Color.cyan;
                    break;
                case 5:
                    lightColors[i] = Color.magenta;
                    break;
            }
        }

        SaveBallData();
        SavePinData();
    }

    void SavePinData()
    {
        pins = new List<TouchCheck>(FindObjectsOfType<TouchCheck>());
        pinDatas = new List<PinData>();

        for (int i = 0; i < pins.Count; i++)
        {
            pinDatas.Add(new PinData());
            pinDatas[i].startPos = pins[i].transform.position;
            pinDatas[i].startRot = pins[i].transform.rotation;
            pinDatas[i].go = pins[i].gameObject;
        }
        print(pins.Count + " pins found...");
    }

    void SaveBallData()
    {
        balls = new List<Ball>(FindObjectsOfType<Ball>());
        ballDatas = new List<BallData>();
        print(balls.Count + " balls found...");


        for (int i = 0; i < balls.Count; i++)
        {
            ballDatas.Add(new BallData());
            ballDatas[i].startPos = balls[i].transform.position;
            ballDatas[i].startRot = balls[i].transform.rotation;
            ballDatas[i].go = balls[i].gameObject;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            CycleLightColor();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPins();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetBalls();
        }
    }

    public void ResetBalls()
    {
        for (int i = 0; i < ballDatas.Count; i++)
        {
            // ballDatas[i].go.SetActive(true);
            ballDatas[i].go.transform.position = ballDatas[i].startPos;
            ballDatas[i].go.transform.rotation = ballDatas[i].startRot;
            ballDatas[i].go.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public void ResetPins()
    {
        for (int i = 0; i < pinDatas.Count; i++)
        {
            pinDatas[i].go.SetActive(true);
            pinDatas[i].go.transform.position = pinDatas[i].startPos;
            pinDatas[i].go.transform.rotation = pinDatas[i].startRot;
            pinDatas[i].go.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public void CycleLightColor()
    {
        colorIndex++;
        if (colorIndex >= lightColors.Length)
            colorIndex = 0;
        pinLight.color = lightColors[colorIndex];
    }
}

[System.Serializable]
public enum Mode
{
    VR,
    PC
}

public class PinData
{
    public Vector3 startPos;
    public Quaternion startRot;
    public GameObject go;
}

public class BallData
{
    public Vector3 startPos;
    public Quaternion startRot;
    public GameObject go;
}