using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CardSystem;

namespace CardSystem {
public class CardView : MonoBehaviour {
    public Image artworkImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI costText;

    public void Initialize(CardData data) {
        if (data == null) {
            Debug.LogError("CardView.Initialize: CardData is null!");
            return;
        }
        
        if (nameText != null) {
            nameText.text = data.cardName;
            nameText.enabled = false;
        } else {
            Debug.LogError("CardView.Initialize: nameText is null! Please assign it in the Inspector.");
        }
        
        if (artworkImage != null) {
            artworkImage.sprite = data.cardImage;
        } else {
            Debug.LogError("CardView.Initialize: artworkImage is null! Please assign it in the Inspector.");
        }
        
        if (descriptionText != null) {
            descriptionText.text = data.description;
            descriptionText.enabled = false;
        } else {
            Debug.LogError("CardView.Initialize: descriptionText is null! Please assign it in the Inspector.");
        }
        
        if (costText != null) {
            costText.text = data.cost.ToString();
            costText.enabled = false;
        } else {
            Debug.LogError("CardView.Initialize: costText is null! Please assign it in the Inspector.");
        }
    }
}
}