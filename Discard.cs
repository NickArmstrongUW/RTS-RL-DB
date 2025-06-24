using UnityEngine;
using System.Collections.Generic;

class Discard: MonoBehaviour
{
    public int size;
    public List<Card> cards;

    // shows you what's in your graveyard
    public void OnClick()
    {
        Debug.Log("Card clicked");
    }
}