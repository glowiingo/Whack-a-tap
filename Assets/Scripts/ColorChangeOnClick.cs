using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeOnClick : MonoBehaviour
{
    /// <summary>
    /// This function will update the color of the gameObject the script is attached to 
    /// depending on whether the object has been changed to a green state or not.
    /// </summary>
    public void updateColorOnClick() {
        if (gameObject.GetComponent<Renderer>().material.color == Color.green)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else {
            changeColor(gameObject);
        }
    }

    /// <summary>
    /// This function will call the coroutine to change the color
    /// of the specified game object to red and back to yellow.
    /// </summary>
    /// <param name="gObject">The specified game object to change the color of.</param>
    public void changeColor(GameObject gObject)
    {
        StartCoroutine(colorChangeRedYellowCoroutine(gObject));
    }


    /// <summary>
    /// The coroutine changes the color of the gameObject to red and then to yellow after 1 second.
    /// </summary>
    /// <param name="gameObject"> The specified game obejct to change the color of. </param>
    /// <returns></returns>
    IEnumerator colorChangeRedYellowCoroutine(GameObject gameObject)
    {
        // For Debuging: Print the time of when the function is first called.
        // Debug.Log("Started Coroutine at timestamp : " + Time.time);

        gameObject.GetComponent<Renderer>().material.color = Color.red;
        //yield on a new YieldInstruction that waits for 1 seconds.
        yield return new WaitForSeconds(1.0f);
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        // Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

}
