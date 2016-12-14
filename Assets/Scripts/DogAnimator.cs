using UnityEngine;
using System.Collections;
using Spine.Unity;

public class DogAnimator : MonoBehaviour
{
    private SkeletonAnimation skeletonAnim;

    void Awake()
    {
        skeletonAnim = GetComponent<SkeletonAnimation>();
    }

    public void OpenEyeAndShowTeeth() {
        skeletonAnim.AnimationState.SetAnimation(0, "openEye", false);
    }

    public void ChewShoe()
    {
        skeletonAnim.AnimationState.SetAnimation(0, "chewShoe", true);
    }
}
