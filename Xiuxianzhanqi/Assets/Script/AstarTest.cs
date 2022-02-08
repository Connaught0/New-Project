using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AstarTest : MonoBehaviour
{
    const int _blockX = 14;
    const int _blockY = 14;
    public Camera cam;
    List<List<Block>> Map;
    public GameObject block;
    public Canvas canvas;
    bool ClickFirst;
    public Vector2 start;
    public Vector2 end;

    // Start is called before the first frame update
    void Start()
    {

        creatrMap();
        ClickFirst = false;
        start = new Vector2(0, 0);
        end = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void creatrMap()
    {
        Map= new List<List<Block>>();

        for (int i = 0; i < _blockX; i++)
        {
            List<Block> mapl = new List<Block>();
            for (int j = 0; j < _blockY; j++)
            {

                
                GameObject bl = Instantiate(block);
                bl.transform.SetParent(canvas.transform);
                bl.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(150 * i, 150 * j,0);
                bl.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                bl.GetComponent<Block>().x = i + 1;
                bl.GetComponent<Block>().y = j + 1;
                bl.transform.Find("Text").GetComponent<Text>().text = (i + 1) + "," + (j + 1);
                bl.GetComponent<Button>().onClick.AddListener(delegate () { GetStart(bl.GetComponent<Button>().gameObject); });
                mapl.Add(bl.GetComponent<Block>());
            }
            Map.Add(mapl);
        }

    }


    class Astar
    {
        public Vector2 vector;
        public int G;
        public float H;
        public Astar Parent;
        public Astar(Vector2 v, int g, float h, Astar p)
        {
            this.vector = v;
            this.G = g;
            this.H = h;
            this.Parent = p;
        }
    }

    List<Vector2> AstarAlthgorithm(Vector2 s,Vector2 e, List<List<Block>> blocks)
    {
        List<Vector2> Road = new List<Vector2>();
        List<Astar> Openlist = new List<Astar>();
        List<Astar> Closelist = new List<Astar>();
        bool[,] AstarCheck = new bool[_blockX, _blockY] ;


        int g = 0;
        Openlist.Add(new Astar(s, g, Vector2.Distance(s, e),null));//Add the start position
        Vector2 now = s;
        Astar nowA = Openlist[0];
        List<Vector2> _4ve = new List<Vector2>() { Vector2.up, Vector2.down, Vector2.right, Vector2.left };
        while(now!=e)
        {
            g = Openlist[0].G+1;//The Moved Step base on the last step;
            //judge the four vector
            foreach(var ve in _4ve)
            {

                if ((Openlist[0].vector + ve).x >= 1 && (Openlist[0].vector + ve).x <= _blockX && (Openlist[0].vector + ve).y >= 1 && (Openlist[0].vector + ve).y <= _blockY)
                {
                    Debug.Log(Openlist[0].vector + ve);
                    if (!AstarCheck[(int)((Openlist[0].vector + ve).x-1), (int)((Openlist[0].vector + ve).y-1)])//if the vector has been checked, the value will be true
                    {
                        Openlist.Add(new Astar(Openlist[0].vector + ve, g, Vector2.Distance(Openlist[0].vector + ve, e), Openlist[0]));
                        AstarCheck[(int)((Openlist[0].vector + ve).x-1), (int)((Openlist[0].vector + ve).y-1)] = true;
                        
                    }
                }
            }
            //Add the min G+H to the closelist;
            Closelist.Add(Openlist[0]);
            Openlist.Remove(Openlist[0]);
            Openlist.Sort((x, y) => { return (x.G + x.H).CompareTo(y.G + y.H); });
            now = Openlist[0].vector;
            nowA = Openlist[0];
        }
        while(nowA!=null)
        {
            Road.Add(nowA.vector);
            nowA = nowA.Parent;
        }

        return Road;

    }
    public void GetStart(GameObject obj)
    {
        if (!ClickFirst)
            start = new Vector2(obj.GetComponent<Block>().x, obj.GetComponent<Block>().y);

        else
        {
            end = new Vector2(obj.GetComponent<Block>().x, obj.GetComponent<Block>().y);
            List<Vector2> Road = AstarAlthgorithm(start, end, Map);
            foreach(var r in Road)
            {
                Map[(int)r.x-1][(int)r.y-1].GetComponent<Image>().color = Color.red;
            }
        }
        ClickFirst = !ClickFirst;

    }
}
