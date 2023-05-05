using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class CheckIfHungry : Bnode
    {
        private Bnode[] _children;

        public override Bnode[] children
        {
            get { return _children; }
            set { _children = value; }
        }

        public override BHnodeStates execute()
        {

            if (_children == null || _children.Length == 0)
            {
                _children = new Bnode[] { new CheckCheese(), new Leave() };
            }

            int randomValue = Random.Range(0, 2);
            Debug.Log("The random value is: " + randomValue);
            if (randomValue == 1)
            {
                
                Debug.Log("I'm not hungry");
                return _children[1].execute();
            }
            else
            {
                Debug.Log("I'm hungry");
                return _children[0].execute();
            }
        }
    }
}
