# RTS-RL-DB
Unity Scripting for game PoC

HOW TO ADD A CARD:

Step 1: Create an object that extends CardData and overrides Activate and PreCast
 - Object must have this line beforehand: [CreateAssetMenu(menuName = "Cards/<YOUR CARD NAME>")]

Step 2: In CardFactory.cs, add a card type for your new card in the CardType enum

Step 3: In Unity Project window, go to the Card Data folder and create a new scriptable object of your type card
Create -> Card -> YOUR CARD NAME
    - Fill out at least the Sprite and whatever numbers you would like for any other values, at least for testing
    - Everything other than Card Image and Card Type is for testing. Card Type should be the enum value you put in CardFactory.cs

Step 4: In the CardFactory in Unity, add a new element to the Card Type Setup list. Set the Type to your card's enum and the Data to your new CardData (of type your card)

If your card casts a spell you will need to make a prefab for that as well, longer more complicated guide coming