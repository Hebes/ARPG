using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("当随机概率低于成功概率时，随机概率任务将返回成功。否则它将返回失败" +
                     "The random probability task will return success when the random probability is below the succeed probability. It will otherwise return failure.")]
    public class RandomProbability : Conditional
    {
        [Tooltip("The chance that the task will return success")]
        public SharedFloat successProbability = 0.5f;
        [Tooltip("Seed the random number generator to make things easier to debug")]
        public SharedInt seed;
        [Tooltip("Do we want to use the seed?")]
        public SharedBool useSeed;

        public override void OnAwake()
        {
            // If specified, use the seed provided.
            if (useSeed.Value) {
                Random.InitState(seed.Value);
            }
        }

        public override TaskStatus OnUpdate()
        {
            // Return success if random value is less than the success probability. Otherwise return failure.
            float randomValue = Random.value;
            if (randomValue < successProbability.Value) {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }

        public override void OnReset()
        {
            // Reset the public properties back to their original values
            successProbability = 0.5f;
            seed = 0;
            useSeed = false;
        }
    }
}