using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class DropPoints : MonoBehaviour,IDropHandler
{
    public bool dropped ;
    public int droppedPoint = 0;
    public int draggedWord = 0;
    Sentence sentence;
    MainScript main;
    public int indicesx = 0; 
    DraggingWords[] draggingwords;
    public void Start()
    {
        sentence = FindObjectOfType<Sentence>();
        main = FindObjectOfType<MainScript>();
        draggingwords = FindObjectsOfType<DraggingWords>();
        dropped = true;
    }
    

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().transform.localPosition = sentence.SentencePoses[sentence.point-1].transform.localPosition;
            
            sentence.dropP = droppedPoint;
        }
    }
}
