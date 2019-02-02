using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public delegate void GameDelegate();
    public static GameManager Instance;


    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countdownPage;
    public Text scoreText;
    public Text countDownText;

    public GameObject quilt;
    public GameObject momCalling;
    public Image quiltImage;
    public Image catImage;


    public AudioSource roosterSound;
    

    enum PageState
    {
        None,
        Start,
        GameOver,
        Countdown
    }

    bool gameOver = false;

    private void Awake()
    {
        Instance = this;
    }

    


    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);
                break;
            case PageState.Start:
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);
                break;
            case PageState.GameOver:
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                countdownPage.SetActive(false);
                break;
            case PageState.Countdown:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(true);
                break;
        }
    }
    
    #region start functions
    void Start()
    {
        SetPageState(PageState.Countdown);
        StartCoroutine("Countdown");
    }

    public void Restart()
    {
        SetPageState(PageState.Countdown);
        StartCoroutine("Countdown");
    }

    IEnumerator Countdown()
    {
        int count = 3;
        for (int i = 0; i < count; i++)
        {
            countDownText.text = (count - i).ToString();
            yield return new WaitForSeconds(1);
        }
        GameStart();        
    }

    void GameStart()
    {
        roosterSound.Play();

        SetPageState(PageState.None);
        quiltImage.sprite = Resources.Load<Sprite>("Images/quilt2");
        quilt.GetComponent<QuiltMovement>().GameStart();
        quilt.GetComponent<ValueControl>().GameStart();
        momCalling.GetComponent<MomCall>().GameStart();
        catImage.GetComponent<CatMovement>().GameStart();
        gameOver = false;



    }
    #endregion

    #region gameover functions
    public void GameOver()
    {

        quilt.GetComponent<QuiltMovement>().GameOver();       
        momCalling.GetComponent<MomCall>().GameOver();
        catImage.GetComponent<CatMovement>().GameOver();
        quiltImage.sprite = Resources.Load<Sprite>("Images/quilt1");
        SetPageState(PageState.GameOver);
    }

    #endregion

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }


}
