using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeightMap : MonoBehaviour
{
    [SerializeField]
    private int width = 100;
    [SerializeField]
    private int height = 100;

    [SerializeField]
    private float scale = 6f;

    private float offsetX = 100f;
    private float offsetY = 100f;

    public Texture2D texture;

    private void Awake()
    {
        offsetX = Random.Range(0, 99999f);
        offsetY = Random.Range(0, 99999f);
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    Texture2D GenerateTexture()
    {
        texture = new Texture2D(width, height);

        for(int x =0;x<width;x++)
        {
            for(int y=0;y<height;y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y,color);
            }
        }
        texture.Apply();
        return texture;
    }
    Color CalculateColor(int x, int y)
    {
        float xCoord = (float)x / width* scale+ offsetX;
        float yCoord = (float)y / height* scale+ offsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample);
    }
}
