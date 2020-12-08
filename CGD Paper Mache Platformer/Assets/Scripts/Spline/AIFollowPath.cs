using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollowPath : MonoBehaviour
{
    public enum MovementType
    {
        MoveTowards,
        LerpTowards
    }

    public MovementType Type = MovementType.MoveTowards;
    public MovementPath MyPath;
    public float Speed = 1;
    public float MaxDistanceToGoal = 0.1f;

    private IEnumerator<Transform> pointInPath;

	void Start ()
    {
		if(MyPath == null)
        {
            Debug.LogError("No Path Set", gameObject);
            return;
        }

        pointInPath = MyPath.GetNextPathPoint();
        pointInPath.MoveNext();

        if (pointInPath.Current == null)
        {
            Debug.LogError("No points", gameObject);
            return;
        }

        transform.position = pointInPath.Current.position;
	}

    void Update()
    {
        if (pointInPath == null || pointInPath.Current == null)
        {
            return;
        }

        if (Type == MovementType.MoveTowards)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * Speed);
        }
        else if (Type == MovementType.LerpTowards)
        {
            transform.position = Vector3.Lerp(transform.position, pointInPath.Current.position, Time.deltaTime * Speed);
        }

        var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
        {
            pointInPath.MoveNext();
        }
    }
}
