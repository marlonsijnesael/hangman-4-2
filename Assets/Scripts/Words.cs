using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//this class handles everything to do with the handling of words and input
public class Words : MonoBehaviour {

    public Wordclass[] words;
    public InputField inputfield;
    public GameObject win_Screen;
    public Text win_TextPlayer, win_TextWord;
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
        //limit input to one letter
        inputfield.characterLimit = 1;
    }

    private void Update()
    {
        if (CheckIfWordIsGuessed())
        {
            OpenWinPanel();
            StringToArray();
            RefreshText();
        }
    }

    //opens when word is guessed right: Displays word and player that guessed the word
    private void OpenWinPanel()
    {
        win_Screen.SetActive(true);
        win_TextPlayer.text = "Guessed by player: " + TurnManager.currentPlayer;
        win_TextWord.text = "Word: " + currentWord;
    }

    //set UI text to guessed letters
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
        //sets empty letters to "x" 
        for (int i = 0; i < currentWordSplitUp.Length; i++)
        {
            if (currentLettersFilled[i] == null)
            {
                currentLettersFilled[i] = "x";
            }
        }

        //checks if word contains submitted letter. if true, loops through word and grants point for each time the letter occurs
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
    
        else if (!currentWord.Contains(letter))
        {
            TurnManager.currentPlayer.wrongAnswers++;
        }
    }

    //checks if all strings in arrray are filled in
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
        //picks random word from list
        wordIndex = Random.Range(0, words.Length - 1);
        currentWord = words[wordIndex].word;
        
        //sets both split up word array and filled in letters array to length of the picked word
        currentWordSplitUp = new string[words[wordIndex].word.Length];
        currentLettersFilled = new string[currentWordSplitUp.Length];
        inputfield.text = null;
        textUI = "";
        
        //splits up selected word into letters and add them to split up word array, then calls checkinput to set letter to "x" in UI
        for (int i = 0; i < words[wordIndex].word.Length; i++)
        {
            currentWordSplitUp[i] = words[wordIndex].word[i].ToString();
            CheckInput();
        }

        wordIndex++;
    }


}
