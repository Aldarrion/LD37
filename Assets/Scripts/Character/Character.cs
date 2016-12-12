using UnityEngine;
using System.Collections;
using Fungus;
using Spine.Unity;

public class Character : MonoBehaviour
{
    public Flowchart GameFlow;

    public static Character Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void AnimBurn()
    {
        var skeletonAnim = Instance.GetComponent< SkeletonAnimation>();
        if (skeletonAnim.AnimationName != "inFlames")
            skeletonAnim.AnimationState.SetAnimation(0, "inFlames", true);
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
