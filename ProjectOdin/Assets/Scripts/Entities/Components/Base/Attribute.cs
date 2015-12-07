using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Attributes;

public class Attribute : BaseAttribute
{
    private List<RawBonus> RawBonuses;
    private List<FinalBonus> FinalBonuses;

    private bool Modified;

    private float p_FinalValue;

    public Attribute(int initialValue) : base(initialValue)
    {
        this.RawBonuses = new List<RawBonus>();
        this.FinalBonuses = new List<FinalBonus>();
        this.Modified = false;
        this.p_FinalValue = initialValue;
    }

    public void AddRawBonus(RawBonus bonus)
    {
        this.RawBonuses.Add(bonus);
        this.Modified = true;
    }

    public void AddFinalBonus(FinalBonus bonus)
    {
        this.FinalBonuses.Add(bonus);
        this.Modified = true;
        if (bonus.Duration != 0.0f)
        {
            StartCoroutine(RemoveBonusAfterDuration(bonus));
        }
    }

    public void RemoveRawbonus(RawBonus bonus)
    {
        if (RawBonuses.Contains(bonus))
        {
            this.RawBonuses.Remove(bonus);
            this.Modified = true;
        }
    }

    public void RemoveFinalBonus(FinalBonus bonus)
    {
        if (FinalBonuses.Contains(bonus))
        {
            this.FinalBonuses.Remove(bonus);
            this.Modified = true;
        }
    }

    IEnumerator RemoveBonusAfterDuration(FinalBonus bonus)
    {
        yield return StartCoroutine(this.WaitForDuration(bonus.Duration));
        this.RemoveFinalBonus(bonus);
    }

    public float FinalValue
    {
        get
        {
            if (this.Modified)
            {
                p_FinalValue = BaseValue;

                int rawBonusValue = 0;
                float rawBonusMultiplier = 0f;

                foreach (RawBonus bonus in this.RawBonuses)
                {
                    rawBonusValue += bonus.BaseValue;
                    rawBonusMultiplier += bonus.BaseMultiplier;
                }

                p_FinalValue += rawBonusValue;
                p_FinalValue *= rawBonusMultiplier;

                int finalBonusValue = 0;
                float finalBonusMultiplier = 0f;

                foreach (FinalBonus bonus in this.FinalBonuses)
                {
                    finalBonusValue += bonus.BaseValue;
                    finalBonusMultiplier += bonus.BaseMultiplier;
                }

                p_FinalValue += finalBonusValue;
                p_FinalValue *= finalBonusMultiplier;

            }

            return p_FinalValue;
        }
    }
}

