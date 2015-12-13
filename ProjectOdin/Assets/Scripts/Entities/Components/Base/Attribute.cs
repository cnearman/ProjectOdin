using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Attributes;

public class inAttribute : BaseAttribute
{
    private List<RawBonus> RawBonuses;
    private List<FinalBonus> FinalBonuses;

    private bool Modified;

    private int p_FinalValue;
    private int p_MaxValue;

    public void Initialize(int initialValue, int maxValue = int.MaxValue)
    {
        this.RawBonuses = new List<RawBonus>();
        this.FinalBonuses = new List<FinalBonus>();
        this.Modified = false;
        this.p_FinalValue = initialValue;
        this.p_MaxValue = maxValue;
    }

    public void AddRawBonus(RawBonus bonus)
    {
        if (FinalValue + bonus.BaseValue > p_MaxValue)
        {
            bonus.BaseValue = p_MaxValue - FinalValue;
        }
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

    public int FinalValue
    {
        get
        {
            if (this.Modified)
            {
                float temp_finalValue = BaseValue;

                int rawBonusValue = 0;
                float rawBonusMultiplier = 0f;

                foreach (RawBonus bonus in this.RawBonuses)
                {
                    rawBonusValue += bonus.BaseValue;
                    rawBonusMultiplier += bonus.BaseMultiplier;
                }

                temp_finalValue += rawBonusValue;
                temp_finalValue *= rawBonusMultiplier;

                int finalBonusValue = 0;
                float finalBonusMultiplier = 0f;

                foreach (FinalBonus bonus in this.FinalBonuses)
                {
                    finalBonusValue += bonus.BaseValue;
                    finalBonusMultiplier += bonus.BaseMultiplier;
                }

                temp_finalValue += finalBonusValue;
                temp_finalValue *= finalBonusMultiplier;

                p_FinalValue = (int)temp_finalValue;
            }

            return p_FinalValue;
        }
    }
}

