using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Leave : Bnode
    {
        public override Bnode[] children { get; set; }

        public override BHnodeStates execute()
        {
            Debug.Log("hmm... ill leave. not feeling... cheesy.");
            return BHnodeStates.SUCCESS;
        }
    }

}