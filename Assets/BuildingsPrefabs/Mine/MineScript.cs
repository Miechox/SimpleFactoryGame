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
        if (!isMining&&isEnergy()&&mapGenerator.planeGrid[(int)(transform.position.x*100)+ (int)transform.position.z].isElectrik)
        {
            isMining = true;
            StartCoroutine(MineCooldown());
        }
    }
    public void FindeResource()
    {
        for (int x = (int)transform.position.x - 1; x < (int)transform.position.x + 2; x++)
        {
            for (int y = (int)transform.position.z + 1; y > (int)transform.position.z - 2; y--)
            {
                int tempCoord = (x * 100) + y;
                
                if(grid.gridInfo[tempCoord].occupiedBy)
                {
                    if(grid.gridInfo[tempCoord].occupiedBy.name == "Gold(Clone)")
                    {
                        resource = grid.gridInfo[tempCoord].occupiedBy.GetComponent<ResourcesScript>();
                    }
                    if (grid.gridInfo[tempCoord].occupiedBy.name == "Gase(Clone)")
                    {
                        resource = grid.gridInfo[tempCoord].occupiedBy.GetComponent<ResourcesScript>();
                    }
                }
            }
        }
    }
    public bool isEnergy()
    {
        if(gameMode.electricityUsed>gameMode.electricityMax)
            return false;
        else       
            return true;       
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
