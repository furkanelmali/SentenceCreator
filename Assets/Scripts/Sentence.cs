using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class Sentence : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject[] SpawnPointsImages;
    public GameObject[] Words;
    public GameObject[] SentencePoses;
    public Vector2[] FirstSentencePoses;
    public Vector2[] randomorder;
    public TextMeshProUGUI WordPrefab;
    public GameObject Canvass;

    public TextMeshProUGUI word;
    public TextMeshProUGUI[] correctorder;
    
    public GameObject firstposegroup;
    public TextMeshProUGUI[] userssentence;

    private CanvasGroup canvasGroup;


    MainScript main;
    SpawnPoses sposes;
    SentencePoses sentenceposes;

    public Color correct = Color.green;
    public Color wrong = Color.red;
    public float ychange = 50f;
    public int dragP = 0;
    public int dropP = 0;
    public int p = 0;
    public int x = 0;

    public Vector2 firstpos;
    public int point = 0;
    public float firstwordLength = 0;
    // Start is called before the first frame update
    void Start()
    {
        main = FindObjectOfType<MainScript>();
       
        
        main.seperator();
        RandomSpawner();
        takingfirstposes();

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void SpawnPointsActiver() 
    {
        for (int i = 0; i < main.words.Length; i++)
        {
            SpawnPoints[i].gameObject.SetActive(true);
            SpawnPointsImages[i].gameObject.SetActive(true);
        }

    }

    public void RandomSpawner() 
    {
        SpawnPointsActiver();

        for (int i = 0;i < main.words.Length; i++)
        {
          
            int randomnum = Random.Range(0, main.words.Length);
             Vector2 pos = SpawnPoints[randomnum].transform.localPosition;

            sposes = SpawnPoints[randomnum].GetComponent<SpawnPoses>();

            
            if (sposes.isthereword == false) 
            {
                word = Instantiate(WordPrefab, SpawnPoints[randomnum].transform);
                word.transform.SetParent(Canvass.transform);
                word.gameObject.tag = "Clone";
                WordPlacer();
                sposes.isthereword = true;
            }
            else
            {
                while(sposes.isthereword == true) 
                {
                   randomnum = Random.Range(0, main.words.Length);
                   pos = SpawnPoints[randomnum].transform.localPosition;

                   sposes = SpawnPoints[randomnum].GetComponent<SpawnPoses>();
                }

                if (sposes.isthereword == false)
                {
                    word = Instantiate(WordPrefab, SpawnPoints[randomnum].transform);
                    word.gameObject.tag = "Clone";
                    word.transform.SetParent(Canvass.transform);
                    WordPlacer();
                    sposes.isthereword = true;
                }
            }

        } 
    }

    void WordPlacer()
    {
        word.text = main.words[p];
        correctorder[p] = word;
         p++;
    }

    public void SentencePosePlacer(int droppoint,int dragpoint) 
    {
            Vector2 firstpos = SentencePoses[droppoint].transform.localPosition;

            float firstwordLength = correctorder[dragpoint].GetComponent<RectTransform>().sizeDelta.x;
            float secondwordLength = correctorder[dragpoint+1].GetComponent<RectTransform>().sizeDelta.x;
            Vector2 horizonstart = SentencePoses[0].transform.localPosition;
            float ff = correctorder[0].GetComponent<RectTransform>().sizeDelta.x/2;
            float road = (firstwordLength / 2) +  (secondwordLength / 2) + 15f;
            Debug.Log(firstpos.x);
            float secondpos = firstpos.x + road;
            if(secondpos <= 636.94) 
            {
                SentencePoses[droppoint + 1].transform.localPosition = new Vector2(firstpos.x + road, firstpos.y);
            }
            else 
            {
                SentencePoses[droppoint + 1].transform.localPosition = new Vector2(horizonstart.x-ff, horizonstart.y - ychange);
                ychange = ychange + 50f;
            }

    }
    public void sentenceposeplacerdragv(int dragpoint, float lengthofsecondword) 
    {
        Vector2 firstpos;
        reordering(dragpoint);
        
        if (point != 0 )
        {

            firstpos = SentencePoses[point - 1].transform.localPosition;

            Vector2 horizonstart = SentencePoses[0].transform.localPosition;
            float ff = userssentence[0].GetComponent<RectTransform>().sizeDelta.x / 2;

            firstwordLength = userssentence[point - 1].GetComponent<RectTransform>().sizeDelta.x;

            float road = firstwordLength / 2 + (lengthofsecondword / 2) + 15f;

            float secondpos = firstpos.x + road;

            if (secondpos <= 699.94)
            {
                SentencePoses[point].transform.localPosition = new Vector2(firstpos.x + road, firstpos.y);
               
            }
            else
            {
                SentencePoses[point].transform.localPosition = new Vector2(horizonstart.x - ff, horizonstart.y - ychange);
               
                ychange = ychange + 50f;
            }


        }
        else
        {
            firstpos = SentencePoses[point].transform.localPosition;
            Vector2 horizonstart = SentencePoses[0].transform.localPosition;
            float ff = userssentence[0].GetComponent<RectTransform>().sizeDelta.x / 2;

            firstwordLength = userssentence[point].GetComponent<RectTransform>().sizeDelta.x;

            float road = firstwordLength / 2 + (lengthofsecondword / 2) + 15f;

            float secondpos = firstpos.x + road;

            if (secondpos <= 699.94)
            {
                SentencePoses[point].transform.localPosition = new Vector2(firstpos.x, firstpos.y);
            }
        
        }

        Debug.Log(point);
        point++;


    }


    public void goingbacktofirstposes() 
    {
        for (int i = 0; i < SentencePoses.Length; i++)
        {
            SentencePoses[i].transform.localPosition = FirstSentencePoses[i];
            ychange = 50f;
        }
    }


    public void takingfirstposes() 
    {
        for(int i = 0; i < SpawnPoints.Length; i++) 
        {
            FirstSentencePoses[i] = SentencePoses[i].transform.localPosition;
        }

    
    }

    public void correctCheck(TextMeshProUGUI label,int text)
    {
        
        if (correctorder[point-1] != null)
        {
           
            
            if (correctorder[text].text == correctorder[point-1].text)
            {
                label.color = correct;
                //canvasGroup = correctorder[dragP].GetComponent<CanvasGroup>();
                //canvasGroup.blocksRaycasts = false;
            }
             
            else
            {  
                label.color = wrong;
                //canvasGroup = correctorder[dragP].GetComponent<CanvasGroup>();
                //canvasGroup.blocksRaycasts = true;
            }
            }
        
    }


    public void reordering(int drag) 
    {
       foreach(var User in userssentence) 
        {
            if (User == correctorder[drag])
            {
                return;
            }
        }
        userssentence[point] = correctorder[drag];
    }


}
