using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Coridor
{
    [SerializeField] int from = -1;
    [SerializeField] int to = -1;
    public int ConnectedFrom { get { return from; } set { from = value; } }
    public int ConnectedTo { get { return to; } set { to = value; } }
    int width = 2;
    public void InitCoridor(int id1, int id2)
    {
        ConnectedFrom = id1;
        ConnectedTo = id2;
    }

    public bool Equality(Coridor obj)
    {
        if (obj == null) return false;
        bool firstCheck = ConnectedFrom == obj.ConnectedFrom && ConnectedTo == obj.ConnectedTo;
        bool secondCheck = ConnectedFrom == obj.ConnectedTo && ConnectedTo == obj.ConnectedFrom;
        return firstCheck || secondCheck;
    }
}
