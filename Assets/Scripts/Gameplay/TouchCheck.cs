using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCheck : MonoBehaviour
{
    public GameObject pin;
    public string pinTag = "Pin";
    private bool isDown = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != pinTag && !isDown)
        {
            PinDown();
            isDown = true;
        }
    }

    private IEnumerator PinDown()
    {
        GameManager.pinsDown++;
        print("Pin Down. Great Job!!");
        yield return new WaitForSeconds(3);
        pin.SetActive(false);
    }
}
