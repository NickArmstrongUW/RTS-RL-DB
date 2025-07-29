using UnityEngine;
using TMPro;
using UnityEngine.UI;
using CardSystem;

public class CardDisplay : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image artworkImage;
    public TextMeshProUGUI countText;

    public Button plusButton;
    public Button minusButton;

    private int count;
    private PlayerData.PlayerCardEntry playerEntry;

    public void Setup(CardData data)
    {
        nameText.text = data.cardName;
        artworkImage.sprite = data.cardImage;
        count = 0; //data.count;
        UpdateCountText();

        plusButton.onClick.AddListener(() =>
        {
            count++;
            UpdateCountText();
        });

        minusButton.onClick.AddListener(() =>
        {
            if (count > 0) count--;
            UpdateCountText();
        });
    }

    public void SetupFromPlayerEntry(PlayerData.PlayerCardEntry entry)
    {
        playerEntry = entry;
        
        // Find the CardData for this card type to get the visual info
        CardData cardData = FindCardDataByType(entry.cardType);
        if (cardData != null) {
            nameText.text = cardData.cardName;
            artworkImage.sprite = cardData.cardImage;
        } else {
            nameText.text = entry.cardType.ToString();
            // You might want to set a default sprite here
        }
        
        count = 0; // Start with 0 selected for deck building
        UpdateCountText();

        plusButton.onClick.AddListener(() =>
        {
            if (count < entry.countOwned) {
                count++;
                UpdateCountText();
            }
        });

        minusButton.onClick.AddListener(() =>
        {
            if (count > 0) count--;
            UpdateCountText();
        });
    }

    private CardData FindCardDataByType(CardType cardType) {
        // Use CardFactory to find the CardData for this card type
        CardFactory cardFactory = FindObjectOfType<CardFactory>();
        if (cardFactory != null) {
            return cardFactory.GetCardData(cardType);
        }
        return null;
    }

    void UpdateCountText()
    {
        countText.text = $"x{count}";
    }
}
