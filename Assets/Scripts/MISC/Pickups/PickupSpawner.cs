using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goldCoin;
    [SerializeField] private GameObject healthGlobe;
    [SerializeField] private GameObject staminaGlobe;

    public void SpawnPickup()
    {
        var random = Random.Range(1, 4);

        HandlePickupToSpawn(random);
    }

    private void HandlePickupToSpawn(int random)
    {
        switch (random)
        {
            case 1:
            {
                var randomNCoins = Random.Range(1, 5);
                for (var i = 0; i < randomNCoins; i++) Instantiate(goldCoin, transform.position, Quaternion.identity);
                break;
            }
            case 2:
                Instantiate(healthGlobe, transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(staminaGlobe, transform.position, Quaternion.identity);
                break;
        }
    }
    
}