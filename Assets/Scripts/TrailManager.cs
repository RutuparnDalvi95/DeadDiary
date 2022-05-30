using System.Collections;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    private TrailRenderer _renderer;
    private SerializedTrail _trail;
    private Vector3[] positions = new Vector3[10000];
    private int savedTrailPositions = 0;
    private int numOfTrailPositions = 0;
    
    void Awake()
    {
        _renderer = GetComponent<TrailRenderer>();
    }
    
    // we need to store the positions in a savedrawing object
    private void Start()
    {
        _trail = DrawingData.AddSerializedTrail();
        StartCoroutine(myCoroutine());
    }
    

    IEnumerator myCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            numOfTrailPositions = _renderer.GetPositions(positions);
            for (int i = savedTrailPositions; i < numOfTrailPositions; i++)
            {
                SerializedVector3 sv = new SerializedVector3(positions[i]);
                _trail.trail.Add(sv);
            }
            savedTrailPositions = numOfTrailPositions;
        }
        
    }
    
}
