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
    private LayerMask hub;
    [SerializeField]
    private LayerMask pole;
    [SerializeField]
    private LayerMask generator;
    [SerializeField]
    private LayerMask upgradeCenter;
    private LayerMask[] layerList = new LayerMask[5];

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
    private Canvas[] canvList = new Canvas[5];
    GameObject clicked;

    public HubScript hubScript;
    GameObject heldObject;
   
    public GridPrototype grid;
    private GameMode gameMode;

    CameraMovement camMove;
  

    void Start()
    {
        SetUpCanvasList();
        SetUpLayerMaskList();
        gameMode = FindObjectOfType<GameMode>().GetComponent<GameMode>();
        camMove = FindObjectOfType<CameraMovement>().GetComponent<CameraMovement>();
    }

    private void Update()
    {
       if(heldObject!=null)
            HoldObjOnMouse(heldObject);

        CheckClickedBuilding();
        SetCameraToHub();
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
    /*private bool something(GameObject obj,out ElectricPole temp)
    {
        temp = obj.GetComponent<ElectricPole>();
        return temp != null;
    }*/
    //electricPole.SetElectricField();
    //something(obj,out var electricPole)
  
    private void SetPointToStay(GameObject obj,Vector3 terrein)
    {
        for (int i = 0; i < grid.gridInfo.Count; i++)
        {
            if (terrein == grid.gridInfo[i].gridPoints && !grid.gridInfo[i].occupiedBy)
            {
                if (obj.GetComponent<ElectricPole>())
                {
                    obj.GetComponent<ElectricPole>().SetElectricField();
                    HeldObjectReset(i, obj);
                }
                else if (obj.GetComponent<Generator>())
                {
                    obj.GetComponent<Generator>().GeneratorSetup();
                    HeldObjectReset(i, obj);
                }  
                else if(obj.GetComponent<UpCenter>())
                {
                    HeldObjectReset(i, obj);
                }
            }
            else if(terrein == grid.gridInfo[i].gridPoints && grid.gridInfo[i].occupiedBy.GetComponent<ResourcesScript>())
            {
                if (obj.GetComponent<MineScript>())
                {
                    MineScript temp = obj.GetComponent<MineScript>();
                    temp.FindResource(i);
                    temp.EnergySum();
                    if(gameMode.mineList.Count>0)
                    {
                        temp.mineSpeed = gameMode.mineList[0].mineSpeed;
                        temp.miningAmount = gameMode.mineList[0].miningAmount;
                    }
                    HeldObjectReset(i,obj);
                }
            }
        }
    }
    private void HeldObjectReset(int i,GameObject obj)
    {
        grid.gridInfo[i].occupiedBy = obj;
        this.heldObject = null;
    }

    public void SpawnUpgradeCenterInWorld()
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
            heldObject = Instantiate(ElectricPole, new Vector3(20, 0, 20),Quaternion.identity);
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

    private void SetUpCanvasList()
    {
        canvList[0] = hubUI;
        canvList[1] = generatorUI;
        canvList[2] = poleUI;
        canvList[3] = mineUI;
        canvList[4] = UpgradeUI;
        foreach (Canvas can in canvList)
        {
            can.enabled = false;
        }
    }
    private void SetUpLayerMaskList()
    {
        layerList[0] = hub;
        layerList[1] = generator;
        layerList[2] = pole;
        layerList[3] = mine;
        layerList[4] = upgradeCenter;
    }
    private void MakeAllCanvasDisappear()
    {
        foreach (Canvas can in canvList)
        {
            can.enabled = false;
        }
    }
    private void ToggleUI(int index,Collider hit)
    {
        clicked = hit.gameObject;
        MakeAllCanvasDisappear();
        canvList[index].enabled = true;
    }
    private void CheckClickedBuilding()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            for (int i = 0; i < layerList.Length; i++)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerList[i]))
                {
                    ToggleUI(i, hit.collider);
                    break;
                }
                else
                    MakeAllCanvasDisappear();
            }
        }
    }

    private void SetCameraToHub()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            camMove.camPosition = new Vector3(hubScript.transform.position.x,
                                              camMove.camPosition.y,
                                              hubScript.transform.position.z - 6
                                              );
        }
    }
    public void DestroyObj()
    {
        if(clicked)
        {
            Destroy(clicked);
        }
    }
    public void DestroyPoleObj()
    {
        if (clicked)
        {
            ElectricPole temp = clicked.GetComponent<ElectricPole>();
            temp.DestroyElcetricField();
            Destroy(clicked);
        }
    }
}
