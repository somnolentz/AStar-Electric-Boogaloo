using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public static class StartSequence 
    {
        public static void Execute()
        {
            Bnode start = new CheckIfHungry();
            start.execute();
        }
    }

}