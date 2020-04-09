﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CubeSelect : MonoBehaviour
{
    public AudioSource audioSource;
    public float delay;
    public GameObject cubeLeft = null;
    public GameObject cubeMid = null;
    public GameObject cubeRight = null;
    int cubeNumber = 0;
    int oldNumber = 0;
    private GameObject songCardObj;
    private List<int> rhythmData;
    private string filePath;
    int time;
    int count;
    float lengthMilli;

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
        // songCardObj = GameObject.FindGameObjectWithTag("SongAliveByMindVortex");
        filePath = Directory.GetCurrentDirectory() + "\\Assets\\Scripts\\Music\\Alive - Mind Vortex";
        rhythmData = new List<int>();
        MusicFileHandler.tryRhythmData(filePath, rhythmData);
        time = 0;
        count = 0;

        // string[] separator = { "-" };
        // string[] songInfo = songCardObj.name.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        Song song = new Song(Directory.GetCurrentDirectory(), "Alive", "Mind Vortex");
        audioSource.PlayDelayed(delay);
        lengthMilli = audioSource.clip.length * 1000;
        //song.play(audioSource, delay);

    }

    // Update is called once per frame
    void Update() {
        if (time < lengthMilli && count < rhythmData.Count)
        {
            time = Convert.ToInt32(Math.Floor(audioSource.time * 100 + 0.5) * 10);
            if (time >= rhythmData[count])
            {
                // Debug.Log("boop " + count)
                pickRandomCube();

                count++;
            }
        }
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

    public void pickRandomCube() {
        //Debug.Log("Old: " + oldNumber.ToString());
        //Debug.Log("New: " + cubeNumber.ToString());
        // pick a random cube number
        cubeNumber = UnityEngine.Random.Range(1, 4);
        // check if old number is the same as cube number
        // if it is, pick again
        while (oldNumber == cubeNumber) {
            cubeNumber = UnityEngine.Random.Range(1, 4);
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
        yield return new WaitForSeconds(1.0f);

        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        //After we have waited 5 seconds print the time again.
        // Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}