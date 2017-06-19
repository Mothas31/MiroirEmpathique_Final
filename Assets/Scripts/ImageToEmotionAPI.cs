using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;

using UnityEngine.UI;


public class ImageToEmotionAPI : MonoBehaviour {

    string EMOTIONKEY = "0bf24095d479418aba9f33a0f3106824"; // replace with your Emotion API Key

    string emotionURL = "https://api.projectoxford.ai/emotion/v1.0/recognize";

    public string fileName { get; private set; }
    string responseData;

    // Use this for initialization
    void Start () {
	    fileName = Path.Combine(Application.streamingAssetsPath, "surveillanceCapture02.png"); // Replace with your file
    }
	
	// Update is called once per frame
	void Update () {
	
        // This will be called with your specific input mechanism
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(GetEmotionFromImages());
        }

	}
    /// <summary>
    /// Get emotion data from the Cognitive Services Emotion API
    /// Stores the response into the responseData string
    /// </summary>
    /// <returns> IEnumerator - needs to be called in a Coroutine </returns>
    IEnumerator GetEmotionFromImages()
    {
        byte[] bytes = System.IO.File.ReadAllBytes(fileName);

        var headers = new Dictionary<string, string>() {
            { "Ocp-Apim-Subscription-Key", EMOTIONKEY },
            { "Content-Type", "application/octet-stream" }
        };

        WWW www = new WWW(emotionURL, bytes, headers);

        yield return www;
        responseData = www.text; // Save the response as JSON string
        Debug.Log(responseData);
        GetComponent<ParseEmotionResponse>().ParseJSONData(responseData);
    }
}
