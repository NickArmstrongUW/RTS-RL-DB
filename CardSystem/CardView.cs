using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CardSystem;

namespace CardSystem {
public class CardView : MonoBehaviour {
    public Image artworkImage;
    public TextMeshProUGUI costText;
    
    private Card card; // Reference to the Card component

    public void Initialize(CardData data, Card cardComponent) {
        this.card = cardComponent;
        
        if (data == null) {
            Debug.LogError("CardView.Initialize: CardData is null!");
            return;
        }
        
        if (artworkImage != null) {
            artworkImage.sprite = data.cardImage;
        } else {
            Debug.LogError("CardView.Initialize: artworkImage is null! Please assign it in the Inspector.");
        }
        
        UpdateCostDisplay(data.cost);
    }
    
    public void UpdateCostDisplay(int newCost) {
        if (costText != null) {
            costText.text = newCost.ToString();
            costText.enabled = true; // Make sure it's visible
        } else {
            Debug.LogError("CardView.UpdateCostDisplay: costText is null! Please assign it in the Inspector.");
        }
    }
}
}