using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Block
{
    // Vector2 oldPosition;
    Vector2Int position;
    Tetramino tetramino;

    Game.SquareDefinition[,] grid;

    public Block(Vector2Int _spawnPosition, Tetramino _tetramino, Game.SquareDefinition[,] _grid)
    {
        position = _spawnPosition;
        tetramino = _tetramino;
        grid = _grid;
        needToUpdate = true;
    }

    bool needToUpdate = false;


    public bool updateBoard(int timeStep)
    {
        if (needToUpdate)
        {
            needToUpdate = false;
            Vector2Int tetraminoIndex = new Vector2Int(0, 0);

            for (int row = position.x; row < position.x + tetramino.grid.GetLength(0); row++)
            {
                // Debug.Log(position.y + " " + tetramino.center.y);

                for (int col = position.y - tetramino.center.y; col < position.y + tetramino.center.y; col++)
                {
                    if (tetramino.grid[tetraminoIndex.x, tetraminoIndex.y])
                    {
                        grid[row, col].activeSquare = true;
                        grid[row, col].color = tetramino.color;
                        grid[row, col].containsBlock = true;
                        grid[row,col].timeStep = timeStep;
                    }
                    tetraminoIndex = new Vector2Int(tetraminoIndex.x, tetraminoIndex.y + 1);
                }
                tetraminoIndex = new Vector2Int(tetraminoIndex.x + 1, tetraminoIndex.y);

            }
            // for(int row = 0; row < grid.GetLength(0); row++)
            // {
            //     for(int col = 0; col < grid.GetLength(1); col++)
            //     {
            //         if(grid[row,col].activeSquare)
            //         {

            //         }
            //     }
            // }
            return true;
        }
        else
        {
            return false;
        }
    }
    public void RotateRight()
    {
        // transform.Rotate(new Vector3(0,0,90));
        needToUpdate = true;
    }

    private bool checkViolation(Vector2Int newPosition)
    {
        if (newPosition.x >= grid.GetLength(0) || newPosition.y - tetramino.center.y < 0 || newPosition.y + tetramino.center.y >= grid.GetLength(1))
        {
            return true;
        }
        Vector2Int tetraminoIndex = new Vector2Int(0, 0);
        for (int row = newPosition.x; row > newPosition.x - tetramino.grid.GetLength(0); row--)
        {
            for (int col = newPosition.y - tetramino.center.y; col < newPosition.y + tetramino.center.y; col++)
            {
                if(tetramino.grid[tetraminoIndex.x, tetraminoIndex.y])
                {
                    if(grid[row, col].containsBlock && grid[row,col].activeSquare == false)
                        return true;
                }
                tetraminoIndex = new Vector2Int(tetraminoIndex.x, tetraminoIndex.y + 1);
            }
            tetraminoIndex = new Vector2Int(tetraminoIndex.x + 1, tetraminoIndex.y);
        }
        return false;
    }

    public bool MoveDown()
    {
        //if you cant go down further, return false, else return true
        //this will let the game know that we should now switch to a new block
        if(checkViolation(new Vector2Int(position.x + 1, position.y)))
            return false;
        position += Vector2Int.right;
        needToUpdate = true;
        return true;
    }

    public void MoveLeft()
    {
        Debug.Log("move me left ");
        if(checkViolation(new Vector2Int(position.x, position.y-1)))
            return;
        position += Vector2Int.down;
        needToUpdate = true;

    }

    public void MoveRight()
    {
        Debug.Log("move me right");
        if(checkViolation(new Vector2Int(position.x, position.y+1)))
            return;
        position += Vector2Int.up;
        needToUpdate = true;

    }
}
