using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEnumerator<T> : IEnumerator {

    private Map<T> map;

    public MapEnumerator(Map<T> map){
        this.map = map;
                }

    public T Current{
        get{return Current;}
    }

    public void Dispose()
    {
        throw new System.NotImplementedException();
    }

    public bool MoveNext()
    {
        throw new System.NotImplementedException();
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }

}
