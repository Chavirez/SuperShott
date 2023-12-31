using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ThrowPrediction : MonoBehaviour
{

    [SerializeField]
    private LineRenderer _lineRenderer;

    [SerializeField]
    [Range(3, 30)]
    private int _lineSegmentCount = 20;

    private List<Vector3> _linePoints = new List<Vector3>();
    private List<Vector3> _linePointss = new List<Vector3>();

    #region Singleton

    public static ThrowPrediction Instance;

    private void Awake()
    {

        Instance = this;

    }

    #endregion

    public void HideLine()
    {
        _lineRenderer.positionCount = 0;
    }

    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidBody, Vector3 startingPoint)
    {

        
        Vector3 velocity = (forceVector / rigidBody.mass) * Time.fixedDeltaTime;

        float FlightDuration = (2 * velocity.y) / Physics.gravity.y;

        float stepTime = FlightDuration / _lineSegmentCount;



        for (int i = 0; i < _lineSegmentCount; i++)
        {
            
            float stepTimePassed = stepTime * i;

            Vector3 MovementVector = new Vector3(

                x: velocity.x * stepTimePassed,
                y: velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                z: velocity.z * stepTimePassed

                );

            
            _linePoints.Add(item: -MovementVector + startingPoint);

        }

        ;
        _lineRenderer.positionCount = _linePoints.Count;
        
        _lineRenderer.SetPositions(_linePoints.ToArray());

    }

    
}
