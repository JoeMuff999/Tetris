using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Block currentBlock;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            currentBlock.MoveLeft();
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            currentBlock.MoveRight();
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            currentBlock.RotateRight();
        }
    }
}
