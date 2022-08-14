using UnityEngine;

public class Waypoints : MonoBehaviour
{
    // It creates a waypoints list with childs of Waypoints gameobject
    public static Transform[] waypoints;
    
    void Awake()
    {
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }
}
