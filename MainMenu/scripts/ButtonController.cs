using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public RectTransform hedefObje;

    public Vector2 hedefBoyutB;
    public Vector2 hedefBoyutK;

    public void enter()
    {
        hedefObje.sizeDelta = hedefBoyutB;
    }
    public void exit()
    {
        hedefObje.sizeDelta = hedefBoyutK;
    }
}
