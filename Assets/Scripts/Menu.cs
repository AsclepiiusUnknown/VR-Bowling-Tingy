using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play(string mode)
    {
        switch (mode)
        {
            case "PC":
                GameManager.mode = Mode.PC;
                break;
            case "VR":
                GameManager.mode = Mode.VR;
                break;
            default:
                GameManager.mode = Mode.PC;
                break;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}