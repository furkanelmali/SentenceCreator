using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
public class DraggingWords : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerUpHandler
{
    private RectTransform RectTransf;
    Canvas canvas;
    private CanvasGroup Canvasgroup;

    TextMeshProUGUI label;
    MainScript main;
    Sentence sentence;
    public string thisText;
    public float time = 0.5f;
    DropPoints dpoint;
    public UnityEvent onClick;
    [SerializeField] Color defaultC = Color.black;
    [SerializeField] Color dragC = Color.gray;
    public int indices = 0;

    public int draggedOne = 0;
    public int droppedOne = 0;
    public float lengthofthisword;


    public int x = 0;
    public Vector2 firstpos;
    public int firstword1, firstword2;
    public int secondword1, secondword2;
    public int turning = 0;

    int y = 0;
    int z = 0;
    public void Update()
    {
    }

    public void Start()
    {
        RectTransf = GetComponent<RectTransform>();
        Canvasgroup = GetComponent<CanvasGroup>();
        sentence = FindObjectOfType<Sentence>();
        canvas = FindObjectOfType<Canvas>();
        label = GetComponent<TextMeshProUGUI>();
        main = FindObjectOfType<MainScript>();
        dpoint = FindObjectOfType<DropPoints>();
        firstposes();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        for (int i = 0; i <= sentence.correctorder.Length; i++)
        {
            if (sentence.correctorder[i].transform.localPosition == this.transform.localPosition)
            {
                y = i;
                Debug.Log("correctorder" + turning);
                break;
            }
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {

        dpoint.dropped = true;
        Canvasgroup.blocksRaycasts = false;
        TextMeshProUGUI textt = this.gameObject.GetComponent<TextMeshProUGUI>();
        label.color = dragC;

        lengthofthisword = this.gameObject.GetComponent<RectTransform>().sizeDelta.x;
        thisText = textt.ToString();
        whichisdragging();

        if (this.gameObject.transform.localPosition.y == sentence.randomorder[draggedOne].y)
        {
            sentence.dragP = draggedOne;
            sentence.sentenceposeplacerdragv(sentence.dragP, lengthofthisword);
        }

    }
    public void OnDrag(PointerEventData eventData)
    {
        RectTransf.anchoredPosition += eventData.delta / canvas.scaleFactor;

    }
    public void OnEndDrag(PointerEventData eventData)
    {

        Canvasgroup.blocksRaycasts = true;
        sentence.correctCheck(label, draggedOne);

        dpoint.dropped = false;
        goingbacktofirstpose();




    }

    public void whichisdragging()
    {
        for (int i = 0; i < sentence.correctorder.Length; i++)
        {
            if (sentence.correctorder[i].transform == transform)
            { draggedOne = i; break; }

        }

    }

    public void goingbacktofirstpose()
    {
        for (int i = 0; i < sentence.SentencePoses.Length; i++)
        {
            if (transform.localPosition == sentence.SentencePoses[i].transform.localPosition)
            {
                return;
            }
        }

        for (int i = 0; i <= main.words.Length; i++)
        {
            if (sentence.correctorder[draggedOne].transform.localPosition == sentence.userssentence[i].transform.localPosition)
            {
                x = i;
                break;
            }
        }

        while (sentence.userssentence[x + 1] != null)
        {
            sentence.point = x;
            float length = sentence.userssentence[x + 1].GetComponent<RectTransform>().sizeDelta.x;
            sentence.sentenceposeplacerdragv(x + 1, length);
            sentence.userssentence[x + 1].transform.localPosition = sentence.SentencePoses[x].transform.localPosition;
            sentence.userssentence[x] = sentence.userssentence[x + 1];
            sentence.point = x + 1;

            for (int i = 0; i <= sentence.correctorder.Length; i++)
            {
                if (sentence.correctorder[i] == sentence.userssentence[sentence.point])
                {
                    turning = i;
                    break;
                }
            }


            TextMeshProUGUI color = sentence.userssentence[sentence.point - 1].GetComponent<TextMeshProUGUI>();
            sentence.correctCheck(color, turning);
            x++;


        }

        if (sentence.userssentence[sentence.point] == null)
        {
            sentence.userssentence[sentence.point - 1] = null;
        }
        else
        {
            sentence.userssentence[sentence.point] = null;
        }



        for (int i = 0; i <= sentence.userssentence.Length; i++)
        {
            if (sentence.userssentence[i] == null)
            {
                sentence.point = i;
                break;
            }
        }



        sentence.correctorder[draggedOne].transform.localPosition = sentence.randomorder[draggedOne];
        label.color = defaultC;
        x = 0;
    }

    public void firstposes()
    {
        sentence.randomorder[sentence.x] = this.transform.localPosition;
        sentence.x++;
    }

    public void OnDrop(PointerEventData eventData)
    {
        bool thisisnotchange = false;
        for (int i = 0; i < sentence.randomorder.Length; i++)
        {
            if (new Vector2(this.GetComponent<RectTransform>().transform.localPosition.x, this.GetComponent<RectTransform>().transform.localPosition.y)
                == sentence.randomorder[i])
            {
                thisisnotchange = true;
            }
        }

        if (!thisisnotchange)
        {

            StartCoroutine(getpos());



            eventData.pointerDrag.GetComponent<RectTransform>().transform.localPosition = this.GetComponent<RectTransform>().transform.localPosition;


            DraggingWords dw = eventData.pointerDrag.GetComponent<DraggingWords>();
            this.GetComponent<RectTransform>().transform.localPosition = dw.firstpos;




            for (int i = 0; i < sentence.userssentence.Length; i++)
            {
                if (this.transform.localPosition == sentence.userssentence[i].transform.localPosition)
                {
                    secondword1 = i;
                    break;
                }
            }
            TextMeshProUGUI temp = sentence.userssentence[dw.firstword1];
            sentence.userssentence[dw.firstword1] = sentence.userssentence[secondword1];
            sentence.userssentence[secondword1] = temp;
            StartCoroutine(getpos());


        }

        thisisnotchange = false;




    }



    public void OnPointerUp(PointerEventData eventData)
    {
        StartCoroutine(getpos());
        for (int i = 0; i < sentence.userssentence.Length; i++)
        {

            if (this.transform.localPosition == sentence.userssentence[i].transform.localPosition)
            {
                firstword1 = i;
                break;
            }

        }

    }

    public void orderSententce()
    {
        for (int i = 0; i < sentence.userssentence.Length; i++)
        {
            if (sentence.userssentence[i] != null)
            {

                if (sentence.userssentence[i].text == sentence.correctorder[i].text)
                {
                    sentence.userssentence[i].color = sentence.correct;
                }

                else { sentence.userssentence[i].color = sentence.wrong; }
            }

        }

        int index = 1;
        Vector2 wordPosition = sentence.SentencePoses[0].transform.localPosition;
        sentence.userssentence[0].transform.localPosition = wordPosition;
        float firstwordlength = sentence.userssentence[0].GetComponent<RectTransform>().sizeDelta.x;

        float yPosition = sentence.SentencePoses[0].transform.localPosition.y;
        float xPosition = wordPosition.x;
        while (sentence.userssentence[index] != null)
        {
            float firstLength = sentence.userssentence[index - 1].GetComponent<RectTransform>().sizeDelta.x / 2f;
            float secondLength = sentence.userssentence[index].GetComponent<RectTransform>().sizeDelta.x / 2f;
            float wordLength = firstLength + secondLength + 20;
            xPosition = sentence.SentencePoses[index - 1].transform.localPosition.x + wordLength;


            if (xPosition > 636.94)
            {
                if (firstwordlength != sentence.userssentence[0].GetComponent<RectTransform>().sizeDelta.x)
                {

                    if (sentence.userssentence[index].GetComponent<RectTransform>().sizeDelta.x < firstwordlength)
                    {
                        float thiss = (sentence.userssentence[index].GetComponent<RectTransform>().sizeDelta.x - firstwordlength) / 2;
                        Debug.Log("This:" + thiss);
                        xPosition = wordPosition.x - 75 - (firstwordlength / 2) + (sentence.userssentence[index].GetComponent<RectTransform>().sizeDelta.x / 2);
                        yPosition = yPosition - 50f;
                    }
                    else if (sentence.userssentence[index].GetComponent<RectTransform>().sizeDelta.x > firstwordlength)
                    {

                        float thiss = sentence.userssentence[index].GetComponent<RectTransform>().sizeDelta.x - firstwordlength;
                        Debug.Log("This:" + thiss);
                        xPosition = wordPosition.x - 75 - (firstwordlength / 2) + (sentence.userssentence[index].GetComponent<RectTransform>().sizeDelta.x / 2);
                        yPosition = yPosition - 50f;
                    }

                }
                else
                {
                    xPosition = wordPosition.x - 75;
                    yPosition = yPosition - 50f;
                }

                firstwordlength = sentence.userssentence[index].GetComponent<RectTransform>().sizeDelta.x;


            }
            sentence.SentencePoses[index].transform.localPosition = new Vector2(xPosition, yPosition);
            sentence.userssentence[index].transform.localPosition = new Vector2(xPosition, yPosition);
            index++;
            Debug.Log("gidiyor");

            sentence.firstpos = sentence.userssentence[0].GetComponent<RectTransform>().localPosition;
        }
        sentence.ychange = 50;
    }

    public IEnumerator getpos()
    {
        yield return new WaitForSeconds(0.2f);
        firstpos = this.gameObject.GetComponent<RectTransform>().anchoredPosition;
        orderSententce();
    }
}