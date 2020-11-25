using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        GameManager.mode = (VrHelper.VRIsPresent()) ? Mode.VR : Mode.PC;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}