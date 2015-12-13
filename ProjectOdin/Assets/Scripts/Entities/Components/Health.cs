class Health : inAttribute
{
    public int StartingHealth;

    public int MaxHealth;

    public int CurrentHealth;

    public bool IsDead
    {
        get
        {
            return CurrentHealth < 0;
        }
    }

    void Awake()
    {
        this.Initialize(StartingHealth);
    }

    public void ResetHealth()
    {
        this.CurrentHealth = this.StartingHealth;
    }

    public void AddDamage(int damage)
    {
        this.CurrentHealth -= damage;
    }

    public void RemoveDamage(int healthValue)
    {
        if(this.CurrentHealth + healthValue > this.MaxHealth)
        {
            this.CurrentHealth = MaxHealth;
        }
        else
        {
            this.CurrentHealth += healthValue;
        }
    }
}