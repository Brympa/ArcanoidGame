using UnityEngine;

public class Brick : MonoBehaviour
{
    public enum BrickType
    {
        Common,
        Uncommon,
        Rare
    }

    public BrickType Type;
    public int Health = 1;
    public int Points;

    private void Start()
    {
        switch (Type)
        {
            case BrickType.Common:
                Points = 10;
                break;
            case BrickType.Uncommon:
                Points = 20;
                break;
            case BrickType.Rare:
                Points = 30;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Health--;

            if (Health <= 0)
            {
                GameManager.Instance.AddScore(Points);
                Destroy(gameObject);
            }
        }
    }
}