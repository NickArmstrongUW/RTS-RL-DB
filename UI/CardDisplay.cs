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
    private EditDeckUI editDeckUI; // Reference to parent EditDeckUI

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

    public void SetupFromPlayerEntry(PlayerData.PlayerCardEntry entry, EditDeckUI parentUI = null)
    {
        playerEntry = entry;
        editDeckUI = parentUI; // Store reference to parent
        
        // decided not to have a null check here because if this is null something has gone horribly wrong and it's better to reboot
        if (CardFactory.Instance == null) {
            Debug.LogError("CardFactory.Instance is null! Creating a CardFactory instance.");
            GameObject cardFactoryGO = new GameObject("CardFactory");
            CardFactory cardFactory = cardFactoryGO.AddComponent<CardFactory>();
        }
        CardData cardData = CardFactory.Instance.GetCardData(entry.cardType);
        if (cardData != null) {
            if (cardData.cardName != null) {
                nameText.text = cardData.cardName;
            } else {
                nameText.text = entry.cardType.ToString();
            }
            artworkImage.sprite = cardData.cardImage;
        } else {
            Destroy(gameObject);
            return;
        }
        
        int countInDeck = editDeckUI.workingDeck.CountOf(entry);
        count = entry.countOwned - countInDeck; // Start with 0 selected for deck building
        UpdateCountText();

        // Clear any existing listeners
        plusButton.onClick.RemoveAllListeners();
        minusButton.onClick.RemoveAllListeners();

        // adds a card to the deck
        plusButton.onClick.AddListener(() =>
        {
            if (count > 0) {
                count--;
                UpdateCountText();
                // Call EditDeckUI to add card to deck
                if (editDeckUI != null) {
                    editDeckUI.AddCard(playerEntry);
                }
            }
        });

        // removes a card from the deck
        minusButton.onClick.AddListener(() =>
        {
            if (count < entry.countOwned) {
                count++;
                UpdateCountText();
                // Call EditDeckUI to remove card from deck
                if (editDeckUI != null) {
                    editDeckUI.RemoveCard(playerEntry);
                }
            }
        });
    }

    void UpdateCountText()
    {
        countText.text = $"x{count}";
    }
}
