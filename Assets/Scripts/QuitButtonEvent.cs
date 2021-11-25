using UnityEngine;
using UnityEngine.EventSystems;

public class QuitButtonEvent : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
    }
}
