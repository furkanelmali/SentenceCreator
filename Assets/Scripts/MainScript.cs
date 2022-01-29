using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainScript : MonoBehaviour
{
    public string[] sentences;
    public string[] words;

    public int sentencenum = 0;

    TextMeshProUGUI text;

    Sentence sentence;
    SpawnPoses sPoses;
   
    // Start is called before the first frame update
    void Start()
    {
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
        Clean();
        sentencenum++;
        seperator();
        sentence.RandomSpawner();

    }

    public void backBtn()
    {
        Clean();
        Clean();
        sentencenum--;
        seperator();
        sentence.RandomSpawner();
    }   

    public void SolutionBtn()
    {
        for (int i=0; i<words.Length;i++) 
        {
            sentence.correctorder[i].transform.position = sentence.SentencePoses[i].transform.position;
        }
       
    
    }

    public void CleanBtn() 
    {
        Clean();
        sentence.RandomSpawner();
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

        for (int i = 0; i < sentence.SpawnPoints.Length; i++)
        {
            sPoses = sentence.SpawnPoints[i].GetComponent<SpawnPoses>();
            sPoses.isthereword = false;
        }

        sentence.p = 0;
    }
}
