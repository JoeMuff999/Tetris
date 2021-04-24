using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TetraminoName{
    LongBoi, Bendy, T, TwoBlock
};

public struct Tetramino{
    public TetraminoName name;
    public bool[,] grid;

    public Color color;

    public Vector2Int center;
    public Tetramino(TetraminoName _name, bool[,] _grid, Color _color, Vector2Int _center)
    {
        name = _name;
        grid = _grid; //all tetras can fit inside 2x4
        color = _color;
        center = _center;
    }
}

public static class TetraminoDefinitions{

    public static Dictionary<TetraminoName, Tetramino> DefineTetraminos()
    {
        Dictionary<TetraminoName, Tetramino> tetraminoMap = new Dictionary<TetraminoName, Tetramino>();        

        bool[,] longyGrid = new bool[1,4] {
            {true, true, true, true}
        };
        Tetramino longy = new Tetramino(TetraminoName.LongBoi, longyGrid, Color.red, new Vector2Int(0,2));
        
        tetraminoMap.Add(longy.name, longy);

        return tetraminoMap;
    }

}

