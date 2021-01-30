using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSkins : MonoBehaviour
{
    [Header("Skin")]
    public Mesh[] mesh;

    [Header("Wings")]
    public Mesh[] wings;

    //public ParticleSystem fire;

    private int saveBird;

    public MeshFilter wingsFilter;
    MeshFilter meshFilter;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        saveBird = PlayerPrefs.GetInt("SaveBird", saveBird);
        switch (saveBird)
        {
            case 1:
                meshFilter.mesh = mesh[1];
                wingsFilter.mesh = wings[1];
                break;

            case 2:
                meshFilter.mesh = mesh[0];
                wingsFilter.mesh = wings[0];
                break;
            case 3:
                meshFilter.mesh = mesh[2];
                wingsFilter.mesh = wings[0];
                break;

            default:
                meshFilter.mesh = mesh[0];
                wingsFilter.mesh = wings[0];
                break;
        }

    }

    public void ChangeSkinDemon()
    {
        meshFilter.mesh = mesh[1];
        wingsFilter.mesh = wings[1];
        saveBird = 1;
        PlayerPrefs.SetInt("SaveBird", saveBird);
    }

    public void ChangeSkinDefault()
    { 
        meshFilter.mesh = mesh[0];
        wingsFilter.mesh = wings[0];
        saveBird = 2;
        PlayerPrefs.SetInt("SaveBird", saveBird);
    }

    public void ChangeSkinCap()
    {
        meshFilter.mesh = mesh[2];
        wingsFilter.mesh = wings[0];
        saveBird = 3;
        PlayerPrefs.SetInt("SaveBird", saveBird);
    }


}
