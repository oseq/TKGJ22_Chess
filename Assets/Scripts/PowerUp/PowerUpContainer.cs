using UnityEngine;

public class PowerUpContainer : MonoBehaviour
{
    [SerializeField] private PowerUp[] powerUpQuery;
    [SerializeField] private PowerUp loot;

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        System.Random rng = new System.Random();

        var index = rng.Next(powerUpQuery.Length);
        loot = powerUpQuery[index];
    }

    public PowerUp Consume()
    {
        var tmp = loot;
        loot = null;
        return tmp;
    }
}
