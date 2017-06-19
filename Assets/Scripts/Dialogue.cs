using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using UnityEditor.VersionControl;
using System.IO;

public class Dialogue : MonoBehaviour
{
    public static int yolo = 3;
    public Timer timer;
    public float cooldown = 1;
    public float cooldowntimer;
    public int etape;
    public MovieTexture movie;
    public MovieTexture movie2;
    public JouerVideo Video;
    public CapturePhoto Photo;
    public Texture Image;
    public string EmotionTest;
    public int newItem;
    public bool action;
    public CoolDown CoolDown;

    void Start()
    {
        timer = gameObject.GetComponentInChildren<Timer>(true);
        
        gameObject.AddComponent<VIDE_Data>();
        Video = gameObject.GetComponentInChildren<JouerVideo>(true);
        Photo = gameObject.GetComponentInChildren<CapturePhoto>(true);
        CoolDown = gameObject.GetComponentInChildren<CoolDown>(true);
        action = true;
       

    }

void OnGUI()
    {

        GUILayout.BeginArea(new Rect(1000, 100, 200, 200));

        if (VIDE_Data.isLoaded)
        {
            var data = VIDE_Data.nodeData; //Quick reference
            if (data.currentIsPlayer) // If it's a player node, let's show all of the available options as buttons
            {
                for (int i = 0; i < data.playerComments.Length; i++)
                {
                   

                 /* string debug = etape.ToString();
                    timer.timerText.text = debug;

                    if (etape == 1)
                        {
                            timer.timerText.color = Color.yellow;
                        }
                    else 
                        {
                            timer.timerText.color = Color.black;
                        }
    */            
    // Choisis dans la liste des émotions celle qui sera choisis, ici ça se traduit avec un bouton du nom de l'émotion
                    if (GUILayout.Button(data.playerComments[i])) //When pressed, set the selected option and call Next()
                    {
                        data.selectedOption = i;        // selectionne la valeur du bouton et l'intégre dans la variable data
                        Debug.Log("i =" + i);
                        Photo.TakePhoto(); //Prend une photo


                          // Lance la fonction choixVideo du script Jouervideo.

                        VIDE_Data.Next();          // passe au texte suivant
                        
                        etape++;
                        action = true;
                      //  CoolDown.CoolDownActive(); 

                    }

                   
               }

               // Choix du dialogue en appuyant sur une touche
                        if (Input.GetKeyDown(KeyCode.Keypad1))
                            {
                                
                               
                                Debug.Log("data =" + data.selectedOption);
                                  Photo.TakePhoto();
                }
                        if (Input.GetKeyDown(KeyCode.Keypad2))
                            {
                                
                                data.selectedOption = 2;     //Choix 2
                    Debug.Log("data =" + data.selectedOption);
                                VIDE_Data.Next();
                            }
            }
            else  //if it's a NPC node, Let's show the comment and add a button to continue
            {
                
            
             if(action == true) // on ne teste qu'une fois l'émotion
                { 

                Debug.Log("Valeur action" + action);
                Debug.Log("ExtraVar" + data.extraVars.ContainsKey("Peur"));
                                //Emotion Peur
                            if (data.extraVars.ContainsKey("Peur")) //Si l'emotion choisi est la peur.
                            {
                                EmotionTest = "Peur";
                                Dictionary<string, object> newVars = data.extraVars; //Clone the current extraVars content
                                newItem = (int)newVars["Peur"]; //Retrieve the value we want to change
                                Debug.Log("Valeur NewItem avant incrément" + newItem);
                    // newItem += 2; //Change it as we desire
                    //  Debug.Log("Valeur NewItem" + newItem);
                    //  newVars["Peur"] = newItem; //Set it back   
                    //  VIDE_Data.UpdateExtraVariables(2, newVars); //Send newVars through UpdateExtraVariable method
                    
                }

                                     //Emotion Colere        
                            if (data.extraVars.ContainsKey("Colere") && action == true) //Si l'emotion choisi est la Colere.
                            {
                                EmotionTest = "Colere";
                                Dictionary<string, object> newVars = data.extraVars; 
                                newItem = (int)newVars["Colere"];
                                Video.ChoixVideo();
                                action = false;
                            }
                            //Emotion Joie       
                            if (data.extraVars.ContainsKey("Joie")) 
                            {
                                EmotionTest = "Joie";
                                Dictionary<string, object> newVars = data.extraVars;
                                newItem = (int)newVars["Joie"];
                                action = false;
                            }
                            //Emotion Tristesse
                            if (data.extraVars.ContainsKey("Tristesse"))
                            {
                                EmotionTest = "Tristesse";
                                Dictionary<string, object> newVars = data.extraVars;
                                newItem = (int)newVars["Tristesse"];
                                action = false;
                            }
                            //Emotion Tristesse
                            if (data.extraVars.ContainsKey("Tristesse"))
                            {
                                EmotionTest = "Tristesse";
                                Dictionary<string, object> newVars = data.extraVars;
                                newItem = (int)newVars["Tristesse"];
                                action = false;
                             }
                            //Emotion Dégout
                            if (data.extraVars.ContainsKey("Degout"))
                            {
                                EmotionTest = "Degout";
                                Dictionary<string, object> newVars = data.extraVars;
                                newItem = (int)newVars["Degout"];
                                 action = false;
                             }
                            //Emotion Surprise
                            if (data.extraVars.ContainsKey("Surprise"))
                            {
                                EmotionTest = "Surprise";
                                Dictionary<string, object> newVars = data.extraVars;
                                newItem = (int)newVars["Surprise"];
                                action = false;
                            }
                            //Emotion Neutre
                            if (data.extraVars.ContainsKey("Neutre"))
                            {
                                EmotionTest = "Neutre";
                                Dictionary<string, object> newVars = data.extraVars;
                                newItem = (int)newVars["Neutre"];
                                action = false;
                            }
                }

             // on ajout un bouton pour pouvoir passer à la suite

                GUILayout.Label(data.npcComment[data.npcCommentIndex]);
                Debug.Log("Valeur" + data.npcComment[data.npcCommentIndex]);
                if (GUILayout.Button(">"))
                {
                    
                    VIDE_Data.Next();
                }
                if (CoolDown.Next == true)
                {

                    VIDE_Data.Next();
                    CoolDown.Next = false;
                }


                // On ajoute un timer pour lancer la suite



            }
            if (data.isEnd) // If it's the end, let's just call EndDialogue
            {
                VIDE_Data.EndDialogue();
            }
        }
        else // Add a button to begin conversation if it isn't started yet
        {
            if (GUILayout.Button("Start Convo"))
            {
                VIDE_Data.BeginDialogue(GetComponent<VIDE_Assign>()); //We've attached a DialogueAssign to this same gameobject, so we just call the component
            }





        }
        GUILayout.EndArea();
    }
    
   

    
    void Update()
    {

        // Debug.Log("Emotion Dans dialogue =" + recupEmotion.put2);
        /*
        if (VIDE_Data.isLoaded)
        {
            var data = VIDE_Data.nodeData; //Quick reference
            if (data.currentIsPlayer) // If it's a player node, let's show all of the available options as buttons
            {


                if (recupEmotion.put2 == "anger")
                {
                    data.selectedOption = 2;
                    Debug.Log("Emotion Dans dialogue =" + recupEmotion.name);
                    VIDE_Data.Next();
                    etape++;
                }
                if (recupEmotion.put2 == "neutral")
                {
                    data.selectedOption = 4;
                    Debug.Log("Emotion Dans dialogue =" + recupEmotion.name);
                    VIDE_Data.Next();
                    etape++;
                }
            }

        }  */
    }
    
