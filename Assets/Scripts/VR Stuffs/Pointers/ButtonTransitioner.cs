using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTransitioner : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler,IPointerUpHandler,IPointerClickHandler
{
    public Color32 normalColor = Color.white;
    public Color32 hoverColor = Color.grey;
    public Color32 downColor = Color.white;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData _eventData)
    {
        print("Enter");
        image.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData _eventData)
    {
        print("Exit");
        image.color = normalColor;
    }

    public void OnPointerDown(PointerEventData _eventData)
    {
        print("Down");
        image.color = downColor;
    }

    public void OnPointerUp(PointerEventData _eventData)
    {
        print("Up");
    }

    public void OnPointerClick(PointerEventData _eventData)
    {
        print("Click");
        image.color = hoverColor;
    }
}
