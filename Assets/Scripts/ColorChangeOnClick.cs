using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeOnClick : MonoBehaviour
{
    public void updateColorOnClick() {
        if (gameObject.GetComponent<Renderer>().material.color == Color.green)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else {
            changeColor(gameObject);
        }
    }

    public void changeColor(GameObject gObject)
    {
        StartCoroutine(colorChangeRedYellowCoroutine(gObject));
    }

    IEnumerator colorChangeRedYellowCoroutine(GameObject gameObject)
    {
        //Print the time of when the function is first called.
        // Debug.Log("Started Coroutine at timestamp : " + Time.time);

        gameObject.GetComponent<Renderer>().material.color = Color.red;
        //yield on a new YieldInstruction that waits for 1 seconds.
        yield return new WaitForSeconds(1.0f);

        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        // Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

}
