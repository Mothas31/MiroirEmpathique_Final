using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDown : MonoBehaviour {




    public float coolDown = 1;
    public float coolDownTimer;
    public bool Next = false;

	// Update is called once per frame
	void Update () {

        if(coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
            Debug.Log("CoolDown" + coolDownTimer);
        }
		
        if(coolDownTimer < 0)
        {

            coolDownTimer = 0;
            Next = false;
        }

        if (coolDownTimer > 0 && coolDownTimer < 0.2)
        {
            Next = true;
        }
        /* if (Input.GetButton("Fire1") && coolDownTimer == 0)
         {
             Attack();
             coolDownTimer = coolDown;
         }

     */
    }
             
        public    void CoolDownActive()
            {
                    if (coolDownTimer == 0 && Next == false)
                    {
                         Debug.Log("CoolDown start");

                        coolDownTimer = coolDown;
                        Debug.Log("CoolDown"+ coolDownTimer);

            }
                    
    }
}
