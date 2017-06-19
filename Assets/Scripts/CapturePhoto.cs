using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using System;

public class CapturePhoto : MonoBehaviour
{
    public int testetape;
    public Dialogue dialogue;
    public RawImage rawimage;
    WebCamTexture webCamTexture;
    private string absolutePath = "./Assets/StreamingAssets";
    //string fileName02 = "filename";
    int photoprise;


    // Pour envoie de la photo
    string EMOTIONKEY = "0bf24095d479418aba9f33a0f3106824"; // replace with your Emotion API Key

    string emotionURL = "https://api.projectoxford.ai/emotion/v1.0/recognize";

    public string fileName { get; private set; }
    string responseData;



    void Start()
    {
        fileName = Path.Combine(Application.streamingAssetsPath, "surveillanceCapture02.png");
        photoprise = 0;
        dialogue = gameObject.GetComponentInParent<Dialogue>();
        Debug.Log("dialogue = " + dialogue);
        if (!System.IO.Directory.Exists(absolutePath))
            System.IO.Directory.CreateDirectory(absolutePath);
        if (Application.isEditor)
            absolutePath = "Assets/StreamingAssets/";

        WebCamDevice[] cam_devices = WebCamTexture.devices;
        webCamTexture = new WebCamTexture(cam_devices[0].name, 480, 640, 30);
        rawimage.texture = webCamTexture;
        if (webCamTexture != null)
        {
            webCamTexture.Play();
        }
    }

    void Update()
    {

        // testetape = dialogue.etape;
        // This will be called with your specific input mechanism
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(GetEmotionFromImages());
        }

    }

    public void GoWebCam01()
    {
        //Start streaming the images captured by the webcam into the texture
        webCamTexture.Play();
    }

    public void GoWebCamStop()
    {
        //Start streaming the images captured by the webcam into the texture
        webCamTexture.Stop();
    }


    //NOTE: This method seems to capture a web camera photo but freezes the camera image.
    public void TakePhoto()
    {
        //webCamTexture.Pause();
        BlitImage();

    }
    void BlitImage()
    {
        Debug.Log("BlitImage");
        testetape = dialogue.etape;
        Texture2D destTexture = new Texture2D(webCamTexture.width, webCamTexture.height, TextureFormat.ARGB32, false);

        Color[] textureData = webCamTexture.GetPixels();

        destTexture.SetPixels(textureData);
        destTexture.Apply();
        Debug.Log(absolutePath);
        byte[] pngData = destTexture.EncodeToPNG();
       
        if (File.Exists(absolutePath + "surveillanceCapture0"+testetape+".png"))
        {
            Debug.Log("Exists"+ testetape);
           
            File.Delete(absolutePath + "surveillanceCapture0" + testetape + ".png");
            Debug.Log("Delete" + testetape);
        }
        
        
        File.WriteAllBytes(absolutePath + "surveillanceCapture0" + testetape + ".png", pngData);
        Debug.Log("Write" + testetape);
        fileName = Path.Combine(Application.streamingAssetsPath, "surveillanceCapture0"+testetape + ".png");
        Debug.Log("File Saved to Desktop/Assets/WebcamVideo/");
        photoprise++;
        StartCoroutine(GetEmotionFromImages());

    }

    /// <summary>
    /// Get emotion data from the Cognitive Services Emotion API
    /// Stores the response into the responseData string
    /// </summary>
    /// <returns> IEnumerator - needs to be called in a Coroutine </returns>
    IEnumerator GetEmotionFromImages()
    {
        byte[] bytes = System.IO.File.ReadAllBytes(fileName);
        Debug.Log("ça avance ?");
        var headers = new Dictionary<string, string>() {
            { "Ocp-Apim-Subscription-Key", EMOTIONKEY },
            { "Content-Type", "application/octet-stream" }
        };

        WWW www = new WWW(emotionURL, bytes, headers);
        Debug.Log("Envoyé ?");
        yield return www;
        responseData = www.text; // Save the response as JSON string
        Debug.Log("En attente de réponse");
        Debug.Log(responseData);
        GetComponent<ParseEmotionResponse>().ParseJSONData(responseData);
    }
 
}
