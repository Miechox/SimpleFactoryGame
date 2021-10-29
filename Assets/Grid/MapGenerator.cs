using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject HubPrefab;

    [SerializeField]
    private GameObject TerrainPrefab;
    [SerializeField]
    private GameObject TreePrefab;
    [SerializeField]
    private GameObject GoldPrefab;
    [SerializeField]
    private GameObject GasePrefab;

    private HeightMap heightMap;
    private Texture2D texture;
    public GridPrototype grid;

    public Vector3 hubPosition;

    private PlayerController playerController;
    private CameraMovement cameraMovement;

    public List<PlaneGridSC> planeGrid = new List<PlaneGridSC>();

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        cameraMovement = FindObjectOfType<CameraMovement>().GetComponent<CameraMovement>();
        heightMap = FindObjectOfType<HeightMap>().GetComponent<HeightMap>();

        RenderWorldObjects();

        playerController.grid = grid;
        cameraMovement.camPosition = new Vector3(hubPosition.x, cameraMovement.transform.position.y, hubPosition.z - 6);

    }
  

    private void RenderWorldObjects()
    {
        RenderBasicTerrain();
        WorldHubPlacement();
        WorldObstaclePlacement();
    }
    private void RenderBasicTerrain()
    {
        grid = new GridPrototype(100, 100);
        for (int i = 0; i < grid.gridInfo.Count; i++)
        {
           GameObject temp = Instantiate(TerrainPrefab, grid.gridInfo[i].gridPoints, Quaternion.identity);
           planeGrid.Add(temp.GetComponent<PlaneGridSC>());
        }
    }
    private void WorldHubPlacement()
    {
        hubPosition = grid.gridInfo[5050].gridPoints;
        grid.gridInfo[5050].occupiedBy = Instantiate(HubPrefab, hubPosition, Quaternion.identity);
    }
    private void WorldObstaclePlacement()
    {
        for (int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 100; y++)
            {
                float perlVal = heightMap.texture.GetPixel(x, y).grayscale;

                if (perlVal > 0 && perlVal < 0.33f)
                    LocationForObj(TreePrefab, x, y);
                else if (perlVal > 0.47 && perlVal < 0.48)
                    LocationForObj(GasePrefab, x, y);
                else if (perlVal > 0.78 && perlVal < 0.80)
                    LocationForObj(GoldPrefab, x, y);
            }
        }
    }
    private void LocationForObj(GameObject obj, int x, int y)
    {
        int tempX = x;
        int tempY = y;
        int tempCoord = (tempX * 100) + tempY;
        if (!grid.gridInfo[tempCoord].occupiedBy)
            grid.gridInfo[tempCoord].occupiedBy = Instantiate(obj, grid.gridInfo[tempCoord].gridPoints, Quaternion.identity);
    }
}
