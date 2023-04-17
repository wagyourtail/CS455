using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{
    public GameObject target;
    public MeshRenderer floor;
    public Material winMaterial;
    public Material loseMaterial;
    
    public float moveSpeed = 1f;

    private Vector3 start;
    
    private void Start()
    {
        start = transform.position;
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-3f, 3f), .5f, Random.Range(-3f, 3f));
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Vector3 tp = new Vector3(Random.Range(-3f, 3f), .5f, Random.Range(-3f, 3f));
        // ensure not in eachother
        while (Vector3.Distance(transform.position, tp) < 1f)
        {
            tp = new Vector3(Random.Range(-3f, 3f), .5f, Random.Range(-3f, 3f));
        }
        target.transform.localPosition = tp;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.transform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        
        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        // reward faster behaviors
        AddReward((MaxStep - StepCount) / (float) MaxStep);
        Debug.Log("Reward: " + ((MaxStep - StepCount) / (float) MaxStep));
        EndEpisode();
        floor.material = winMaterial;
    }

    private void Update()
    {
        if (transform.localPosition.y < .1f)
        {
            SetReward(-1f);
            EndEpisode();
            floor.material = loseMaterial;
        }
    }
}
