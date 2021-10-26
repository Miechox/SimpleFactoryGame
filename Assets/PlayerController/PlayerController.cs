using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
   
    [SerializeField]
    private Camera cam;
    Ray ray;
    RaycastHit hit;
    [SerializeField]
    private LayerMask mine;
    [SerializeField]
    private LayerMask Hub;
    [SerializeField]
    private LayerMask pole;
    [SerializeField]
    private LayerMask generator;
    [SerializeField]
    private LayerMask upgradeCenter;

    [SerializeField]
    GameObject ElectricPole;
    [SerializeField]
    GameObject Generatro;
    [SerializeField]
    GameObject MinePrefab;
    [SerializeField]
    GameObject UpgradeCenterPrefab;

    [SerializeField]
    private Canvas hubUI;
    [SerializeField]
    private Canvas mineUI;
    [SerializeField]
    private Canvas poleUI;
    [SerializeField]
    private Canvas generatorUI;
    [SerializeField]
    private Canvas UpgradeUI;
    GameObject clicked;

    public Vector3 hubPosition;
    public HubScript hubScript;

    GameObject heldObject;
   
    public GridPrototype grid;
    private GameMode gameMode;
  

    void Start()
    {
        hubUI.enabled = false;
        generatorUI.enabled = false;
        poleUI.enabled = false;
        mineUI.enabled = false;
        UpgradeUI.enabled = false;
        gameMode = FindObjectOfType<GameMode>().GetComponent<GameMode>();
    }

    private void Update()
    {
       if(heldObject!=null)
            HoldObjOnMouse(heldObject);

        CheckClickedBuilding();
    }


    private void HoldObjOnMouse(GameObject obj)
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hit,Mathf.Infinity))
        {
            obj.transform.position = hit.collider.gameObject.transform.position;
            if (Input.GetMouseButtonUp(1))
                SetPointToStay(obj, hit.collider.gameObject.transform.position);
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(heldObject.gameObject);
                heldObject = null;
            }
        }
        
    }
    private void SetPointToStay(GameObject obj,Vector3 terrein)
    {
        for (int i = 0; i < grid.gridInfo.Count; i++)
        {
            if (terrein == grid.gridInfo[i].gridPoints && !grid.gridInfo[i].occupiedBy)
            {
                grid.gridInfo[i].occupiedBy = obj;
                this.heldObject = null;
                if (obj.GetComponent<ElectricPole>())
                    obj.GetComponent<ElectricPole>().SetElectricField();
                else if (obj.GetComponent<Generator>())
                    obj.GetComponent<Generator>().GeneratorSetup();
                else if (obj.GetComponent<MineScript>())
                {
                    obj.GetComponent<MineScript>().FindeResource();
                    obj.GetComponent<MineScript>().EnergySum();
                }  
            }
        }
    }

    public void SpawUpgradeCenterInWorld()
    {
        if (heldObject == null && gameMode.goldVal >= 130 && !FindObjectOfType<UpCenter>())
        {
            gameMode.goldVal -= 130;
            heldObject = Instantiate(UpgradeCenterPrefab, new Vector3(20, 0, 20), Quaternion.identity);
        }
    }
    public void SpawnMineInWorld()
    {
        if (heldObject == null && gameMode.goldVal>=30)
        {
            gameMode.goldVal -= 30;
            heldObject = Instantiate(MinePrefab, new Vector3(20, 0, 20), Quaternion.identity);
        }   
    }
    public void SpawnElectricPoleInWorld()
    {
        if (heldObject == null && gameMode.goldVal >= 50)
        {
            gameMode.goldVal -= 50;
            heldObject = Instantiate(ElectricPole, new Vector3(20, 0, 20), Quaternion.identity);
        }   
    }
    public void SpawnGeneratorInWorld()
    {
        if (heldObject == null && gameMode.goldVal >= 100)
        {
            gameMode.goldVal -= 100;
            heldObject = Instantiate(Generatro, new Vector3(20, 0, 20), Quaternion.identity);
        }   
    }

    private void CheckClickedBuilding()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Hub))
            {
                clicked = hit.collider.gameObject;
                hubUI.enabled = true;
                UpgradeUI.enabled = false;
                poleUI.enabled = false;
                mineUI.enabled = false;
            }
            else if(Physics.Raycast(ray, out hit, Mathf.Infinity, mine))
            {
                clicked = hit.collider.gameObject;
                mineUI.enabled = true;
                UpgradeUI.enabled = false;
                hubUI.enabled = false;
                generatorUI.enabled = false;
                poleUI.enabled = false;
            }   
            else if(Physics.Raycast(ray, out hit, Mathf.Infinity, generator))
            {
                clicked = hit.collider.gameObject;
                generatorUI.enabled = true;
                UpgradeUI.enabled = false;
                poleUI.enabled = false;
                mineUI.enabled = false;
                hubUI.enabled = false;
            }  
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, pole))
            {
                clicked = hit.collider.gameObject;
                poleUI.enabled = true;
                UpgradeUI.enabled = false;
                generatorUI.enabled = false;
                mineUI.enabled = false;
                hubUI.enabled = false;
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, upgradeCenter))
            {
                clicked = hit.collider.gameObject;
                UpgradeUI.enabled = true;
                generatorUI.enabled = false;
                poleUI.enabled = false;
                mineUI.enabled = false;
                hubUI.enabled = false;
            }
            else
            {
                hubUI.enabled = false;
                generatorUI.enabled = false;
                poleUI.enabled = false;
                mineUI.enabled = false;
                UpgradeUI.enabled = false;
            }
        }
    }
    public void DestroyObj()
    {
        if(clicked)
        {
            Destroy(clicked);
        }
    }
}
