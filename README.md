# RTS-RL-DB
Unity Scripting for game PoC

HOW TO ADD A CARD:

Step 1: Create an object that extends CardData and overrides Activate and PreCast
 - Object must have this line beforehand: [CreateAssetMenu(menuName = "Cards/<YOUR CARD NAME>")]

 Step 2: In Unity Project window, go to the Card Data folder and create a new scriptable object of your type card
 Create -> Card -> YOUR CARD NAME
    - Fill out atleast the Sprite and whatever numbers you would like for any other values, atleast for testing

 Step 3: In CardFactory.cs, add a card type for your new card in the Card Types num

 Step 4: In the CardFactory in Unity, add a new element to the list of cards. Set that element to your card's enum in card factory, and the value to your new CardData (of type your card)

 If your card casts a spell you will need to make a prefab for that as well, longer more complicated guide coming