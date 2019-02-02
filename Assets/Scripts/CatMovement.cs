using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    private Vector2 originalPoint;
    private Rigidbody2D rb;
    public float upSpeed;
    public float downSpeed;
    public AudioSource catBackgroundSound;
    public AudioSource catSound;

    public GameObject quiltMovement;

    public bool gameOver = false;

    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        originalPoint = rb.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        gameOver = false;
        coroutine = Countdown();
        StartCoroutine(coroutine);
    }

    public void GameOver()
    {
        gameOver = true;
        rb.velocity = Vector2.zero;
        StopCoroutine(coroutine);
    }

    IEnumerator Countdown()
    {
        float count = 60f;
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(1);
        }
        if (!gameOver)
        {
            CatStartMove();
        }
    }

    public void CatStartMove()
    {
        //momCalling = true;
        //momTimer = 6f;

        rb.position = originalPoint;
        rb.velocity = new Vector2(0f, upSpeed);

        //play cat sneaky sound
        catBackgroundSound.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "CatLine")
        {
            rb.velocity = new Vector2(0f, downSpeed);//cat go down
            quiltMovement.GetComponent<QuiltMovement>().CatSpeedUp();
            StartCoroutine("CountdownSix");
            //cat sound 
            catSound.Play();
        }
    }

    IEnumerator CountdownSix()
    {
        float count = 6f;
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(1);
        }
        quiltMovement.GetComponent<QuiltMovement>().CatSpeedUpStop();
    }
   

}
