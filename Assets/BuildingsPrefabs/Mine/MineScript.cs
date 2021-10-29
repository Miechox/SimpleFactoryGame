using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
   
    public ResourcesScript resource;

    MapGenerator mapGenerator;
    GridPrototype grid;
    GameMode gameMode;

    public int miningAmount { get; set; }
    public float mineSpeed { get; set; }
    bool isMining=false;
    public int coord { get; set; }
    
    void Start()
    {
        mapGenerator = FindObjectOfType<MapGenerator>().GetComponent<MapGenerator>();
        gameMode = FindObjectOfType<GameMode>().GetComponent<GameMode>();
        grid = mapGenerator.grid;
        mineSpeed = 5f;
        miningAmount = 10;
        
    }

    void Update()
    {
        if (!isMining && isEnergy())
        {
            isMining = true;
            StartCoroutine(MineCooldown());
        }
    }
    public void FindResource(int resourceCoord)
    {
        coord = resourceCoord;
        if(grid.gridInfo[resourceCoord].occupiedBy.name == "Gold(Clone)")
        {
            resource = grid.gridInfo[resourceCoord].occupiedBy.GetComponent<ResourcesScript>();
        }
        if (grid.gridInfo[resourceCoord].occupiedBy.name == "Gase(Clone)")
        {
            resource = grid.gridInfo[resourceCoord].occupiedBy.GetComponent<ResourcesScript>();
        }
    }
    public bool isEnergy()
    {
        if(gameMode.electricityUsed<=gameMode.electricityMax && mapGenerator.planeGrid[coord].isElectric)
            return true;
        else
            return false;       
    }
    public void EnergySum()
    {
        gameMode.mineList.Add(this);
    }
    private void OnDestroy()
    {
        gameMode.mineList.Remove(this);
        gameMode.goldVal += 10;
    }

    IEnumerator MineCooldown()
    {
        yield return new WaitForSeconds(mineSpeed);
        isMining = false;
        if (resource)
        {
            if (resource.gameObject.name == "Gold(Clone)")
            {
                resource.resourceAmount -= miningAmount;
                gameMode.goldVal += miningAmount;
            }
            else if (resource.gameObject.name == "Gase(Clone)")
            {
                resource.resourceAmount -= miningAmount;
                gameMode.gaseVal += miningAmount;
            }
        }
    }
}
