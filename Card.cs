using UnityEngine;

namespace Cards
{
    public enum CardState
    {
        InDeck,
        InHand,
        Casting,
        InDiscard,
        Burned
    }

    public interface Card
    {
        int cost { get; set; }
        string name { get; set; }
        CardState State { get; set; }
        // int uses { get; set; } // how many times the card can be played
        // bool isSpell { get; set; }
        // int level { get; set; }

        void Play();
    }
}