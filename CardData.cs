using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Card Data")]
public class CardData : ScriptableObject
{
    public string cardName;
    public int cost;
    public Sprite cardImage;
    public GameObject cardPrefab; // Base card prefab with Card component
    public CardType cardType;
}

public enum CardType
{
    Fireball,
} 