using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Words : MonoBehaviour {

    public Wordclass[] words;
    public InputField inputfield;
    public Text displayWord;

    public int wordIndex;
    public string textUI;
    private string currentWord;
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
        StringToArray();
        RefreshText();
       // LoopThroughWord(inputfield.text); ;

        //limit input to one letter
        inputfield.characterLimit = 1;
    }

    private void Update()
    {
        if (CheckIfWordIsGuessed())
        {
            StringToArray();
            RefreshText();
        }
    }

    //set UI text to guessed letters;
    public void RefreshText()
    {
        textUI = "";
        for (int i = 0; i < currentWordSplitUp.Length; i++)
        {
            textUI = textUI + currentLettersFilled[i];
        }
        displayWord.text =  textUI;
    }

    //called from the text imput field
    public void CheckInput()
    {
        LoopThroughWord(inputfield.text);
        inputfield.text = "";
    }

    //checks if submitted letter is part of current word, also sets all letters to `X` when new word is selected
    private void LoopThroughWord(string letter)
    {

        for (int i = 0; i < currentWordSplitUp.Length; i++)
        {
            if (currentLettersFilled[i] == null)
            {
                currentLettersFilled[i] = "x";
            }
        }


        if (letter != null && currentWord.Contains(letter))
        {
            for (int i = 0; i < currentWordSplitUp.Length; i++)
            {
                if (currentWordSplitUp[i] == letter)
                {
                    currentLettersFilled[i] = letter;
                    TurnManager.currentPlayer.score += 1;
                }
            }
        }

        else
        {
            TurnManager.currentPlayer.wrongAnswers++;
        }
    }
    private bool CheckIfWordIsGuessed()
    {
        for (int i = 0; i < currentWordSplitUp.Length; i++)
        {
            if (currentWordSplitUp[i] != currentLettersFilled[i])
            {
                return false;
            }
        }
        return true;
    }

    //split up word and sets displayword to length of current word
    private void StringToArray()
    {
        //reset wordindex when end of words is reached
        if (wordIndex >= words.Length)
        {
            wordIndex = 0;
        }

        currentWord = words[wordIndex].word;
        currentWordSplitUp = new string[words[wordIndex].word.Length];
        currentLettersFilled = new string[currentWordSplitUp.Length];
        inputfield.text = null;
        textUI = "";
        
        for (int i = 0; i < words[wordIndex].word.Length; i++)
        {
            currentWordSplitUp[i] = words[wordIndex].word[i].ToString();

            CheckInput();
        }
        wordIndex++;
    }


}
