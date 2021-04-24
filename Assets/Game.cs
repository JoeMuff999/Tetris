using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Game : MonoBehaviour
{

    public struct SquareDefinition{
        public bool containsBlock;
        public Color color;
        public bool activeSquare;

        public GameObject square;
        public int timeStep;
    };
    public float leftBound;
    // Start is called before the first frame update
    public float moveDistance;

    public GameObject SpawnPosition;

    public float fallInterval;

    public static Game Instance;

    public int gridHeight = 20;
    public int gridWidth = 10;

    public SquareDefinition[,] grid;

    public Vector2Int spawnPosition;
    public GameObject Unit;
    private Queue<TetraminoName> blocks;
    public Dictionary<TetraminoName, Tetramino> TetraminoMap = TetraminoDefinitions.DefineTetraminos();
    public Player player;
    void Start()
    {
        Instance = this;
        grid = new SquareDefinition[gridHeight, gridWidth];
        spawnPosition = new Vector2Int(0, gridWidth/2);
        bool[] arr = new bool[TetraminoMap.Count];
        blocks = new Queue<TetraminoName>();
        blocks.Enqueue(TetraminoName.LongBoi);
        // foreach(var key in TetraminoName)
        // {
        //     Begin:
        //     int rand = Random.Range(0, TetraminoMap.Count);
        //     if(arr[rand])
        //         goto Begin;
        //     else{
        //         arr[rand] = true;
        //         blocks.Enqueue(TetraminoMap[(TetraminoName) rand]);
        //     }
        // }
        SpawnBlock();
    }

    void SpawnBlock(){
        TetraminoName curr = blocks.Dequeue();
        Block you = new Block(spawnPosition, TetraminoMap[curr], grid);
        player.currentBlock = you;  
        StartCoroutine(blockDown());
    }
    private bool blockStillMoving = true;
    private int timeStep = 0;
    // Update is called once per frame
    void Update()
    {
        //update active block, check for complete lines, redraw
        if(!blockStillMoving)
        {
            //spawn new block
            //player.currentBlock = another new block
            
            // StartCoroutine(blockDown());
            blockStillMoving = true;
        }
        else{

            if(!player.currentBlock.updateBoard(timeStep))
                return;

            for(int row = 0; row < grid.GetLength(0); row++)
            {
                for(int col = 0; col < grid.GetLength(1); col++)
                {
                    if(grid[row,col].activeSquare)
                    {
                        // Debug.Log("updated");
                        if(grid[row,col].timeStep == timeStep)
                        {
                            if(grid[row,col].square != null)
                                continue;
                            GameObject go = Instantiate(Unit, new Vector2(col-.5f, gridHeight-row-.5f), new Quaternion(0,0,0,1));
                            go.GetComponent<SpriteRenderer>().color = grid[row,col].color;
                            grid[row,col].square = go;
                        }
                        else{
                            // Debug.Log("destroyed");
                            Destroy(grid[row,col].square);
                            grid[row,col].containsBlock = false;
                            grid[row,col].activeSquare = false;
                        }

                    }
                }
            }
            timeStep++;
        }
    }
    IEnumerator blockDown(){
        while(blockStillMoving)
        {
            float currTime = 0;
            while(currTime < fallInterval)
            {
                currTime += Time.deltaTime;
                yield return null;
            }
            //let player move block once its about to touch down
            if(!player.currentBlock.MoveDown())    
            {
                currTime = 0;
                while(currTime < fallInterval)
                {
                    currTime += Time.deltaTime;
                    yield return null;
                }
                blockStillMoving = false;
            }    
            
        }


    }
}
