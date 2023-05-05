using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class CheckCheese : Bnode
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
                _children = new Bnode[] { new SeeCheese(), new CheckMold(), new EatCheeseAction() };
            }

            if (_children[0].execute() == BHnodeStates.SUCCESS)
            {
                if (_children[1].execute() == BHnodeStates.SUCCESS)
                {
                    if (_children[2].execute() == BHnodeStates.SUCCESS)
                    {
                        Debug.Log("Checked the cheese...");
                        return BHnodeStates.SUCCESS;
                    }
                    else
                    {
                        Debug.Log("Cheese not good");
                        return BHnodeStates.FAILED;
                    }
                }
                else
                {
                    Debug.Log("Cheese not good");
                    return BHnodeStates.FAILED;
                }
            }
            else
            {
                Debug.Log("Cheese not good");
                return BHnodeStates.FAILED;
            }
        }
    }
}
