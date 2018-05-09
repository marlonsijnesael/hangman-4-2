using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// loads desired scene
/// </summary>
public class LoadScene : MonoBehaviour {

    
	public void NextScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
