using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private float movementSpeed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        if(!(transform.position.x < -0.5f))
        {
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementSpeed;
            if(transform.position.x < -0.5f)
            {
                transform.position = new Vector3(-0.5f, transform.position.y, transform.position.z);
            }
        }
    }
}
