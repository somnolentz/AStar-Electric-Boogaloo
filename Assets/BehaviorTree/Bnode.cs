using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    public enum BHnodeStates
    {
        RUNNING,
        SUCCESS,
        FAILED
    }

    public abstract class Bnode
    {
        public abstract Bnode[] children { get; set; }

        public abstract BHnodeStates execute();

    }

}

