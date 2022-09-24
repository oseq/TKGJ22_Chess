using System;
using System.Collections;
using UnityEngine;

public class PowerUpsCoordinator : MonoBehaviour
{
    [Serializable]
    public struct SpawnRules
    {
        public float startDelay;
        public float spawnCooldown;
    }

    [SerializeField] private PowerUpContainer[] powerUps;
    [SerializeField] private SpawnRules spawnRules;

    private void Start()
    {
        foreach(var powerUp in powerUps)
        {
            powerUp.OnLootConsumed.AddListener(ScheduleNextGenerate);
        }

        IEnumerator StartScheduler(float startDelay)
        {
            yield return new WaitForSeconds(startDelay);
            PickRandom().Generate();
        }

        StartCoroutine(StartScheduler(spawnRules.startDelay));
    }

    private void ScheduleNextGenerate()
    {
        IEnumerator Scheduler(float spawnCooldown)
        {
            yield return new WaitForSeconds(spawnCooldown);
            PickRandom().Generate();
        }

        StartCoroutine(Scheduler(spawnRules.spawnCooldown));
    }

    PowerUpContainer PickRandom()
    {
        System.Random rng = new System.Random();

        var index = rng.Next(powerUps.Length);
        return powerUps[index];
    }
}
