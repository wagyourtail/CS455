using System;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

namespace Behaviors.Look
{
    public class Align : BaseLookBehavior
    {
        public GameObject target;

        public Align(BaseKinematic character) : base(character)
        {
        }

        public override float? GetTargetAngle()
        {
            if (target == null)
            {
                return null;
            }
            return target.transform.eulerAngles.y;
        }
    }
}