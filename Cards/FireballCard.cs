using UnityEngine;


class FireballCard: Card
{
    public int damage;

    public void OnClick()
    {
        Debug.Log("Fireball clicked");
    }
}