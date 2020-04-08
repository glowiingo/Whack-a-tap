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

    float startTime;
    float timer; 
    float waitTime = 1.0f;

    bool called = false;
    // Start is called before the first frame update
    void Start() {
        setCubes();
        // startTime = Time.time;
        if (!called) {
            InvokeRepeating("pickRandomCubeAndChangeY", 1.0f, 1.0f);
        }
    }

    // Update is called once per frame
    void Update() {
        //timer += Time.deltaTime;
        //if (timer > 1.0f && timer < 2.0f) {
        //    if (!called) {
        //        pickRandomCubeAndChangeY();
        //        called = true;
        //    }
        //}

        //if (timer > 2.0f && timer < 3.0f) {
        //    if (!calledSecond) {
        //        pickRandomCubeAndChangeY();
        //        calledSecond = true;
        //    }
        //}
    }

    void setCubes() {
        cubeLeft = GameObject.FindGameObjectWithTag("CubeLeft");
        cubeMid = GameObject.FindGameObjectWithTag("CubeMid");
        cubeRight = GameObject.FindGameObjectWithTag("CubeRight");

        Debug.Log("Left: " + cubeLeft.ToString());
        Debug.Log("Mid: " + cubeMid.ToString());
        Debug.Log("Right: " + cubeRight.ToString());
    }

    void pickRandomCubeAndChangeY() {
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
        changeY(randomCube);
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

    // Function to change the alpha
    public void changeY(GameObject gObject)
    {
        float time = 0.4f;
        Debug.Log("Test on click.");
        Debug.Log(gObject.name.ToString());
        Debug.Log(gObject.GetComponent<Renderer>().material.ToString());

        gObject.LeanMoveLocalY(0.3f, time);
        gObject.LeanMoveLocalY(0f, time).setDelay(time + 0.1f);
    }

}
