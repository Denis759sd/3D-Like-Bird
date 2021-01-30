using UnityEngine;

public class MainCam : MonoBehaviour
{
    [Header("Скорость м/с")]
    [SerializeField]
    int speed;

    public static int speedCam;

    private void Start()
    {
        speedCam = speed;
    }
    void FixedUpdate()
    {
        transform.Translate(new Vector3(speedCam, 0f,0f) * Time.deltaTime);
    }
}
