using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sentence : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject[] Words;
    public GameObject[] SentencePoses;
    public TextMeshProUGUI WordPrefab;
    public GameObject Canvass;

    public TextMeshProUGUI word;
    public TextMeshProUGUI[] correctorder;


    MainScript main;
    SpawnPoses sposes;

    public int p = 0;
    // Start is called before the first frame update
    void Start()
    {
        main = FindObjectOfType<MainScript>();
       
        main.seperator();
        
        RandomSpawner();
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

        }

    }

    public void RandomSpawner() 
    {
        SpawnPointsActiver();

        for (int i = 0;i < main.words.Length; i++)
        {
          
            int randomnum = Random.Range(0, main.words.Length);
             Vector2 pos = SpawnPoints[randomnum].transform.position;

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
                   pos = SpawnPoints[randomnum].transform.position;

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
}
