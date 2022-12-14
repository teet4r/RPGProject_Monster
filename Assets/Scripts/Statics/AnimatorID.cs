using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorID
{
    public class Int
    {
        public static readonly int Attack = Animator.StringToHash("Attack");
    }

    public class Bool
    {
        public static readonly int IsWalking = Animator.StringToHash("IsWalking");
    }

    public class Trigger
    {
        public static readonly int Hit = Animator.StringToHash("Hit");
        public static readonly int Die = Animator.StringToHash("Die");
    }
}
