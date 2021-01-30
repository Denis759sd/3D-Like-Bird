using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow_position : MonoBehaviour
{
    public int speedSnow = 55;
    
    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + speedSnow * Time.deltaTime, this.transform.position.y);
    }
}
