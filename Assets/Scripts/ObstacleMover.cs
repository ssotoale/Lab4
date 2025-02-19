using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        AdjustSpeedMultiplier(currentLevel);
    }

    void AdjustSpeedMultiplier(int level)
    {
        if (level == 1)
        {
            speed = 7f;
        }
        else if (level == 2)
        {
            speed = 10f;
        }
        else if (level == 3)
        {
            speed = 16f;
        }
        else
        {
            speed = 18f;
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
        
    }
}
