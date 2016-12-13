using UnityEngine;
using System.Collections;
using Fungus;
using Spine.Unity;

public class Character : MonoBehaviour
{
    public Flowchart GameFlow;
    public SkeletonAnimation chimneySmoke;

    public static Character Instance { get; private set; }
    private SkeletonAnimation skeletonAnim;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            skeletonAnim = Instance.GetComponent<SkeletonAnimation>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AnimBurn()
    {
        if (skeletonAnim.AnimationName != "inFlames")
            skeletonAnim.AnimationState.SetAnimation(0, "inFlames", true);
    }

    public void AnimFall()
    {
        if (skeletonAnim.AnimationName != "fall")
            skeletonAnim.AnimationState.SetAnimation(0, "fall", true);
    }

    public void LandAndBurn()
    {
        skeletonAnim.AnimationState.SetAnimation(0, "fallIntoFlames", false);
        skeletonAnim.AnimationState.AddAnimation(0, "inFlames", true, 0);
    }

    public void LandAndStand()
    {
        skeletonAnim.AnimationState.SetAnimation(0, "hitGround", false);
        chimneySmoke.AnimationState.SetAnimation(0, "coal", false);
        skeletonAnim.AnimationState.AddAnimation(0, "idle", true, 0);
    }

    public void SlideAndElectrize()
    {
        skeletonAnim.AnimationState.SetAnimation(0, "slide", false);
        skeletonAnim.AnimationState.AddAnimation(0, "fallOnBack", false, 0);
        skeletonAnim.AnimationState.AddAnimation(0, "electrizeGround", true, 0.8f);
    }

    public void Die(DeathReason reason)
    {
        switch (reason)
        {
            case DeathReason.Fire:
                GameFlow.SendFungusMessage("DieFire");
                break;
            case DeathReason.Slip:
                break;
            case DeathReason.RoofElectricity:
                GameFlow.SendFungusMessage("SlideOnIce");
                break;
            case DeathReason.TreeElectricity:
                break;
            case DeathReason.Dog:
                break;
            default:
                break;
        }
    }

    public void Continue(ContinueReason reason)
    {
        switch (reason)
        {
            case ContinueReason.None:
                break;
            case ContinueReason.Chimney:
                GameFlow.SendFungusMessage("GoChimney");
                break;
            default:
                break;
        }
    }
}
