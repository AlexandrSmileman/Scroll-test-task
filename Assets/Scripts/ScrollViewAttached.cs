using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ScrollViewAttached : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private ScrollRect parentScrollRect;
    private ScrollRect currentScrollRect;
    private float startScrollNormalizedPosition;
    //flag of event transfer to the parent scroll
    private bool ignoreCurrentScrollDrag = false;

    private void Start()
    {
        currentScrollRect = GetComponent<ScrollRect>();
        parentScrollRect = transform.parent.GetComponentInParent<ScrollRect>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(ignoreCurrentScrollDrag)
        {
            //fix the position of the scroll on the start position
            currentScrollRect.horizontalNormalizedPosition = startScrollNormalizedPosition;
            //transfer control to the parent scroll
            ((IDragHandler)parentScrollRect).OnDrag(eventData);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //if the parent scroll exist and vertical scrolling is disabled on the current scroll,
        //check if the vertical offset is larger
        if (parentScrollRect != null && !currentScrollRect.vertical &&
            eventData.delta.y * eventData.delta.y > eventData.delta.x * eventData.delta.x)
        {
            ignoreCurrentScrollDrag = true;
            startScrollNormalizedPosition = currentScrollRect.horizontalNormalizedPosition;
            //send the event to the parent scroll
            ((IBeginDragHandler)parentScrollRect).OnBeginDrag(eventData);            
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //send the event to the parent scroll
        ((IEndDragHandler)parentScrollRect).OnEndDrag(eventData);
        ignoreCurrentScrollDrag = false;
    }
}
