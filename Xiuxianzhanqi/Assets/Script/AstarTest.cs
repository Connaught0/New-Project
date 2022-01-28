using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AstarTest : MonoBehaviour
{
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

        for (int i = 0; i < 5; i++)
        {
            List<Block> mapl = new List<Block>();
            for (int j = 0; j < 5; j++)
            {
                GameObject bl = Instantiate(block, new Vector3(75 + i * 150, 75 + j * 150, 0), transform.rotation, canvas.transform);
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
        public Vector2 Parent;
        public Astar(Vector2 v, int g, float h, Vector2 p)
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
        float k = float.MaxValue;
        int g = 0;
        Openlist.Add(new Astar(s, g, Vector2.Distance(s, e),Vector2.zero));
        Vector2 now = s;

        while(now!=e)
        {
            g = Openlist[0].G+1;//The Moved Step base on the last step;
            //judge the four vector
            if((Openlist[0].vector+Vector2.down).y>=1)
            {
                Openlist.Add(new Astar(Openlist[0].vector + Vector2.down, g, Vector2.Distance(Openlist[0].vector + Vector2.down, e), Openlist[0].vector));
            }
            if ((Openlist[0].vector + Vector2.up).y <= 5)
            {
                Openlist.Add(new Astar(Openlist[0].vector + Vector2.up, g, Vector2.Distance(Openlist[0].vector + Vector2.up, e), Openlist[0].vector));
            }
            if ((Openlist[0].vector + Vector2.right).x <= 5)
            {
                Openlist.Add(new Astar(Openlist[0].vector + Vector2.right, g, Vector2.Distance(Openlist[0].vector + Vector2.right, e), Openlist[0].vector));
            }
            if ((Openlist[0].vector + Vector2.left).x >= 1)
            {
                Openlist.Add(new Astar(Openlist[0].vector + Vector2.left, g, Vector2.Distance(Openlist[0].vector + Vector2.left, e), Openlist[0].vector));
            }
            //Add the min G+H to the closelist;
            Closelist.Add(Openlist[0]);
            Openlist.Remove(Openlist[0]);
            Openlist.Sort((x, y) => { return (x.G + x.H).CompareTo(y.G + y.H); });
            now = Openlist[0].vector;
        }
        Debug.Log(Closelist);


        return Road;

    }
    public void GetStart(GameObject obj)
    {
        if (!ClickFirst)
            start = new Vector2(obj.GetComponent<Block>().x, obj.GetComponent<Block>().y);

        else
        {
            end = new Vector2(obj.GetComponent<Block>().x, obj.GetComponent<Block>().y);
            AstarAlthgorithm(start, end, Map);
        }
        ClickFirst = !ClickFirst;

    }
}
