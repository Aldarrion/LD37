using UnityEngine;
using System.Collections;
using Spine.Unity;

public class KittenAnimator : MonoBehaviour {

    private SkeletonAnimation skeletonAnim;

    void Awake()
    {
        skeletonAnim = GetComponent<SkeletonAnimation>();
    }

    public void CatHat()
    {
        skeletonAnim.AnimationState.SetAnimation(0, "hat", false).timeScale = 0.6f;
    }

}
