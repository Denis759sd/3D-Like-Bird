using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public GameObject[] buttons;


    public void ShopPanel()
    {
        buttons[0].SetActive(!buttons[0].activeSelf);
    }

}
