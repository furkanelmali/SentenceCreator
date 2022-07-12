using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Word : MonoBehaviour
{
    TextMeshProUGUI WordText;
    BoxCollider2D Collider;
    [SerializeField] private RectTransform _textRectTransform;

    // Start is called before the first frame update
    void Start()
    {   
        WordText = GetComponent<TextMeshProUGUI>();
        Collider = GetComponent<BoxCollider2D>();  
        _textRectTransform = GetComponent<RectTransform>();
        changingWidth();

    }

    // Update is called once per frame
    void Update()
    {
       
    }

   

    public void changingWidth()
    {

        int characternum = WordText.text.Length;
        float Size =  characternum * WordText.fontSize / 2f;
        Collider.size = new Vector2(Size,50); 
       _textRectTransform.sizeDelta=new Vector2(Size,50);
    }

    
}
