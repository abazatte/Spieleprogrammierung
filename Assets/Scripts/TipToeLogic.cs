using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class TipToeLogic : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    private int width = 10;
    private int depth = 13;
    private float gapbetween = 3f;

    // Start is called before the first frame update
    void Start()
    {
        platformPrefab.transform.localScale = new Vector3(1.2f,1,1.2f);
        for (int i = 0; i < depth; i++)
        {
            Random random = new Random();
            //für jede Reihe neu bestimmen 
            for (int j = 0; j < width; j++)
            {
                GameObject gameObject = Instantiate(platformPrefab, new Vector3(-13.52f + (gapbetween * j), 0, 10 + (gapbetween * i)), Quaternion.identity);

                gameObject.AddComponent(typeof (BoxCollider));
                gameObject.AddComponent(typeof(TipToePlatform));
                int rnd = random.Next(1,3);
                print(rnd);
                if (rnd % 2 == 0)
                {
                    gameObject.GetComponent<TipToePlatform>().isPath = true;
                }
                
                //TipToePlatform tipToePlatform = new TipToePlatform();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
