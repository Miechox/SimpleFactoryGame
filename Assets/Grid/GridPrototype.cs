using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPrototype 
{
    private int height, width;
    public List<GridInfo> gridInfo = new List<GridInfo>();
    
    public GridPrototype(int height,int width)
    {
        this.height = height;
        this.width = width;

        for(int i = 0;i<this.height;i++)
        {
            for (int j = 0; j < this.width; j++)
            {
                gridInfo.Add(new GridInfo(i, j));
            }   
        }  
    }

}
