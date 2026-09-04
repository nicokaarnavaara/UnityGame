using UnityEngine;
using TMPro;

public class CollectibleCounter : MonoBehaviour
{
    public static CollectibleCounter Instance;

    public int diamonds = 0;

    public TextMeshProUGUI collectibleText;

    private void Awake()
    {
        Instance = this;
        UpdateUI();
    }

    public void AddDiamond(int amount)
    {
        diamonds += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        collectibleText.text = "Diamonds: " + diamonds;
    }
}