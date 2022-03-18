# Factory Game
## Project is a prototype of factory builder game
The current and only goal is to improve the speed of gathering resources
but for the project to be more interesting to expand, it would be worth changing 
some stuff in code.

## Map
The entire map is generated from the perlin noise which is recreated
each time the game is restarted.

![](GitImage/MapGenIMage.PNG)
![](GitImage/MapGenIMageV3.PNG)

The grid system to which the objects on the map stick is created
with regular planes that have their own scripts.
It is currently a very limited grid system since
the map can only be the size
100 x 100 otherwise the code will not be
useful.


And this is what all the basic objects like trees and two kinds of resources are
inserted on the map depending on the color value between
0f and 1f which is pulled out
from the previously generated perlin noisa texture

```csharp
private void WorldObstaclePlacement()
{
    for (int x = 0; x < 100; x++)
    {
        for (int y = 0; y < 100; y++)
        {
            float perlVal = heightMap.texture.GetPixel(x, y).grayscale;

            if (perlVal > 0 && perlVal < 0.33f)
                LocationForObj(TreePrefab, x, y, true);
            else if (perlVal > 0.47 && perlVal < 0.48)
                LocationForObj(GasePrefab, x, y, false);
            else if (perlVal > 0.78 && perlVal < 0.80)
                LocationForObj(GoldPrefab, x, y, false);
        }
    }
}
```

## HUB
The player always starts at the location of the main hub

![](GitImage/HubImage.PNG)

Mainly through the main hub you can get to the UI
construction of buildings where, depending on the amount of gold, you can choose a building

![](GitImage/HubImageV2.PNG)

## Player Inputs

That the player has a visualization of where he places the object
under its cursor location this specific object is snapped.

```csharp
private void HoldObjOnMouse(GameObject obj)
{
    ray = cam.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
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
```
As you can see in the code above, if the player decides to place the object
we check if this point is empty for what we need input information.
The following function uses them to check what object is being held
and accordingly, perform the given actions that start when the object is
placed.

```csharp
private void SetPointToStay(GameObject heldObject, Vector3 planePosition);
```

Before placing an object, the player must choose what he wants
to put and he does it by buttons in the UI to which
there are pinned functions that can spawn chosen building. In every such
function, the program checks how much a player has of a given currency
and whether he is not "holding" something anymore.


```csharp
public void SpawnMineInWorld()
{
    if (heldObject == null && gameMode.goldVal >= 30)
    {
        gameMode.goldVal -= 30;
        heldObject = Instantiate(MinePrefab, new Vector3(20, 0, 20), Quaternion.identity);

    }
}
```







 
