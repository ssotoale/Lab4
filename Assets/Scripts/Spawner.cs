using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] objects;
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    private void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // Default to level 1
        AdjustSpawnRate(currentLevel);
    }

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        float spawnChance = Random.value;

        foreach (SpawnableObject obj in objects)
        {
            if (spawnChance < obj.spawnChance)
            {
                if (obj.prefab.name == "Student")
                {
                    GameObject obstacle = Instantiate(obj.prefab, new Vector3(transform.position.x, -3.47f, transform.position.z), Quaternion.identity);
                    break;
                }
                else
                {
                    GameObject obstacle = Instantiate(obj.prefab, new Vector3(transform.position.x, Random.Range(transform.position.y - 3.0f, transform.position.y), transform.position.z), Quaternion.identity);
                    break;
                }
            }

            spawnChance -= obj.spawnChance;
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void AdjustSpawnRate(int level)
    {
        // Increase spawn rate with level progression
        if (level == 1)
        {
            minSpawnRate = 2f;
            maxSpawnRate = 3f;
            objects[0].spawnChance = 0.05f;
            objects[1].spawnChance = 0.05f;
            objects[2].spawnChance = 0.5f;
            objects[3].spawnChance = 0.3f;
            objects[4].spawnChance = 0.08f;
            objects[5].spawnChance = 0.02f;
        }
        else if (level == 2)
        {
            minSpawnRate = 1.5f;
            maxSpawnRate = 2.5f;
            objects[0].spawnChance = 0.1f;
            objects[1].spawnChance = 0.1f;
            objects[2].spawnChance = 0.4f;
            objects[3].spawnChance = 0.3f;
            objects[4].spawnChance = 0.1f;
            objects[5].spawnChance = 0.05f;
        }
        else if (level == 3)
        {
            minSpawnRate = 1f;
            maxSpawnRate = 2f;
            objects[0].spawnChance = 0.15f;
            objects[1].spawnChance = 0.15f;
            objects[2].spawnChance = 0.3f;
            objects[3].spawnChance = 0.25f;
            objects[4].spawnChance = 0.1f;
            objects[5].spawnChance = 0.05f;
        }
        else
        {
            // Higher levels = faster spawning
            minSpawnRate = 0.5f;
            maxSpawnRate = 1.0f;
            objects[0].spawnChance = 0.2f;
            objects[1].spawnChance = 0.2f;
            objects[2].spawnChance = 0.2f;
            objects[3].spawnChance = 0.2f;
            objects[4].spawnChance = 0.1f;
            objects[5].spawnChance = 0.1f;
        }
    }
}
