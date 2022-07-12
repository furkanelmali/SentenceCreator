using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MainScript : MonoBehaviour
{
    public string[] sentences;
    public string[] words;

    public int sentencenum = 0;

    public bool checkforSolution;

    TextMeshProUGUI text;

    Sentence sentence;
    SpawnPoses sPoses;


    // Start is called before the first frame update
    void Start()
    {
        checkforSolution = true;
        sentence = FindObjectOfType<Sentence>();
    }

    // Update is called once per frame
    void Update()
    {



    }
    public void seperator()
    {
        words = sentences[sentencenum].Split();
        foreach (string word in words)
        {
            Debug.Log(word);
        }

    }

    public void nextBtn()
    {
        if (sentencenum < sentences.Length - 1)
        {
            Clean();
            sentencenum++;
            seperator();
            sentence.RandomSpawner();
        }

        checkforSolution = true;

    }

    public void backBtn()
    {

        if (sentencenum > 0)
        {
            Clean();
            sentencenum--;
            seperator();
            sentence.RandomSpawner();


        }

        checkforSolution = true;

    }

    public void SolutionBtn()
    {
        if (checkforSolution)
        {
            int index = 1;
            Vector2 wordPosition = sentence.SentencePoses[0].transform.localPosition;
            Vector2 firstwordpos = sentence.SentencePoses[0].transform.localPosition;
            sentence.correctorder[0].transform.localPosition = wordPosition;
            float firstwordlength = sentence.correctorder[0].GetComponent<RectTransform>().sizeDelta.x;

            float yPosition = sentence.SentencePoses[0].transform.localPosition.y;
            float xPosition = wordPosition.x;
            while (sentence.correctorder[index] != null)
            {
                float firstLength = sentence.correctorder[index - 1].GetComponent<RectTransform>().sizeDelta.x / 2f;
                float secondLength = sentence.correctorder[index].GetComponent<RectTransform>().sizeDelta.x / 2f;
                float wordLength = firstLength + secondLength + 20;
                xPosition = sentence.SentencePoses[index - 1].transform.localPosition.x + wordLength;


                if (xPosition > 600)
                {
                    if (firstwordlength != sentence.correctorder[0].GetComponent<RectTransform>().sizeDelta.x)
                    {
                       
                        if (sentence.correctorder[index].GetComponent<RectTransform>().sizeDelta.x < firstwordlength)
                        {
                            float thiss = (sentence.correctorder[index].GetComponent<RectTransform>().sizeDelta.x -firstwordlength)/2;
                            Debug.Log("This:" + thiss);
                            xPosition = wordPosition.x - 75 - (firstwordlength/2) + (sentence.correctorder[index].GetComponent<RectTransform>().sizeDelta.x/2);
                            yPosition = yPosition - 50f;
                        }
                        else if (sentence.correctorder[index].GetComponent<RectTransform>().sizeDelta.x > firstwordlength)
                        {

                            float thiss = sentence.correctorder[index].GetComponent<RectTransform>().sizeDelta.x - firstwordlength;
                            Debug.Log("This:" + thiss);
                            xPosition = wordPosition.x - 75 - (firstwordlength / 2) + (sentence.correctorder[index].GetComponent<RectTransform>().sizeDelta.x / 2);
                            yPosition = yPosition - 50f;
                        }

                    }
                    else
                    {
                        xPosition = wordPosition.x - 75;
                        yPosition = yPosition - 50f;
                    }

                    firstwordlength = sentence.correctorder[index].GetComponent<RectTransform>().sizeDelta.x;
                    ;

                }
                sentence.SentencePoses[index].transform.localPosition = new Vector2(xPosition, yPosition);
                sentence.correctorder[index].transform.localPosition = new Vector2(xPosition, yPosition);
                sentence.correctorder[index].color = Color.black;
                CanvasGroup canvas = sentence.correctorder[0].GetComponent<CanvasGroup>();
                canvas.blocksRaycasts = false;
                CanvasGroup canvass = sentence.correctorder[index].GetComponent<CanvasGroup>();
                sentence.correctorder[0].color = Color.black;
                canvass.blocksRaycasts = false;
                index++;
                Debug.Log("gidiyor");


                sentence.firstpos = sentence.correctorder[0].GetComponent<RectTransform>().localPosition;
            }

            checkforSolution = false;

        }

    }

    public void CleanBtn()
    {
        Clean();
        sentence.RandomSpawner();
        checkforSolution = true;
    }

    public void Clean()
    {
        var clones = GameObject.FindGameObjectsWithTag("Clone");

        foreach (var clone in clones)
        {
            Destroy(clone.gameObject);
        }

        for (int i = 0; i < sentence.correctorder.Length; i++)
        {
            sentence.correctorder[i] = null;

        }
        for (int i = 0; i < sentence.userssentence.Length; i++)
        {
            sentence.userssentence[i] = null;

        }
        for (int i = 0; i < sentence.randomorder.Length; i++)
        {
            sentence.randomorder[i] = new Vector2(0, 0);

        }
        for (int i = 0; i < sentence.SpawnPoints.Length; i++)
        {
            sPoses = sentence.SpawnPoints[i].GetComponent<SpawnPoses>();

            sPoses.isthereword = false;
            sentence.SpawnPoints[i].gameObject.SetActive(false);
            sentence.SpawnPointsImages[i].gameObject.SetActive(false);
        }

        sentence.p = 0;
        sentence.ychange = 50f;
        sentence.goingbacktofirstposes();
        sentence.x = 0;
        sentence.point = 0;
    }




}