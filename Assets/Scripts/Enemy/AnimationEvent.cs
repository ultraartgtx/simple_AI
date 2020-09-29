using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
   public delegate void HitEvent(); 
   public event HitEvent OnHit; 
   public void Hit()
   {
      if (OnHit != null) OnHit();
   }
   public void FootL()
   {
   }public void FootR()
   {
   }
}
