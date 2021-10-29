using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPole : MonoBehaviour
{
    private GridPrototype grid;
    private MapGenerator mapGenerator;
    private GameMode gameMode;
    

    private void Start()
    {
        grid = FindObjectOfType<MapGenerator>().GetComponent<MapGenerator>().grid;
        mapGenerator = FindObjectOfType<MapGenerator>().GetComponent<MapGenerator>();
        gameMode = FindObjectOfType<GameMode>().GetComponent<GameMode>();
    }

    public void SetElectricField()
    {
        for (int x = (int)transform.position.x - 2;x<(int)transform.position.x+3;x++)
        {
            for (int y = (int)transform.position.z + 2;y > (int)transform.position.z - 3;y--)
            {
                grid.gridInfo[CalculateCoord(x, y)].isElectricity = true;
                mapGenerator.planeGrid[CalculateCoord(x, y)].SetPlaneTextrureToYellow();
                mapGenerator.planeGrid[CalculateCoord(x, y)].isElectric = true;
            }
        }
    }
    public void DestroyElcetricField()
    {
        for (int x = (int)transform.position.x - 2; x < (int)transform.position.x + 3; x++)
        {
            for (int y = (int)transform.position.z + 2; y > (int)transform.position.z - 3; y--)
            {
                grid.gridInfo[CalculateCoord(x, y)].isElectricity = false;
                mapGenerator.planeGrid[CalculateCoord(x, y)].isElectric = false;
                mapGenerator.planeGrid[CalculateCoord(x, y)].SetMaterialToOriginal();
            }
        }
    }
    private void OnDestroy()
    {
        gameMode.goldVal += 20;
    }

    private int CalculateCoord(int x,int y)
    {
        int calculatedCoord = (x * 100) + y;

        return calculatedCoord;
    }
}
