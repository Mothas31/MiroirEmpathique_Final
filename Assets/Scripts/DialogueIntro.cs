using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using UnityEditor.VersionControl;
using System.IO;
//using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class DialogueIntro : MonoBehaviour
{
    
    
   
    public int etape;
    public Texture Image;
    public string EmotionTest;
    public int newItem;
    public CapturePhotoIntro Photo;
    public Text npcText;
    public bool action;
    public RawImage Mask;
    public int etatMask;
    Vector2 position = new Vector2(500, 360);
    Vector2 positionIni = new Vector2(500, 360);
    public float coolDown = 3;
    public float coolDownTimer;
    public bool Next = false;
    public int Timeractive = 1;
    public bool ordinateur;

  

    void Start()
    {


        coolDownTimer = coolDown;
        positionIni = Mask.transform.position;
        position = Mask.transform.position;

        gameObject.AddComponent<VIDE_Data>();

      
        VIDE_Data.LoadDialogues(); //Load all dialogues to memory so that we dont spend time doing so later
        VIDE_Data.BeginDialogue(GetComponent<VIDE_Assign>());
        Photo = gameObject.GetComponentInChildren<CapturePhotoIntro>(true);
        npcText = gameObject.GetComponentInChildren<Text>(true);
        action = true;
        etape = 0;
        
        npcText.text = "Bonjour";
        etatMask = 0;

    }
   

    void Update()
    {
        if (position.x < 1000 && etatMask == 1)
        {
            //   Mask.transform.position = new Vector2(100, 0);

            position.x = position.x + 2;
            this.transform.position = position;
          //  Debug.Log("Postion en X après changement" + position.x);
          //  Debug.Log("Postion en X  et Y après changement" + position);
            Mask.transform.position = position;
        }
        else
        {
            etatMask = 0;
            Debug.Log("Etat mask" + etatMask);
        }
       
     //   Debug.Log("Postion en X Final" + position.x);
      //  Debug.Log("Postion en X et Y Final " + position);





        if (VIDE_Data.isLoaded) //Only if   
            {
            
                var data = VIDE_Data.nodeData;
            if (data.currentIsPlayer) // If it's a player node, let's show all of the available options as buttons
            {

               Debug.Log("C'est le joueur");
                ordinateur = false;

                if (Timeractive == 0)
                {
                    Photo.TakePhoto();
                    Debug.Log("Photo prise");
                    Timeractive = 3;
                }
            }
            else
            {
              
             Debug.Log("C'est le comput");
                ordinateur = true;
                npcText.text = data.npcComment[data.npcCommentIndex];
                if (Timeractive == 0)
                {

                    
                    Timeractive = 1;
                }

                if (Timeractive == 3)
                {

                    coolDownTimer = coolDown;
                    Timeractive = 1;
                }

            }
            if (data.isEnd) // If it's the end, let's just call EndDialogue
            {
                VIDE_Data.EndDialogue();
                 Debug.Log("Chargement de la scène suivante");
            SceneManager.LoadScene("MiroirEmpathique", LoadSceneMode.Single);
               

            }

            if (Input.GetKey("escape"))
            {
                Application.Quit();

            }

    }


            // CoolDown party


        if (coolDownTimer > 0 && Timeractive == 1)              // Lorsque Le timer est supérieur à 0 et actif
        {
            coolDownTimer -= Time.deltaTime;
          //  Debug.Log("CoolDown" + coolDownTimer);
        }

        if (coolDownTimer < 0 && Timeractive == 1 && ordinateur == true)
        {
            
            coolDownTimer = coolDown;
            VIDE_Data.Next();
            Debug.Log("Timer finis ordi,Next joueur");
           
        }

        if (coolDownTimer < 0 && Timeractive == 1 && ordinateur == false)
        {
           
            Debug.Log("Timer finis du joueur");
            Timeractive = 0;

        }
        

        if (coolDownTimer > 1.5 && coolDownTimer < 1.9 && Timeractive == 1)
        {
            etatMask = 1;
            Debug.Log("Lancer le mask" + etatMask);
        }
        if (coolDownTimer > 2.5 && Timeractive == 1)
        {
            position = positionIni;
            Debug.Log("Reposition du mask" + position);
            
            
        }
        // if (GetComponent<VIDE_Assign>().overrideStartNode == 40)
        if (GetComponent<VIDE_Assign>().overrideStartNode == 9)
        {
            Debug.Log("Chargement de la scène suivante");
            SceneManager.LoadScene("MiroirEmpathique", LoadSceneMode.Single);
        }

       
    }
        






    // Après ce n'est plus la fonction Update


    public void ChoixEmotion2()
    {
        Debug.Log("Recup emo :");
        position = positionIni;
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
          
            Debug.Log("Highest Emotion:put2 " + put2);
            ChoixEmotion(put2);
         

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
                 //   npcText.text = data.npcComment[data.npcCommentIndex];

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
                  //  npcText.text = data.npcComment[data.npcCommentIndex];

                }
                if (put2 == "sadness ")
                {
                    data.selectedOption = 3;
                    Debug.Log("Emotion =" + put2);
                    VIDE_Data.Next();
                  //  npcText.text = data.npcComment[data.npcCommentIndex];

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

