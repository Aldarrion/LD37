using UnityEngine;
using System.Collections;

public enum DeathReason
{
    Fire,
    Slip,
    RoofElectricity,
    TreeElectricity,
    Dog
}

public enum ContinueReason
{
    None,
    Chimney
}

public class Trap : MonoBehaviour
{
    public bool Clickable;
    public DeathReason DieReason;
    public ContinueReason ContinueReason;
    public string[] Disablers;


    public bool IsDisabled { get; set; }

    public bool UseOnSelf(Item item)
    {
        foreach (var d in Disablers)
        {
            if (d == item.Name)
            {
                Disable();
                return true;
            }
        }
        return false;
    }

    public void Disable()
    {
        IsDisabled = true;
    }

    void OnMouseDown()
    {
        if (GameController.Instance.IsInputEnabled)
            if (Clickable)
                Trigger();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Character>() != null)
        {
            Trigger();
        }
    }

    public void Trigger()
    {
        if (!IsDisabled)
        {
            Character.Instance.Die(DieReason);
        }
        else
        {
            Character.Instance.Continue(ContinueReason);
        }
    }
}
