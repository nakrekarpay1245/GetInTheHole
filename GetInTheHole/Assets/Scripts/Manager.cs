using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject ball_1;
    [SerializeField]
    private GameObject ball_2;
    [SerializeField]
    private GameObject ball_3;
    [SerializeField]
    private GameObject ballDisplay;

    [SerializeField]
    private float health;


    [SerializeField]
    private GameObject celebrationDisplay;

    [SerializeField]
    private Image celebrationImage;

    [SerializeField]
    private Sprite[] celebrationSprites;


    [Header("LEVEL START END")]
    [SerializeField]
    private int levelNumber;
    [SerializeField]
    private GameObject levelDisplay;
    [SerializeField]
    private TextMeshProUGUI levelText;

    [SerializeField]
    private GameObject levelEndScreen;

    [SerializeField]
    private GameObject levelFailed;
    [SerializeField]
    private GameObject restartButton;

    [SerializeField]
    private GameObject levelCompleted;
    [SerializeField]
    private GameObject nextButton;

    public bool isLevelFinished;

    public static Manager instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        StartLevel();
    }

    public void DecreaseHealth()
    {
        health--;
        DisplayBalls();
    }

    private void DisplayBalls()
    {
        switch (health)
        {
            case 0:
                ball_1.transform.DOScale(Vector3.zero, 0.15f);
                FinishLevel(false);
                break;
            case 1:
                ball_2.transform.DOScale(Vector3.zero, 0.15f);
                break;
            case 2:
                ball_3.transform.DOScale(Vector3.zero, 0.15f);
                break;
            case 3:
                ball_1.transform.DOScale(Vector3.zero, 0.15f);
                ball_2.transform.DOScale(Vector3.zero, 0.15f);
                ball_3.transform.DOScale(Vector3.zero, 0.15f);
                break;
            default:
                ball_1.transform.DOScale(Vector3.one, 0.15f);
                ball_2.transform.DOScale(Vector3.one, 0.15f);
                ball_3.transform.DOScale(Vector3.one, 0.15f);
                break;
        }
    }

    public void DisplayCelebration()
    {
        celebrationImage.sprite = celebrationSprites[Random.Range(0, celebrationSprites.Length)];
        celebrationDisplay.transform.DOScale(Vector3.one * 2, 0.25f).OnComplete(() =>
        celebrationDisplay.transform.DOScale(Vector3.one, 0.15f));
    }

    public void HideCelebration()
    {
        celebrationDisplay.transform.DOScale(Vector3.zero, 0);
    }

    // LEVEL START-END
    private void StartLevel()
    {
        levelText.text = "LEVEL - " + levelNumber;
        StartCoroutine(StartLevelRoutine());
    }

    private IEnumerator StartLevelRoutine()
    {
        Debug.Log("Level Started");

        HideCelebration();

        levelDisplay.transform.DOScale(Vector3.zero, 0);
        levelDisplay.transform.DOScale(Vector3.one, 0.75f);

        ballDisplay.transform.DOScale(Vector3.zero, 0);
        ballDisplay.transform.DOScale(Vector3.one, 0.75f);

        levelEndScreen.transform.DOScale(Vector3.zero, 0);

        levelFailed.transform.DOScale(Vector3.zero, 0);
        restartButton.transform.DOScale(Vector3.zero, 0);

        levelCompleted.transform.DOScale(Vector3.zero, 0);
        nextButton.transform.DOScale(Vector3.zero, 0);
        yield return new WaitForSeconds(1);
    }

    public void FinishLevel(bool _isLevelCompleted)
    {
        Debug.Log("Level Finished");
        isLevelFinished = true;
        levelEndScreen.transform.DOScale(Vector3.one, 0.25f);
        levelDisplay.transform.DOScale(Vector3.zero, 0.25f);
        if (_isLevelCompleted)
        {
            StartCoroutine(LevelCompletedRoutine());
        }
        else
        {
            StartCoroutine(LevelFailedRoutine());
        }
    }

    private IEnumerator LevelCompletedRoutine()
    {
        Debug.Log("Level Completed");
        DisplayCelebration();
        yield return new WaitForSeconds(0.25f);
        levelCompleted.transform.DOScale(Vector3.one, 0.25f);
        nextButton.transform.DOScale(Vector3.one, 0.25f);
        ballDisplay.transform.DOScale(Vector3.zero, 0.25f);
        yield return new WaitForSeconds(1);
    }

    private IEnumerator LevelFailedRoutine()
    {
        Debug.Log("Level Failed");
        yield return new WaitForSeconds(0.25f);
        levelFailed.transform.DOScale(Vector3.one, 0.25f);
        restartButton.transform.DOScale(Vector3.one, 0.25f);
        yield return new WaitForSeconds(1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(levelNumber - 1);
    }

    public void NextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings > levelNumber)
            SceneManager.LoadScene(levelNumber);
        else
            SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
