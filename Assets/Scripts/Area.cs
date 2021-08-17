using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uRandom = UnityEngine.Random;

[System.Serializable]
public class Area
{
    static Vector2 minSizeForDivide = new Vector2(21, 21);
    public int Square { get; private set; }
    public bool IsRoot { get; private set; } = false;
    public bool CanBeDivided { get; set; } = true;
    public bool WasDivided { get; set; } = false;
    int noDividePercent = 100;
    Vector2Int startPosition;
    Vector2Int endPosition;
    Vector2Int size;
    Room roomInArea = null;
    int randomConstraintsMulitiplier = 9;
    int divideChanceReducer = 3;

    public void InitArea(Vector2Int startPoint, Vector2Int endPoint, int chanceToNotDivide )
    {
        if (chanceToNotDivide == 100) IsRoot = true;
        noDividePercent = chanceToNotDivide;
        startPosition = startPoint;
        endPosition = endPoint;
        size = endPosition - startPoint;
        Square = size.x * size.y; 
        CanBeDivided = CheckSizeForDivide() && CheckChanceToNotDivide(noDividePercent);
    }

    private bool CheckChanceToNotDivide(int border)
    {
        int percentage = uRandom.Range(0, 101);
        return percentage <= border;
    }

    private bool CheckSizeForDivide()
    {
        return size.x >= minSizeForDivide.x && size.y >= minSizeForDivide.y;
    }

    public Area[] SplitArea()
    {
        Vector2Int start = Vector2Int.zero;
        Vector2Int end = Vector2Int.zero;
        if (size.y >= size.x)
        {
            start.x = startPosition.x;
            end.x = startPosition.x + size.x;
            int half = (startPosition.y + endPosition.y) / 2;
            start.y = uRandom.Range(half-size.y/ randomConstraintsMulitiplier, half + size.y / randomConstraintsMulitiplier);
            end.y = start.y;
        }
        else
        {
            start.y = startPosition.y;
            end.y = startPosition.y + size.y;
            int half = (startPosition.x + endPosition.x) / 2;
            start.x = uRandom.Range(half - size.x / randomConstraintsMulitiplier, half + size.x / randomConstraintsMulitiplier);
            end.x = start.x;
        }
        Vector3 startLine = new Vector3(start.x, start.y, 0);
        Vector3 endLine = new Vector3(end.x, end.y, 0);
        Debug.DrawLine(startLine, endLine, Color.red, 50000f);
        Area child1 = new Area();
        Area child2 = new Area();
        child1.InitArea(startPosition, end, noDividePercent - divideChanceReducer);
        child2.InitArea(start, endPosition, noDividePercent - divideChanceReducer);
        return new Area[] { child1, child2 };
    }

    public Room CreateRoom()
    {
        roomInArea = new Room();
        roomInArea.InitRoom(startPosition,endPosition);
        return roomInArea;
    }
}
