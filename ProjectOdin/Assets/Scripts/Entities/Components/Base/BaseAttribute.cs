using UnityEngine;

namespace Attributes
{
    public class FinalBonus : BaseAttribute
    {
        public float Duration { get; internal set; }

        public FinalBonus() : base() { }

        public FinalBonus(int baseValue, float duration = 0.0f) : base(baseValue)
        {
            this.Duration = duration;
        }

        public FinalBonus(int baseValue, float baseMultiplier, float duration = 0.0f) : base(baseValue, baseMultiplier)
        {
            this.Duration = duration;
        }

    }

    public class RawBonus : BaseAttribute
    {
        public RawBonus() : base() {}

        public RawBonus(int baseValue) : base(baseValue) { }

        public RawBonus(int baseValue, float baseMultiplier) : base(baseValue, baseMultiplier) { }
    }

    public class BaseAttribute : BaseClass
    {
        public BaseAttribute() : this(0,0) { }

        public BaseAttribute(int baseValue) : this(baseValue, 0) { }

        public BaseAttribute(int baseValue, float baseMultiplier)
        {
            this.BaseValue = baseValue;
            this.BaseMultiplier = baseMultiplier;
        }

        public int BaseValue { get; internal set; }

        public float BaseMultiplier { get; internal set; }

    }
}