using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int x;
    public int y;
    int height;
    //int x;
    //int y;
    Block right;
    Block left;
    Block up;
    Block down;
    Object ob;
    Vector3 position;
    public Block()
    {

    }
    public Block(int _x,int _y)
    {
        this.x = _x;
        this.y = _y;
    }
    public Block GetNeibour(string position)
    {
        switch(position)
        {
            case "right":
                return right;
                
            case "left": return left;
            case "up": return up;
            case "down": return down;
        }
        return null;

    }

}
