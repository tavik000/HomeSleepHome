using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomCall : MonoBehaviour
{
    public AudioSource callingSound;
    public AudioSource closingSound;

    bool closingSoundEnable = false;
    bool callingSoundEnable = false;

    public float callLoopInterval;

    private float resetCallLoopInterval = 27;

    public Transform quilt;
    public GameObject momImage;

    private bool momCallEnable = false;
    private bool gameOver = true;

    // For calling the function of ValueControl
    ValueControl valueControl;

    // Start is called before the first frame update
    public void GameStart()
    {
        gameOver = false;
        callLoopInterval = resetCallLoopInterval;        
        valueControl = quilt.GetComponent<ValueControl>();
    }

    public void GameOver()
    {
        callingSound.Stop();
        closingSound.Stop();
        //MomStopCalling();
        ResetCalling();
        gameOver = true;
    }

    // Update is called once per frame
    void Update()
    {

        callLoopInterval -= Time.deltaTime;
        if (!gameOver)
        {
            if (callLoopInterval <= 9 && !closingSoundEnable)
            {
                closingSound.Play();
                closingSoundEnable = true;
            }
            else if (callLoopInterval <= 4 && callLoopInterval > 3)
            {
                momImage.GetComponent<MoveFromLeftToRight>().StartMoving();

                closingSound.Stop();
            }
            else if (callLoopInterval <= 3 && callLoopInterval > 0 && !callingSoundEnable)
            {
                callingSound.Play();
                MomCalling();
                callingSoundEnable = true;
            }
            else if (callLoopInterval <= 0)//sound source problem
            {
                MomStopCalling();
                ResetCalling();
            }
        }
    }



    // Mon call
    public void MomCalling()
    {
        valueControl.MomCallDeduction();
    }

    public void MomStopCalling()
    {
        valueControl.MomCallDeductionEnd();
    }
    public void ResetClosing()
    {
        momCallEnable = false;
    }

    public void ResetCalling()
    {
        callLoopInterval = resetCallLoopInterval;
        closingSoundEnable = false;
        callingSoundEnable = false;
    }
}
