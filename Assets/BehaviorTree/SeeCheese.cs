using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class SeeCheese : Bnode
    {
        public override Bnode[] children { get; set; }
        

        public override BHnodeStates execute()
        {
            if (Random.Range(0, 2) == 1)
            {
                Debug.Log("I SEE CHEESE RAAAAAAAAAH");
                return BHnodeStates.SUCCESS;
            }
            else
            {
                Debug.Log("I DONT SEE CHEEEESE");
                return BHnodeStates.FAILED;
            }
            
  
        }
    }

}
