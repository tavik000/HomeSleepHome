using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Boundary
{
    public float minHeight, maxHeight;
}

public class QuiltMovement : MonoBehaviour
{
    Rigidbody2D rigidbody;

    public float upSpeed;
    public float downSpeed;
    public Boundary boundary;

    public Vector2 quiltStartPos;

    public bool qmGameOver = true;

    //button sound
    public AudioSource buttonSound;

    // Start is called before the first frame update
    public void GameStart()
    {
        qmGameOver = false;
        rigidbody.simulated = true;
        rigidbody.velocity = Vector3.zero;
        rigidbody.gravityScale = downSpeed;
        rigidbody.position = quiltStartPos;
    }

    //gameover
    public void GameOver()
    {
        qmGameOver = true;
        rigidbody.simulated = false;        
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.simulated = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (!qmGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //play sound effect everytime player press the button
                buttonSound.Play();
                rigidbody.velocity = Vector3.zero;
                rigidbody.AddForce(Vector2.up * upSpeed);
            }

            this.transform.position = new Vector2
               (
                   transform.position.x,
                   Mathf.Clamp(transform.position.y, boundary.minHeight, boundary.maxHeight)
               );
        }
    }
    public void CatSpeedUp()
    {
        downSpeed += 45;
        rigidbody.gravityScale = downSpeed;
    }

    public void CatSpeedUpStop()
    {
        downSpeed -= 45;
        rigidbody.gravityScale = downSpeed;
    }
}
