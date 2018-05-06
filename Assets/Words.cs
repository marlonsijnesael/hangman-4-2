using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Words : MonoBehaviour {

    public Wordclass[] words;
    public InputField inputfield;

    public int wordIndex;
    public string[] currentWordSplitUp;
    public string[] currentLettersFilled;



    #region singleton
    public static Words _Instance;
    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }

        else
        {
            Destroy(this);
        }
    }
    #endregion

    private void Start()
    {
        SplitWords();

        //limit input to one letter
        inputfield.characterLimit = 1;
     
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SplitWords();
        }

        //if (inputfield.onValueChanged)
        {

        }
    }


    //checks if submitted letter is part of current word;
    public void LoopThroughWord(string letter)
    {
        for (int i = 0; i < words[wordIndex].word.Length; i++)
        {
            if (currentWordSplitUp[i] == letter)
            {
                currentLettersFilled[i] = letter;
            }
        }
    }

    //split up word and sets displayword to length of current word
    public void SplitWords()
    {
        currentWordSplitUp = new string[words[wordIndex].word.Length];
        currentLettersFilled = new string[currentWordSplitUp.Length];

        for (int i = 0; i < words[wordIndex].word.Length; i++)
        {
            currentWordSplitUp[i] = System.Convert.ToString(words[wordIndex].word[i]);
        }
        wordIndex++;
    }
}
