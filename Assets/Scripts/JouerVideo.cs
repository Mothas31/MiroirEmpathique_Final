using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
//using UnityEditor.VersionControl;

[RequireComponent (typeof(AudioSource))]
public class JouerVideo : MonoBehaviour {

    public MovieTexture movie;
    public MovieTexture movie2;
    private AudioSource audio;
    public MovieTexture Texturedynamique = new MovieTexture();
    private string Chemin = "./Assets/Ressources/Video";
    public bool action;
    public string Emotion;
    public int newItem;
    public Dialogue dialogue;
   // public int testetape;
    public Text gui;
    


    void Start () {
        if (!System.IO.Directory.Exists(Chemin))
            System.IO.Directory.CreateDirectory(Chemin);
        if (Application.isEditor)
            Chemin = "Assets/Ressources/Video/";

        dialogue = gameObject.GetComponentInParent<Dialogue>();
       

        Debug.Log("dialogue = " + dialogue);
        GetComponent<RawImage>().texture = movie as MovieTexture;
        
        audio = GetComponent<AudioSource>();
        audio.clip = movie.audioClip;
        movie.Play();
        audio.Play();
        
    
        movie.loop = true;
        gui.text = movie.duration.ToString();

    }
	

	void Update () {



       

    }
    string NomVarriableEmotion;
    int NuméroVarEtat;
    string NewChemin;
    
    public void ChoixVideo()
        {


       
        NomVarriableEmotion = dialogue.EmotionTest;

        NuméroVarEtat = dialogue.newItem;
        NewChemin = "Video/";
        Debug.Log(NomVarriableEmotion);



        
        if (File.Exists(Chemin + "video_1.mp4"))
        {
            Debug.Log("Exists" + Chemin);
            Debug.Log("Exists" + NomVarriableEmotion);
            Debug.Log("Exists" + NuméroVarEtat);
            System.IO.Directory.CreateDirectory(NewChemin);
            Debug.Log("new chemin" + NewChemin);
            //  Texturedynamique = (MovieTexture)Resources.Load("Video/" + NomVarriableEmotion + "_" + NomVarriableEtat);
            //Texturedynamique = (MovieTexture)Resources.Load("video_2");
            //Texturedynamique = (MovieTexture)Resources.Load("video_2.mp4") as MovieTexture;
            NomVarriableEmotion = "video";
            NuméroVarEtat = 2;
            Texturedynamique = (MovieTexture)Resources.Load(NewChemin + NomVarriableEmotion + NuméroVarEtat);
            GetComponent<RawImage>().texture = Texturedynamique;
           
            Debug.Log("Il charge quelquechose");
            
            //Texturedynamique.Play();
        }
        else
        {
            Debug.Log("Ce chemin n'existe pas");
            GetComponent<RawImage>().texture = Texturedynamique as MovieTexture;
            Texturedynamique.Play();
            Texturedynamique.loop = true;
        }
        //Texturedynamique = (MovieTexture)Resources.Load("Vidéo/" + NomVarriableEmotion + "_" + NomVarriableEtat);
        
        //NomVideo.loadVideoIntoMovieTexture(Texturedynamique);
        //Texturedynamique = movie2;
       
        //Texturedynamique.Play();

        // string NomVideo;

        //NomVideo = Asset.name;



        // Texturedynamique = NomVideo.movie;


        // movie = NomVideo as MovieTexture;
        //movie = GameObjectby


    }






}
