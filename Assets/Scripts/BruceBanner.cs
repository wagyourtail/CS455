using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruceBanner : MonoBehaviour
{
    public Door theDoor;
    public GameObject theTreasure;
    public GameObject test;
    bool executingBehavior = false;
    Task myCurrentTask;

    private void Start()
    {
        executingBehavior = false;
        this.GetComponent<Kinematic>().myTarget = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!executingBehavior)
            {
                executingBehavior = true;
                myCurrentTask = BuildTask_GetTreasue();

                EventBus.StartListening(myCurrentTask.TaskFinished, OnTaskFinished);
                myCurrentTask.run();
            }
        }
    }

    void OnTaskFinished()
    {
        EventBus.StopListening(myCurrentTask.TaskFinished, OnTaskFinished);
        //Debug.Log("Behavior complete! Success = " + myCurrentTask.succeeded);
        executingBehavior = false;
    }
    
    Task BuildTask_GetTreasue()
    {
        Debug.Log("Building behavior tree");
        // create our behavior tree based on Millington pg. 344
        // building from the bottom up
        List<Task> taskList = new List<Task>();

        // if door isn't locked, open it
        Task isDoorNotLocked = new IsFalse(theDoor.isLocked);
        Task waitABeat = new Wait(0.5f);
        Task openDoor = new OpenDoor(theDoor);
        taskList.Add(isDoorNotLocked);
        taskList.Add(waitABeat);
        taskList.Add(openDoor);
        Sequence openUnlockedDoor = new Sequence(taskList);

        // barge a closed door
        taskList = new List<Task>();
        Task isDoorClosed = new IsTrue(theDoor.isClosed);
        Task hulkOut = new HulkOut(this.gameObject);
        Task bargeDoor = new BargeDoor(theDoor.transform.GetChild(0).GetComponent<Rigidbody>());
        taskList.Add(isDoorClosed);
        taskList.Add(waitABeat);
        taskList.Add(hulkOut);
        taskList.Add(waitABeat);
        taskList.Add(bargeDoor);
        Sequence bargeClosedDoor = new Sequence(taskList);

        // open a closed door, one way or another
        taskList = new List<Task>();
        taskList.Add(openUnlockedDoor);
        taskList.Add(bargeClosedDoor);
        Selector openTheDoor = new Selector(taskList);

        // get the treasure when the door is closed
        taskList = new List<Task>();
        Task moveToDoor = new MoveKinematicToObject(this.GetComponent<Kinematic>(), theDoor.gameObject);
        Task moveToTreasure = new MoveKinematicToObject(this.GetComponent<Kinematic>(), theTreasure.gameObject);
        taskList.Add(moveToDoor);
        taskList.Add(waitABeat);
        taskList.Add(openTheDoor); // one way or another
        taskList.Add(waitABeat);
        taskList.Add(moveToTreasure);
        Sequence getTreasureBehindClosedDoor = new Sequence(taskList);

        // get the treasure when the door is open 
        taskList = new List<Task>();
        Task isDoorOpen = new IsFalse(theDoor.isClosed);
        taskList.Add(isDoorOpen);
        taskList.Add(moveToTreasure);
        Sequence getTreasureBehindOpenDoor = new Sequence(taskList);

        // detect if treasure will explode
        taskList = new List<Task>();
        Task isTreasureExplody = new IsTrue(theTreasure.GetComponent<Treasure>().willExplode);
        Task runAway = new MoveKinematicAwayFromObject(this.GetComponent<Kinematic>(), theTreasure.gameObject, 10);
        taskList.Add(isTreasureExplody);
        taskList.Add(waitABeat);
        taskList.Add(runAway);
        Sequence runAwayFromExplodyTreasure = new Sequence(taskList);
        
        // detect if the treasure is hostile
        // if so attack it
        taskList = new List<Task>();
        Task isTreasureHostile = new IsTrue(theTreasure.GetComponent<Treasure>().isHostile);
        Task attackTreasure = new AttackObject(this.gameObject, theTreasure.gameObject);
        taskList.Add(isTreasureHostile);
        taskList.Add(waitABeat);
        taskList.Add(attackTreasure);
        Sequence attackHostileTreasure = new Sequence(taskList);

        // if treasure isn't hostile, collect it
        taskList = new List<Task>();
        Task isTreasureNotExplody = new IsFalse(theTreasure.GetComponent<Treasure>().willExplode);
        Task isTreasureNotHostile = new IsFalse(theTreasure.GetComponent<Treasure>().isHostile);
        Task collectTreasure = new RemoveTarget(this.gameObject, theTreasure.gameObject);
        taskList.Add(isTreasureNotExplody);
        taskList.Add(isTreasureNotHostile);
        taskList.Add(waitABeat);
        taskList.Add(collectTreasure);
        Sequence collectNonHostileTreasure = new Sequence(taskList);

        // get to Treasure
        taskList = new List<Task>();
        taskList.Add(getTreasureBehindOpenDoor);
        taskList.Add(getTreasureBehindClosedDoor);
        Selector getTreasure = new Selector(taskList);
        
        taskList = new List<Task>();
        taskList.Add(runAwayFromExplodyTreasure);
        taskList.Add(attackHostileTreasure);
        taskList.Add(collectNonHostileTreasure);
        Selector afterArrive = new Selector(taskList);
        
        taskList = new List<Task>();
        taskList.Add(getTreasure);
        taskList.Add(afterArrive);
        Sequence root = new Sequence(taskList);
        
        return root;
    }
}
