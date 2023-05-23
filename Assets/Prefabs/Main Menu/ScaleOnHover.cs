using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ScaleOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 defaultScale = new Vector3(1f, 1f, 1f);
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f);

    public float duration = 0.2f; // The duration of the animation

    private bool isHovering = false;

    private void Update()
    {
        float step = (isHovering ? hoverScale : defaultScale).x / duration * Time.unscaledDeltaTime;
        transform.localScale = Vector3.MoveTowards(transform.localScale, isHovering ? hoverScale : defaultScale, step);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
}
