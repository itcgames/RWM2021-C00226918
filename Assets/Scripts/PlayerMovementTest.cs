using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    private float moveSpeed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().position = new Vector2(GetComponent<Rigidbody2D>().position.x + moveSpeed, GetComponent<Rigidbody2D>().position.y);
        }

        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().position = new Vector2(GetComponent<Rigidbody2D>().position .x - moveSpeed, GetComponent<Rigidbody2D>().position.y);
        }

        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody2D>().position = new Vector2(GetComponent<Rigidbody2D>().position.x, GetComponent<Rigidbody2D>().position.y + moveSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody2D>().position = new Vector2(GetComponent<Rigidbody2D>().position.x, GetComponent<Rigidbody2D>().position.y - moveSpeed);
        }
    }
}
