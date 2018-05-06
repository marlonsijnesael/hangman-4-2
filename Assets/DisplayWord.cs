using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayWord : MonoBehaviour {

    public Text displayWord;


    public void Update()
    {
        displayWord.text = NewText();
    }


    private string NewText()
    {
        string _newText = "";
        for (int i = 0; i < Words._Instance.currentWordSplitUp.Length; i++)
        {
            _newText = _newText + Words._Instance.currentLettersFilled[i];
        }
        return _newText;
    }
}
