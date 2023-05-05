using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    public class EatCheeseAction : Bnode
    {
        public override Bnode[] children { get ; set ; }

        public override BHnodeStates execute()
        {
            Debug.Log(" * eats cheese * yum yum tum tum");
            return BHnodeStates.SUCCESS;
        }
    }

}
