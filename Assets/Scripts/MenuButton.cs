using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMPro.TMP_Text text;

    public Color MainColor;
    public Color HoverColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = HoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = MainColor;
    }
}
