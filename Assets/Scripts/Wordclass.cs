using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// class for words, extra atributes can be added like score later
/// </summary>
[System.Serializable]
public class Wordclass
{
    public string word;

    public Wordclass(string _word)
    {
        word = _word;
    }
}

