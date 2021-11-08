using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    
    List<Generator> generatorsList = new List<Generator>();
    public List<MineScript> mineList = new List<MineScript>();

    #region Resources

    public float electricityMax { get; set; }//1
    public float electricityUsed { get; set; }//2

    //public int treesVal { get; set; }//3
    public int goldVal { get; set; }//4
    public int gaseVal { get; set; }//5


    public int mineElctricityUsage { get; set; }
    public int generatorElectricityGenerated { get; set; }

    #endregion

    public Text[] playerResourceUI = new Text[5];


    void Start()
    {
        mineElctricityUsage = 10;
        generatorElectricityGenerated = 30;
        goldVal = 350;
        gaseVal = 0;
    }

    
    void Update()
    {
        EnergyCheck();
    }
    private void LateUpdate()
    {
        UpdateResourcesUI();
    }


    public void UpdateResourcesUI()
    {
        playerResourceUI[0].text = $"Uisng: {electricityUsed}";
        playerResourceUI[1].text = $"Max ele: {electricityMax}";
        playerResourceUI[3].text = $"Gold: {goldVal}";
        playerResourceUI[4].text = $"Gase: {gaseVal}";
    }

    private void EnergyCheck()
    {
        electricityUsed = mineList.Count * mineElctricityUsage;
    }
    public void AddGenerator(Generator gen)
    {
        generatorsList.Add(gen);
        electricityMax = generatorsList.Count * generatorElectricityGenerated;
    }

    public void GeneratorUpgrade()
    {
        if(gaseVal >= 900)
        {
            generatorElectricityGenerated += 5;
            gaseVal -= 900;
            electricityMax = generatorsList.Count * generatorElectricityGenerated;
        }
    }
    public void MineSpeedUpgrade()
    {
        if(gaseVal >= 1500)
        {
            gaseVal -= 1500;
            foreach (MineScript mine in mineList)
            {
                if(mine.mineSpeed >=0.2)
                {
                    mine.mineSpeed -= 0.2f;
                }
            }
            mineElctricityUsage += 5;
        }
    }
    public void MiningAmountUpgrade()
    {
        if(gaseVal>=1000)
        {
            gaseVal -= 1000;
            foreach (MineScript mine in mineList)
            {
                mine.miningAmount += 5;
            }
        }
    }

}
