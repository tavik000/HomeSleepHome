using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ValueControl : MonoBehaviour
{
    public float changeRate;
    public float healRate;
    public float changeSpeed;


    public float comfortValue;

    public Slider comfortBar;
    public Text comforText;

    public Transform[] emojis;

    private bool isCoverEar = false;
    private bool momCallEnable = false;
    private bool momCallDeductionEnable = false;

    public bool gameOver = true;

    public GameObject gameManager;

    public float timePeriod;
    public float maxTimePeriod;
    public Text timerText;

    public Text gameStatus;


    public Image footDamageEffect1;
    public Image footDamageEffect2;
    public Image bodyDamageEffect1;
    public Image bodyDamageEffect2;
    public Image earDamageEffect1;
    public Image earDamageEffect2;

    private bool bodyDamaging = false;
    private bool footDamaging = false;

    private float timerEffect = 0.25f;
    private float earTimerEffect = 0.05f;


    private void Update()
    {
        if (!gameOver)
        {
            if (footDamaging)
            {
                timerEffect -= Time.deltaTime;
                
                if (timerEffect <= 0)
                {
                    if (footDamageEffect1.gameObject.active)
                    {
                        footDamageEffect1.gameObject.active = false;
                        footDamageEffect2.gameObject.active = true;
                    }
                    else
                    {
                        footDamageEffect1.gameObject.active = true;
                        footDamageEffect2.gameObject.active = false;
                    }
                    timerEffect = 0.25f;
                }
            }

            if (bodyDamaging)
            {
                timerEffect -= Time.deltaTime;

                if (timerEffect <= 0)
                {
                    if (bodyDamageEffect1.gameObject.active)
                    {
                        bodyDamageEffect1.gameObject.active = false;
                        bodyDamageEffect2.gameObject.active = true;
                    }
                    else
                    {
                        bodyDamageEffect1.gameObject.active = true;
                        bodyDamageEffect2.gameObject.active = false;
                    }
                    timerEffect = 0.25f;
                }
            }

            if (momCallDeductionEnable)
            {
                earTimerEffect -= Time.deltaTime;

                if (earTimerEffect <= 0)
                {
                    if (earDamageEffect1.gameObject.active)
                    {
                        earDamageEffect1.gameObject.active = false;
                        earDamageEffect2.gameObject.active = true;
                    }
                    else
                    {
                        earDamageEffect1.gameObject.active = true;
                        earDamageEffect2.gameObject.active = false;
                    }
                    earTimerEffect = 0.05f;
                }
            }
        }
    }

    // GameStart
    public void GameStart()
    {

        momCallEnable = false;
        momCallDeductionEnable = false;

        bodyDamaging = false;
        footDamaging = false;
        earDamageEffect1.gameObject.active = false;
        earDamageEffect2.gameObject.active = false;
        bodyDamageEffect1.gameObject.active = false;
        bodyDamageEffect2.gameObject.active = false;
        footDamageEffect1.gameObject.active = false;
        footDamageEffect2.gameObject.active = false;

        isCoverEar = false;
        comfortValue = 100f;
        comfortBar.value = comfortValue;
        comforText.text = comfortValue.ToString();
        changeRate = 0f;
        Debug.Log("start" + "changeRate:"+ changeRate);

        gameOver = false;
        StartCoroutine("ComfortDeduction");

        timePeriod = maxTimePeriod + 1;
        timerText.text = "Timer: " + timePeriod.ToString("N0") + "s";

        StartCoroutine("WinTimer");

    }


    #region checking changeRate
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameOver)
        {
            if (collision.gameObject.tag == "EarZone")
            {
                isCoverEar = true;
                if (momCallDeductionEnable && momCallEnable)
                {
                    changeRate -= 6;
                    print("-6 => now: " + changeRate + " with object " + collision.gameObject.name + " at line 69");
                    momCallDeductionEnable = false;
                    earDamageEffect1.gameObject.active = false;
                    earDamageEffect2.gameObject.active = false;
                }
            }


            if (changeRate > 0 && collision.gameObject.tag == "BodyFootZone")
            {
                changeRate -= 1;
                print("-1 => now: " + changeRate + " with object " + collision.gameObject.name + " at line 78");

                // Zone1 is the body zone
                if (collision.gameObject.name == "Zone1")
                {
                    bodyDamaging = false;
                    bodyDamageEffect1.gameObject.active = false;
                    bodyDamageEffect2.gameObject.active = false;
                }

                // Zone9 is the foot zone
                if (collision.gameObject.name == "Zone9")
                {
                    footDamaging = false;
                    footDamageEffect1.gameObject.active = false;
                    footDamageEffect2.gameObject.active = false;
                }
            }
        }

           
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!gameOver) {
            if (collision.gameObject.tag == "BodyFootZone")
            {

                changeRate += 1;
                print("+1 => now: " + changeRate + " with object " + collision.gameObject.name + " at line 88");

                // Zone1 is the body zone
                if (collision.gameObject.name == "Zone1")
                {
                    bodyDamaging = true;
                }

                // Zone9 is the foot zone
                if (collision.gameObject.name == "Zone9")
                {
                    footDamaging = true;
                }
            }


            if (collision.gameObject.tag == "EarZone")
            {
                isCoverEar = false;

                if (!momCallDeductionEnable && momCallEnable)
                {
                    changeRate += 6;
                    print("+6 => now: " + changeRate + " with object " + collision.gameObject.name + " at line 100");
                    momCallDeductionEnable = true;
                }
            }
        }
        
    }
    #endregion

    // Mom Call and Increase Deduction Speed
    public void MomCallDeduction()
    {
        print("deduct=============");
        momCallEnable = true;

        if (!isCoverEar)
        {
            changeRate += 6;
            print("+6 => now: " + changeRate +  " at line 116");

            momCallDeductionEnable = true;
        }

    }

    // Mom Call and Increase Deduction Speed End
    public void MomCallDeductionEnd()
    {
        print("=======deductend======");

        momCallEnable = false;

        if (momCallDeductionEnable)
        {
            momCallDeductionEnable = false;
            earDamageEffect1.gameObject.active = false;
            earDamageEffect2.gameObject.active = false;
            changeRate -= 6;
            print("-6 => now: " + changeRate + " at line 135");
        }
    }


    IEnumerator WinTimer()
    {
        while (!gameOver)
        {
            timePeriod -= 1;
            timerText.text = "Timer: " + timePeriod.ToString("N0") + "s";
            if (timePeriod <= 0 )
            {
                timePeriod = 0;
                // Win 
                gameStatus.text = "You Win!";
                GameOver();
            }
            yield return new WaitForSeconds(1);
        }
    }


    //deduction loop
    IEnumerator ComfortDeduction()
    {
        while (!gameOver)
        {
            
            comfortValue -= changeRate;
            if (comfortValue < 0)
            {
                comfortValue = 0;
            }

            if (changeRate == 0)
            {
                if(comfortValue < 100)
                    comfortValue += healRate;
            }

            comfortBar.value = comfortValue;
            comforText.text = comfortValue.ToString();

            CloseEmoji();
            if (comfortValue >= 75)
            {

                emojis[0].gameObject.active = true;
            }
            else if (comfortValue >= 50 && comfortValue < 75)
            {

                emojis[1].gameObject.active = true;
            }
            else if (comfortValue >= 25 && comfortValue < 50)
            {
                emojis[2].gameObject.active = true;
            }
            else if (comfortValue > 0 && comfortValue < 25)
            {
                emojis[3].gameObject.active = true;
            }
            else if(comfortValue <= 0)
            {
                emojis[4].gameObject.active = true;
               
                GameOver();
            }


            yield return new WaitForSeconds(changeSpeed);

        }
    }

    //gameover
    public void GameOver()
    {
        gameOver = true;
        gameManager.GetComponent<GameManager>().GameOver();
        //ConfirmGameOver
    }

    public void CloseEmoji()
    {
        foreach (Transform emoji in emojis)
        {
            emoji.gameObject.active = false;
        }
    }



}
