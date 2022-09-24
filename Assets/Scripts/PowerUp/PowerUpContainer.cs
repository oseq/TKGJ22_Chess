using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PowerUp")]
public class PowerUpContainer : MonoBehaviour
{
    [SerializeField] private PowerUp[] powerUpQuery;
    [SerializeField] private PowerUp loot;

    public UnityEvent OnLootGenerated = new UnityEvent();
    public UnityEvent OnLootConsumed = new UnityEvent();

    public void Generate()
    {
        System.Random rng = new System.Random();

        var index = rng.Next(powerUpQuery.Length);
        loot = powerUpQuery[index];

        OnLootGenerated.Invoke();
    }

    public PowerUp Consume()
    {
        OnLootConsumed.Invoke();

        var tmp = loot;
        loot = null;
        return tmp;
    }
}
