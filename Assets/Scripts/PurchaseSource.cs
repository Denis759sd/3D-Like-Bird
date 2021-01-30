using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;


public class PurchaseSource : MonoBehaviour
{
    [Header("Price")]
    [SerializeField]
    GameObject[] priceButtons;

    [Header("Items")]
    public Button[] items;

    private int saveNumber;

    private void Start()
    {
        saveNumber = PlayerPrefs.GetInt("Save", saveNumber);

        if (saveNumber == 1)
        {
            priceButtons[0].SetActive(false);
            items[1].interactable = true;
        }

    }

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == "skin_demon")
        {
            priceButtons[0].SetActive(false);
            items[1].interactable = true;
            saveNumber = 1;
            PlayerPrefs.SetInt("Save", saveNumber);
        }

    }

    public void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("Error in app purchasing");
    }
}

    