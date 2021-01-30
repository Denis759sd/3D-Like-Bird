using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    [Header("Bird")]
    [SerializeField]
    GameObject bird;

    [Header("Камни")]
    [SerializeField]
    GameObject[] rocks;

    [Header("Деревья")]
    [SerializeField]
    public GameObject[] trees;

    void Update()
    {
        for (int i = 0; i < rocks.Length; i++)
        {
            if (bird.transform.position.x - 180 > rocks[i].transform.position.x)
            {
                rocks[i].transform.position = new Vector3(rocks[i].transform.position.x + 550, rocks[i].transform.position.y, rocks[i].transform.position.z);
            }
        }



        for (int i = 0; i < trees.Length; i++)
        {
            if (transform.position.x - 100 > trees[i].transform.position.x)
            {
                trees[i].transform.position = new Vector3(trees[i].transform.position.x + 1000, trees[i].transform.position.y, trees[i].transform.position.z);
            }
        }
    }
}
