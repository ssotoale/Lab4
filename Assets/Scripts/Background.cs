using UnityEngine;

public class Ground : MonoBehaviour
{

    public float speed = 5f;

    private MeshRenderer mesh;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // Default to level 1
        AdjustSpeedMultiplier(currentLevel);
    }

    void AdjustSpeedMultiplier(int level)
    {
        if (level == 1)
        {
            speed = 0.4f;

        }
        else if (level == 2)
        {
            speed = 0.7f;
        }
        else if (level == 3)
        {
            speed = 1f;
        }
        else
        {
            speed = 1.4f;
        }
    }

    private void Update()
    {
        mesh.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
    }
}
