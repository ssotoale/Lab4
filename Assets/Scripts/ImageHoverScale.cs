
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageHoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleFactor = 1.2f; // How much to enlarge the image
    private Vector3 originalScale;

    void Awake()  // Runs only once when the GameObject is created
    {
        originalScale = transform.localScale;
    }

    void OnEnable()
    {
        transform.localScale = originalScale; // Reset to default when enabled
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * scaleFactor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }
}
