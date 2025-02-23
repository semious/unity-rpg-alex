using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Player player;
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

   public void AnimationTriggers()
    {
        player.AttackOver();
    }
}
