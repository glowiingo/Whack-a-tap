using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class is used as a wrapper for the ButtonSceneManager. This class allows
/// the ButtonSceneManager to be instantiated and cause scene changes without
/// the need for onClick events.
/// </summary>
public class SceneChanger : MonoBehaviour
{
    ButtonSceneManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = new ButtonSceneManager();
    }

    /// <summary>
    /// Call the ButtonSceneManager's method to change scenes, passing
    /// the name of the scene to it as a string.
    /// </summary>
    /// <param name="scene"></param>
    public void triggerSceneChange(string scene)
    {
        manager.ButtonChangeScene(scene);
    }
}
