using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using System.Diagnostics;
using System.Collections;

public class BirdController : MonoBehaviour
{
#if UNITY_ANDROID
    private string gameId = "3825981";
#endif

    [Header("Скорость м/с")]
    [SerializeField]
    int speed;
    [Header("Высота прыжка")]
    [SerializeField]
    int jumpForce;

    [Header("Счётчик очков")]
    [SerializeField]
    Text score;
    public static int scoreCount;

    [Header("Рестарт")]
    [SerializeField]
    GameObject restartPan;

    [Header("Пауза")]
    [SerializeField]
    GameObject pausePan;

    [Header("Рекорд")]
    [SerializeField]
    Text recordScore;
    [SerializeField]
    int record;

    [Header("Платформа с травой")]
    [SerializeField]
    GameObject[] planeGrass;

    [Header("Pipes")]
    [SerializeField]
    GameObject[] pipes;

    [Header("Limiter text")]
    [SerializeField]
    Text limiterWorld;

    [Header("Score")]
    public GameObject scoreText;
    [Header("Menu")]
    public GameObject menuPanel;
    [SerializeField]
    Text hightScore;

    private bool jumpButtonPressed;
    public static bool clickActive;
    int randomAds;

    Animator animator;
    Rigidbody rb;


    private void Start()
    {
        scoreCount = 0;
        randomAds = Random.Range(0,3);
;
        clickActive = false;
        Time.timeScale = 0;

        record = PlayerPrefs.GetInt("RecordScore", scoreCount);
        hightScore.text = "" + record;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        if(Advertisement.isSupported)
            Advertisement.Initialize(gameId, false);

        RandomSpawnPipes();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && clickActive == true)
        {
            animator.SetTrigger("Jump");
            Time.timeScale = 1;
            SoundManager.snd.PlayJumpSounds();
            jumpButtonPressed = true;
        }


        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }
    }

    void FixedUpdate()
    {
        animator.SetBool("Wings", true);
        if (jumpButtonPressed == true)
        {
            rb.velocity = Vector3.up * jumpForce;
            jumpButtonPressed = false;
        }

        rb.MovePosition(new Vector3(base.transform.position.x + speed * Time.deltaTime, base.transform.position.y, base.transform.position.z ));

        switch (scoreCount)
        {
            case 300:
                planeGrass[1].SetActive(true);
                break;

            case 3000:
                speed = 58;
                MainCam.speedCam = 58;
                break;

            default:
                break;
        }


        for (int i = 0; i < planeGrass.Length; i++)
        {
            if (transform.position.x -1350 > planeGrass[i].transform.position.x)
            {
                planeGrass[i].transform.position = new Vector3(planeGrass[i].transform.position.x + 2844, planeGrass[i].transform.position.y, planeGrass[i].transform.position.z);
            }
        }


        if (transform.position.y > 90)
        {
            recordScore.text = "" + record;
            restartPan.SetActive(true);

            speed = 0;
            MainCam.speedCam = 0;
            limiterWorld.enabled = true;

            jumpForce = 0;
        }
    }

    void DieBird()
    {
        if (scoreCount > record)
        {
            recordScore.text = "" + scoreCount;
            PlayerPrefs.SetInt("RecordScore", scoreCount);
        }
        else
            recordScore.text = "" + record;

        SoundManager.snd.PlayDieSounds();
        restartPan.SetActive(true);
        speed = 0;
        MainCam.speedCam = 0;
        jumpForce = 0;

        clickActive = false;

        if (randomAds == 2)
            ShowUnityAd();
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        restartPan.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void NextLevel() => SceneManager.LoadScene(1);
    

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePan.SetActive(true);
        clickActive = false;
    }

    public void ResumeGame()
    {
        pausePan.SetActive(false);
        Time.timeScale = 1;
        clickActive = true;
    }

    public void StartGame()
    {
        clickActive = true;
        menuPanel.SetActive(false);
        scoreText.SetActive(true);
    }

    public void ShowUnityAd()
    {
        if(Advertisement.IsReady())
            Advertisement.Show("video");
    }

    private void RandomSpawnPipes()
    {
        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i].transform.position = new Vector3(pipes[i].transform.position.x, Random.Range(-10, 25), pipes[i].transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            DieBird();
        else if (collision.gameObject.CompareTag("Pipe"))
        {
            DieBird();
        }
               
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Point>())
        {
            SoundManager.snd.PlayPointsSounds();
            scoreCount += 100;
            score.text = "" + scoreCount;
        }
        
    }
}
