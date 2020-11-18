using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play(string mode)
    {
        if (mode == "PC")
        {
            VrHelper.SetEnabled(false);
            GameManager.mode = Mode.PC;
        }
        else if (mode == "VR")
        {
            VrHelper.SetEnabled(true);
            GameManager.mode = Mode.VR;
        }
        else
            Debug.LogError("**ERROR**");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}