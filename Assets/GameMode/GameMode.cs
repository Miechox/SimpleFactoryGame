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
    public int treesVal { get; set; }//3
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
        goldVal = 200000;
    }

    
    void Update()
    {
        UpdateResourcesUI();
        EnergyCheck();
    }

    private void UpdateResourcesUI()
    {
        playerResourceUI[0].text = $"Uisng: {electricityUsed}";
        playerResourceUI[1].text = $"Max ele: {electricityMax}";
        playerResourceUI[2].text = $"Trees: {treesVal}";
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
        if(goldVal>=900)
        {
            generatorElectricityGenerated += 5;
            goldVal -= 900;
            electricityMax = generatorsList.Count * generatorElectricityGenerated;
        }
    }
    public void MineSpeedUpgrade()
    {
        if(goldVal>=1500)
        {
            goldVal -= 1500;
            foreach (MineScript mine in mineList)
            {
                mine.mineSpeed -= 0.5f;
            }
        }
    }
    public void MiningAmountUpgrade()
    {
        if(goldVal>=1000)
        {
            goldVal -= 1000;
            foreach (MineScript mine in mineList)
            {
                mine.miningAmount += 5;
            }
        }
    }


}
