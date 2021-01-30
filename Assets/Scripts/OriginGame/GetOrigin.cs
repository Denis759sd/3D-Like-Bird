using System.Collections;
using UnityEngine;

public class GetOrigin : MonoBehaviour
{
    [Header("Кольцо")]
    [SerializeField]
    GameObject origin;

    [Header("Кольца с эффектом")]
    public GameObject[] extraOrigins;
    private GameObject gameObject;

    BoxCollider boxCollider;

    private int spawnPosX = 140;
    private int eventer;
    private float random;

    BirdControllerOnOriginGame bird;

    private void Start()
    {
        random = Random.Range(300, 1000);
        CellNumber(ref random);
        Debug.Log(random);
    }

    private void Update()
    {
        eventer = BirdControllerOnOriginGame.scoreOrigin;

    }

    void CellNumber(ref float a)
    {
        if (a >= 1000)
        {
            a /= 1000;
            a = Mathf.Round(a);
            a *= 1000;
        }
        else
        {
            a /= 100;
            a = Mathf.Round(a);
            a *= 100;
        }   
    }


    private void OnTriggerEnter(Collider other)
    {
        if (eventer == random)
            SpawnExtraOrigins();
        

        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        StartCoroutine(OriginTransform());
    }

    private void SpawnExtraOrigins()
    {
        gameObject = Instantiate(extraOrigins[0], extraOrigins[0].transform.position, Quaternion.identity);
        gameObject.transform.position = new Vector3(origin.transform.position.x + 140, Random.Range(-53, -13), -4);
        gameObject.transform.rotation = Quaternion.Euler(0,0,Random.Range(20,50));
        spawnPosX = 280;
    }

    IEnumerator OriginTransform()
    {
        yield return new WaitForSeconds(1f);
        origin.transform.position = new Vector3(origin.transform.position.x + spawnPosX, Random.Range(-53, -13), origin.transform.position.z);
        origin.transform.rotation = Quaternion.Euler(0, 0, Random.Range(20, 50));
        boxCollider.enabled = true;
        spawnPosX = 140;
    }

}
