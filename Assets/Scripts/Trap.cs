using UnityEngine;
using System.Collections;

public enum DeathReason
{
    None,
    Fire,
    Lego,
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
    public GameObject externalTrigger = null;


    public bool IsDisabled { get; set; }

    public void UseOnSelf()
    {
        Disable();
    }

    public bool CanUseOnSelf(Item item)
    {
        foreach (var d in Disablers)
        {
            if (d == item.Name)
            {
                return true;
            }
        }
        return false;
    }

    public void Disable()
    {
        IsDisabled = true;
        if (externalTrigger != null) externalTrigger.SetActive(false);
    }

    void OnMouseDown()
    {
        if (GameController.Instance.IsInputEnabled)
            if (Clickable)
                Trigger();
    }

    void OnTriggerEnter2D(Collider2D other)
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
