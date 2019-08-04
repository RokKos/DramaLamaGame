using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] AudioSource sfx_btn_hover_;

    public void OnPointerEnter(PointerEventData eventData)
    {
        sfx_btn_hover_.Play();
    }
}
