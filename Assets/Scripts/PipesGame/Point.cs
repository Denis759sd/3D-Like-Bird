using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour
{

    [Header("Полная труба")]
    [SerializeField]
    GameObject FullPipe;
    
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(PipeTransform());
    }

    IEnumerator PipeTransform()
    {
        yield return new WaitForSeconds(1.5f);
        FullPipe.transform.position = new Vector3(FullPipe.transform.position.x + 600, Random.Range(-10, 25), FullPipe.transform.position.z);
    }
    
}
