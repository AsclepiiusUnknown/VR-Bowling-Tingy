using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoringSystem : MonoBehaviour
{
    TextMeshProUGUI displayElement;

    public void ScoreBowl(int _pinsDown)
    {
        displayElement.text = _pinsDown + " pins down!!";
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            ScoreBowl(GameManager.pinsDown);
        }
    }
}
