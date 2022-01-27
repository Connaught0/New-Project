using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contralor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    List<Block> creatMap(int x,int y)
    {
        List<Block> Map = new List<Block>(x*y);
        for(int i=0;i<x;i++)
        {
            for(int j=0;j<y;j++)
            {
                Map[j + i * x] = new Block(i, j);

            }
        }
        return Map;
    }
    class AstarBlock
    {
        Block block;
        float Step;//Just like g in the original algotithm
        float Distance;//Just like h in the original algotithm
        public List<Block> LeastRoad(Vector2 start,Vector2 end)
        {
            List<Block> road = new List<Block>();

            return road;
        }
    }
}
