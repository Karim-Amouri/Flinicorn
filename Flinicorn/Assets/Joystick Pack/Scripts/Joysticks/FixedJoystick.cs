using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{
    //[SerializeField] private RectTransform baseRectSnap;
    private Vector2 baseRectPositionSnap;

    override
    public void OnDrag(PointerEventData eventData)
    {
        /*cam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            cam = canvas.worldCamera;
        */
        Vector3 vect = cam.WorldToScreenPoint(background.transform.GetComponent<Transform>().position);
        baseRectPositionSnap = new Vector2(vect.x, vect.y);
        Vector2 radius = background.sizeDelta / 2;
        input = (eventData.position - baseRectPositionSnap) / (radius * canvas.scaleFactor);
        FormatInput();
        HandleInput(input.magnitude, input.normalized, radius, cam);
        handle.anchoredPosition = input * radius * handleRange;
    }
}