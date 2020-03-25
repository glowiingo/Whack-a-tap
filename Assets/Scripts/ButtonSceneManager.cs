using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneManager : MonoBehaviour
{
    /// <summary>
    /// This method will load the scene that is specified by the string paramter.
    /// </summary>
    /// <param name="scene">a string, the name of the Scene to load</param>
    public void ButtonChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
