using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schwengus : MonoBehaviour
{
    // Start is called before the first frame update

    int rnd = 2;
    void Start()
    {
        

    }
    

    bool schalter = false;
    float y = 0;
    float x = 0;
    // Update is called once per frame
    void Update()
    {
        if (rnd % 2 == 0)
            {
                transform.Rotate(new Vector3(0, y*100*Time.deltaTime, 0));
            if (!schalter){
                y = 0.5f;
            } 
            else{
                 y = -0.5f;
            }
            if (transform.rotation.y > 0.5) {schalter = true;};
            if (transform.rotation.y < -0.5) {schalter = false;};
            }
        else
            {
            transform.Rotate(new Vector3(x*100*Time.deltaTime, 0, 0));
            if (!schalter){
                x = 0.5f;
            } 
            else{
                x = -0.5f;
            }
            if (transform.rotation.x > 0.5)  
                {
                    schalter = true;
                    };
            if (transform.rotation.x < -0.5) {
                schalter = false;
                };
            }
        
    }
}