    public void ChoixEmotion2()
    {
        Debug.Log("Recup emo :");
    }

    public class FaceObject 
    {
        
        public string faceRectangle { get; private set; }
        public List<Emotion> emotions { get; private set; }
        public string put2;

       

        public FaceObject(string rect, string scorelist)
        {
            faceRectangle = rect;
            emotions = ConvertScoresToEmotionDictionary(scorelist);
            string put = GetHighestWeighedEmotion().ToString();
            string put2 = "";
            int i = 0;
            char temp = put[i];

            while (temp != ':')
            {
                put2 += temp;
                i++;
                temp = put[i];
            }
            // dialogue.ChoixEmotion(put2);
            //dialogue.ChoixEmotion();
            // Debug.Log("Highest Emotion: " + GetHighestWeighedEmotion().ToString());
            Debug.Log("Highest Emotion:put2 " + put2);
            // gameObject.SendMessage("ChoixEmotion", put2);
            ChoixEmotion(put2);
            //envoichize(put2);
        }
        /// <summary>
        /// Convert a JSON-formatted string from the Emotion API call into a List of Emotions
        /// </summary>
        /// <param name="scores"></param>
        /// <returns></returns>
        public List<Emotion> ConvertScoresToEmotionDictionary(string scores)
        {
            List<Emotion> emotes = new List<Emotion>();
            JSONObject _scoresJSON = new JSONObject(scores);
            for (int i = 0; i < _scoresJSON.Count; i++)
            {

                Emotion e = new Emotion(_scoresJSON.keys[i], float.Parse(_scoresJSON.list[i].ToString()));
                emotes.Add(e);
                Debug.Log("I : " + i);
            }
            return emotes;
        }

