using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotationScript : MonoBehaviour
{
    public float speed = 5;
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up*Time.deltaTime*speed);
    }
}
