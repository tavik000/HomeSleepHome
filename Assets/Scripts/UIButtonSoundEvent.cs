using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIButtonSoundEvent : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public AudioSource buttonHighlightSound;
    public AudioSource buttonClickSound;

    public void OnPointerEnter(PointerEventData ped)
    {
        buttonHighlightSound.Play();
    }

    public void OnPointerDown(PointerEventData ped)
    {
        buttonClickSound.Play();
    }
}