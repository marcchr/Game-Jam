using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float holdDuration = 3f;
    [SerializeField] Image _strengthFill;

    private float holdTimer = 0;
    private bool buttonHeld = false;

    void Update ()
    {
        if (buttonHeld == true)
        {
            holdTimer += Time.deltaTime;
            _strengthFill.fillAmount = holdTimer / holdDuration;

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonHeld = true;
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonHeld = false;
        holdTimer = 0;
        TongueShoot.Instance.ShootTongue(_strengthFill.fillAmount);
        _strengthFill.fillAmount = 0;

    }
}