        /// <summary>
        /// Get the highest scored emotion 
        /// </summary>
        /// <returns></returns>
        public Emotion GetHighestWeighedEmotion()
        {
            Emotion max = emotions[0];
            Debug.Log("Emotion 0 : " + emotions[0]);
            Debug.Log("Emotion 1 : " + emotions[1]);
            Debug.Log("Emotion 2 : " + emotions[2]);
            Debug.Log("Emotion 3 : " + emotions[3]);
            Debug.Log("Emotion 4 : " + emotions[4]);
            Debug.Log("Emotion 5 : " + emotions[5]);
            Debug.Log("Emotion 6 : " + emotions[6]);
            Debug.Log("Emotion 7 : " + emotions[7]);
            foreach (Emotion e in emotions)
            {
                if (e.value > max.value)
                {
                    max = e;
                }
            }
            Debug.Log("max : " + max.name);


            // put2 = max.name;

            return max;

        }
        public void ChoixEmotion(string put2)
        {
            Debug.Log("Emotion à envoyer =" + put2);

            if (VIDE_Data.isLoaded)
            {
                Debug.Log("Vide data is loaded");
                var data = VIDE_Data.nodeData; //Quick reference
             //   if (data.currentIsPlayer) // If it's a player node, let's show all of the available options as buttons
             //   {
                    Debug.Log("Current is player");

                    if (put2 == "anger ")
                    {
                        data.selectedOption = 2;
                        Debug.Log("Emotion change next =" + put2);
                        VIDE_Data.Next();
                        
                    }
                    if (put2 == "contempt ")
                    {
                        data.selectedOption = 0;
                        Debug.Log("Emotion change next =" + put2);
                        VIDE_Data.Next();
                        
                    }
                    if (put2 == "disgust ")
                    {
                        data.selectedOption = 5;
                        Debug.Log("Emotion change next =" + put2);
                        VIDE_Data.Next();
                        
                    }
                    if (put2 == "fear ")
                    {
                        data.selectedOption = 1;
                        Debug.Log("Emotion =" + put2);
                        VIDE_Data.Next();
                       
                    }
                    if (put2 == "happiness ")
                    {
                        data.selectedOption = 0;
                        Debug.Log("Emotion =" + put2);
                        VIDE_Data.Next();
                        
                    }
                    if (put2 == "neutral ")
                    {
                        data.selectedOption = 4;
                        Debug.Log("Emotion change next =" + put2);
                        VIDE_Data.Next();
                        
                    }
                    if (put2 == "sadness ")
                    {
                        data.selectedOption = 3;
                        Debug.Log("Emotion =" + put2);
                        VIDE_Data.Next();
                       
                    }
                    if (put2 == "surprise ")
                    {
                        data.selectedOption = 6;
                        Debug.Log("Emotion =" + put2);
                        VIDE_Data.Next();
                        
                    }
                  /*  else
                    {
                        data.selectedOption = 4;
                        Debug.Log("Emotion Else =" + put2+"test");
                        VIDE_Data.Next();
                        
                    }
*/

              //  }

            }

        }

        public void envoichize(string put2)
        {
            Debug.Log("Test envoi chize " + put2);
            
            // gameObject.GetComponentInParent<Dialogue>().SendMessage("ChoixEmotion", put2);
        }

    }

    /// <summary>
    ///  A helper class for storing an emotion
    ///  From the spec of the Cognitive Services API
    /// </summary>
    public class Emotion
    {
        public string name { get; private set; }
        public float value { get; private set; }

        public Emotion(string name, float value)
        {
            this.name = name;
            this.value = value;
        }

        override public string ToString()
        {
            return name + " : " + value;
        }



    }
 






}

