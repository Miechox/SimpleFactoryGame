# Factory Game
## Projekt jest prototypem factory buildera
Aktualnym i jedynym celem jest ulepszanie prędkości zbierania surowców
ale aby projekt można było ciekawiej rozbudować warto byłoby zmienić parę
rzeczy w kodzie.
## Mapa
Cala mapa jest generowana na bazie Perlin noisa który jest tworzony na nowo
za każdym kolejnym właczeniem gry.



System gridu do którego się przyczepiają obiekty na mapie jest tworzony
za pomocą zwykłych plane'ów które maja swoje własne skrypty.
Aktualnie jest to bardzo ogrnaiczony grid system ponieważ
mapa może mieć tylko wielkość 
100 x 100 inaczej kod nie będzie
użyteczny.



Żeby gracz miał wizualizacje tego gdzie stawia obiekt
pod lokacją jego kursora jest snapowany 

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

Funkcja sprawdzająca czy dany blok jest zajęty wykorzystuję
Raycast dzięki któremu dostaje się do skryptu danego miejsca
i patrzy na bool określająćy stan zajęcia.



 
