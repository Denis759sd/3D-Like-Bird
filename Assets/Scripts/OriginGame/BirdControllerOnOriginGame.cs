using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Globalization;
using UnityEditor;

public class BirdControllerOnOriginGame : MonoBehaviour
{
    [Header("Скорость м/с")]
    [SerializeField]
    int speed;

    [Header("Высота прыжка")]
    [SerializeField]
    int jumpForce;

    [Header("Рестарт")]
    public GameObject restartPan;
    [Header("Пауза")]
    public GameObject pausePan;

    [Header("Счётчик очков")]
    public Text score;
    public static int scoreOrigin;

    [Header("Рекорд")]
    public Text recordScore;
    [SerializeField]
    int record;

    [Header("Limiter text")]
    public Text limiterWorld;

    [Header("ScoreObject")]
    public GameObject scoreText;
    [Header("Menu")]
    public GameObject menuPanel;
    public Text hightScore;

    [Header("Платформа с травой")]
    public GameObject[] planeGrass;

    [Header("Кольца")]
    public GameObject[] origins;

    public static bool clickActive;
    bool jumpButtonPressed;

    Animator animator;
    Rigidbody rb;

    private void Start()
    {

        scoreOrigin = 0;

        clickActive = false;
        Time.timeScale = 0;

        record = PlayerPrefs.GetInt("RecordScoreOriginGame", scoreOrigin);
        hightScore.text = "" + record;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        RandomOrigins();
    }

    private void Update()
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
                PauseGame();
        }

        for (int i = 0; i < planeGrass.Length; i++)
        {
            if (transform.position.x - 1350 > planeGrass[i].transform.position.x)
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

        switch (scoreOrigin)
        {
            case 200:
                planeGrass[1].SetActive(true);
                break;

            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        animator.SetBool("Wings", true);
        if (jumpButtonPressed == true)
        {
            rb.velocity = Vector2.one * jumpForce;
            //rb.velocity = Vector3.up * jumpForce;
            jumpButtonPressed = false;
        }

        //rb.MovePosition(new Vector3(base.transform.position.x + speed * Time.deltaTime, base.transform.position.y, base.transform.position.z));
    }

    void DieBird()
    {
        if (scoreOrigin > record)
        {
            recordScore.text = "" + scoreOrigin;
            PlayerPrefs.SetInt("RecordScoreOriginGame", scoreOrigin);
        }
        else
            recordScore.text = "" + record;

        SoundManager.snd.PlayDieSounds();
        restartPan.SetActive(true);
        speed = 0;
        MainCam.speedCam = 0;
        jumpForce = 0;

        clickActive = false;
        Time.timeScale = 0;
    }
    public void RestartGame()
    {
        restartPan.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void BackLevel() => SceneManager.LoadScene(0);
    
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) DieBird();
        //if (collision.gameObject.CompareTag("Origin")) DieBird(); 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GetOrigin>())
        {
            SoundManager.snd.PlayPointsSounds();
            scoreOrigin += 100;
            score.text = "" + scoreOrigin;
        }
        else if (other.GetComponent<Baf>())
        {
            speed = 40;
        }
    }
    void RandomOrigins()
    {
        for (int i = 0; i < origins.Length; i++)
        {
            origins[i].transform.position = new Vector3(origins[i].transform.position.x, Random.Range(-53, -13), origins[i].transform.position.z);
            origins[i].transform.rotation = Quaternion.Euler(origins[i].transform.position.x, origins[i].transform.position.y, Random.Range(0,50));
        }
    }
}
