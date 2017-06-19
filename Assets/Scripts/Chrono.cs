using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chrono : MonoBehaviour
{




    public float chrono = 0;
    public float tempsChrono;

    // Update is called once per frame
    void Update()
    {

       
            tempsChrono += Time.deltaTime;

      

        if (Input.GetButton("Fire1") && tempsChrono > 0)
        {
            Attack();
            tempsChrono = chrono;
        }
    }
    void Attack()
    {
        Debug.Log("Hi");
    }
}
