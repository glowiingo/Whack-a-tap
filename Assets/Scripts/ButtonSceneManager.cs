using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneManager : MonoBehaviour
{
    [SerializeField] private string tutorialScene;

    public void ButtonChangeScene()
    {
        SceneManager.LoadScene(tutorialScene);
    }
}
