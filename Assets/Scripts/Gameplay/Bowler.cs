using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[RequireComponent(typeof(PCGrabber))]
public class Bowler : MonoBehaviour
{
    public Image powerBar;
    public float barFillSpeed = .1f;
    bool isFilling = true;
    [MinMaxSlider(0, .1f, true)]
    public Vector2 swayMinMax;
    [MinMaxSlider(0, 1000, true)]
    public Vector2 powerMinMax;
    bool canFill = false;
    PCGrabber grabber;
    float barFillSpeedVal;

    private void Awake()
    {
        grabber = GetComponent<PCGrabber>();
        barFillSpeedVal = barFillSpeed;
    }

    private void Update()
    {
        if (grabber.currentlyGrabbed != null && Input.GetMouseButton(1))
            canFill = true;
        else
            canFill = false;

        if (canFill)
        {
            if (!powerBar.gameObject.activeSelf)
                powerBar.gameObject.SetActive(true);

            if (powerBar.fillAmount >= 1)
            {
                isFilling = false;
            }
            else if (powerBar.fillAmount <= 0)
            {
                isFilling = true;
            }

            powerBar.fillAmount += (isFilling) ? barFillSpeedVal : -barFillSpeedVal;
            barFillSpeedVal += barFillSpeedVal / 1000;
        }
        else if (barFillSpeedVal != barFillSpeed)
        {
            barFillSpeedVal = barFillSpeed;
        }
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     grabber.currentlyGrabbed.GetComponent<Ball>().ThrowBall(powerMinMax.y, transform.forward);
        // }

        if (Input.GetMouseButtonUp(1))
        {
            float force = (powerMinMax.y - powerMinMax.x * powerBar.fillAmount) + powerMinMax.x;
            float swayX = Random.Range(-swayMinMax.y, swayMinMax.y);
            Vector3 direction = transform.forward + new Vector3(swayX, 0, 0);

            if (grabber.currentlyGrabbed != null)
                grabber.currentlyGrabbed.GetComponent<Ball>().ThrowBall(force, direction);
            powerBar.fillAmount = 0;
            powerBar.gameObject.SetActive(false);
        }
    }
}
