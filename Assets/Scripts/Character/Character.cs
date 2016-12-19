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

    public Transform Chimney;

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
    
    public void AnimFall()
    {
        if (skeletonAnim.skeleton.flipX) 
            skeletonAnim.skeleton.FlipX = false;

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

    public void Electrize()
    {
        skeletonAnim.AnimationState.SetAnimation(0, "shocked", false);
        skeletonAnim.AnimationState.AddAnimation(0, "electrize", true, 0);
    }

    public void ScaredByDog()
    {
        skeletonAnim.AnimationState.SetAnimation(0, "shocked", false);
    }

    public void StepOnLego()
    {
        skeletonAnim.AnimationState.SetAnimation(0, "shocked", false);
    }

    public void JumpIntoChimney()
    {
        skeletonAnim.AnimationState.SetAnimation(0, "jump", false);
    }

    public void Smile()
    {
        skeletonAnim.AnimationState.SetAnimation(0, "smile", false);
    }

    public void Die(DeathReason reason)
    {
        switch (reason)
        {
            case DeathReason.Fire:
                SantaController.controller.ComeCloserToObj(Chimney.position, "DieFire");
                break;
            case DeathReason.Lego:
                GameFlow.SendFungusMessage("StepOnLego");
                break;
            case DeathReason.RoofElectricity:
                GameFlow.SendFungusMessage("SlideOnIce");
                break;
            case DeathReason.TreeElectricity:
                GameFlow.SendFungusMessage("StepOnCable");
                break;
            case DeathReason.Dog:
                GameFlow.SendFungusMessage("ScaredByDog");
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
                SantaController.controller.ComeCloserToObj(Chimney.position , "GoChimney");
                //GameFlow.SendFungusMessage("GoChimney");
                break;
            default:
                break;
        }
    }
}
