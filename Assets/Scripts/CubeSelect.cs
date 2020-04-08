using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSelect : MonoBehaviour
{
    public GameObject cubeLeft = null;
    public GameObject cubeMid = null;
    public GameObject cubeRight = null;
    int cubeNumber = 0;
    int oldNumber = 0;

    //float startTime;
    //float timer; 
    //float waitTime = 1.0f;

    bool called = false;
    // Start is called before the first frame update
    void Start() {
        setCubes();
        // startTime = Time.time;
        /**
         * Testing purposes
        if (!called) {
            InvokeRepeating("pickRandomCube", 1.0f, 1.0f);
        }
        */
        if (!called)
        {
            InvokeRepeating("pickRandomCube", 1.0f, 1.0f);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    void setCubes() {
        cubeLeft = GameObject.FindGameObjectWithTag("CubeLeft");
        cubeMid = GameObject.FindGameObjectWithTag("CubeMid");
        cubeRight = GameObject.FindGameObjectWithTag("CubeRight");

        Debug.Log("Left: " + cubeLeft.ToString());
        Debug.Log("Mid: " + cubeMid.ToString());
        Debug.Log("Right: " + cubeRight.ToString());

        // set the color to a default color
        // this cannot be set by the theme because we are unable
        // to set the 
        cubeLeft.GetComponent<Renderer>().material.color = Color.yellow;
        cubeMid.GetComponent<Renderer>().material.color = Color.yellow;
        cubeRight.GetComponent<Renderer>().material.color = Color.yellow;
    }

    void pickRandomCube() {
        //Debug.Log("Old: " + oldNumber.ToString());
        //Debug.Log("New: " + cubeNumber.ToString());
        // pick a random cube number
        cubeNumber = Random.Range(1, 4);
        // check if old number is the same as cube number
        // if it is, pick again
        while (oldNumber == cubeNumber) {
            cubeNumber = Random.Range(1, 4);
        }
        // Debug.Log("New: " + cubeNumber.ToString());
        // if it isn't set new old number to cube number
        oldNumber = cubeNumber;
        Debug.Log("New: " + cubeNumber.ToString());
        GameObject randomCube = randomCubeSelector(cubeNumber);
        // changeY(randomCube);
        changeColor(randomCube);
    }

    /***
     * Given a random number between 1 and 3 return the correlating cube.
     * param: cubeNumber, a random number between 1 and 3.
     */
    GameObject randomCubeSelector(int cubeNumber) {
        GameObject selectedCube = null;
        switch (cubeNumber) {
            case 1:
                selectedCube = cubeLeft;
                break;
            case 2:
                selectedCube = cubeMid;
                break;
            case 3:
                selectedCube = cubeRight;
                break;
        }
        return selectedCube;
    
    }

    public void changeColor(GameObject gObject) {
        StartCoroutine(colorChangeCoroutine(gObject));
    }

    IEnumerator colorChangeCoroutine(GameObject gameObject)
    {
        //Print the time of when the function is first called.
        // Debug.Log("Started Coroutine at timestamp : " + Time.time);

        gameObject.GetComponent<Renderer>().material.color = Color.green;
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.75f);

        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        //After we have waited 5 seconds print the time again.
        // Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
