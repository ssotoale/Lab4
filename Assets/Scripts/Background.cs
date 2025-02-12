using UnityEngine;

public class Ground : MonoBehaviour
{

    public float speed = 5f;

    private MeshRenderer mesh;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        mesh.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
    }
}
