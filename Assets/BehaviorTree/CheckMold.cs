using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class CheckMold : Bnode
    {
        public override Bnode[] children { get; set; }


        public override BHnodeStates execute()
        {
            if (Random.Range(0, 2) == 1)
            {
                Debug.Log("THE CHEESE ISNT MOLDY");
                return BHnodeStates.SUCCESS;
            }
            else
            {
                Debug.Log("EW ITS MOLDY");
                return BHnodeStates.FAILED;
            }


        }
    }

}