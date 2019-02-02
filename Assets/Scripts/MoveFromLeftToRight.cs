using UnityEngine;
using UnityEngine.UI;

public class MoveFromLeftToRight : MonoBehaviour
{
    public float momSpeed;

    public Image effect1;
    public Image effect2;

    private bool momCalling = false;


    private Vector2 originalPoint;
    private Rigidbody2D rb;

    private float timerEffect = 0.25f;
    private float momTimer = 3f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        originalPoint = rb.position;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "StopLine")
    //    {
    //        //rb.gameObject.active = false;
    //    }
    //}




    void Update()
    {
        if(momCalling)
        {
            //rb.velocity = new Vector2(momSpeed, 0.0f);
            timerEffect -= Time.deltaTime;
            momTimer -= Time.deltaTime;
            
            if (momTimer <= 0)
            {
                momCalling = false;
            }
            if (timerEffect <= 0)
            {
                if (effect1.gameObject.active)
                {
                    effect1.gameObject.active = false;
                    effect2.gameObject.active = true;
                }
                else
                {
                    effect1.gameObject.active = true;
                    effect2.gameObject.active = false;
                }
                timerEffect = 0.25f;
            }
        }
        else
        {
            effect1.gameObject.active = false;
            effect2.gameObject.active = false;
        }
        
    

    }

    public void StartMoving()
    {
        momCalling = true;
        momTimer = 3f;

        rb.position = originalPoint;
        rb.velocity = new Vector2(momSpeed, 0f);

        effect1.gameObject.active = false;

    }

}
