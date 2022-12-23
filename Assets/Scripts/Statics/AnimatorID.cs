using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorID
{
    public class Bool
    {
        public static readonly int IsWalking = Animator.StringToHash("IsWalking");
    }

    public class Trigger
    {
        public static readonly int Hit = Animator.StringToHash("Hit");
        public static readonly int Die = Animator.StringToHash("Die");

        public static readonly int[] Attacks = {
            Animator.StringToHash("Attack0"),
            Animator.StringToHash("Attack1"),
            Animator.StringToHash("Attack2"),
            Animator.StringToHash("Attack3")
        };
    }
}
