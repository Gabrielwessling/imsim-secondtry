using UnityEngine;

public class MovingTexture : MonoBehaviour
{
    [SerializeField] private float scrollSpeedU = 0.1f;
    [SerializeField] private float scrollSpeedV = 0.1f;

    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Material mat = renderer.material;
            Vector2 offset = mat.mainTextureOffset;
            offset.x += scrollSpeedU * Time.deltaTime;
            offset.y += scrollSpeedV * Time.deltaTime;
            mat.mainTextureOffset = offset;
        }
    }
}
