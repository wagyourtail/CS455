using UnityEngine;

namespace Behaviors.Move
{
    public class FollowPath : Seek
    {
        public Vector3[] path;
        public float pathOffset = 1f;
        
        public FollowPath(BaseKinematic character) : base(character)
        {
        }

        public override Vector3? GetTargetPosition()
        {
            if (path == null) return null;
            Vector3 charPos = character.transform.position;
            int currentParam = 0;
            float minDist = float.MaxValue;
            for (int i = 0; i < path.Length; ++i)
            {
                if (i > 0)
                {
                    Debug.DrawLine(path[i-1], path[i], Color.blue);
                }
                float dist = Vector3.Distance(charPos, path[i]);
                if (dist < minDist)
                {
                    minDist = dist;
                    currentParam = i;
                }
            }
            
            // target next point along path to the point we're closest to, this may result in corner cutting, and issues
            // if the path overlaps... maybe should store previous point and check if we're skipping...
            // or only use a subsection from previous point in previous check, ie. if we're at pos 3, only allow checking against [3:5]
            int next = currentParam + 1;
            if (next < path.Length)
            {
                currentParam += 1;
            }
            
            Vector3 target = path[currentParam];
            return target - character.transform.position;
        }
    }
}