using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DraggingWords : MonoBehaviour , IPointerDownHandler, IBeginDragHandler, IEndDragHandler , IDragHandler
{
    private RectTransform RectTransf;
    Canvas canvas;
    private CanvasGroup Canvasgroup;
    public void Start()
    {
        RectTransf  = GetComponent<RectTransform>(); 
        Canvasgroup = GetComponent<CanvasGroup>();
        canvas = FindObjectOfType<Canvas>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin");
        Canvasgroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        RectTransf.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end");
        Canvasgroup.blocksRaycasts = true;
    }

  
}
