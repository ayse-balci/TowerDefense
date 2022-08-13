using UnityEngine;

public class Location
{
    public Transform transform;
    public bool isFull;

    public Location(Transform transform, bool isFull)
    {
        this.transform = transform;
        this.isFull = isFull;
    }

    public Transform GetTransform()
    {
        return transform;
    }
    
    public void SetTransform(Transform position)
    {
        this.transform = position;
    }
    
    public bool GetIsFull()
    {
        return isFull;
    }
    
    public void SetIsFull(bool isFull)
    {
        this.isFull = isFull;
    }
}
