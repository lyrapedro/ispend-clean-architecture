﻿namespace iSpend.Domain.Entities;

public sealed class Expense : Entity
{
    public string UserId { get; private set; }
    public int? CategoryId { get; private set; }
    public string Name { get; private set; }
    public decimal Value { get; private set; }
    public bool Recurrent { get; private set; }
    public int BillingDay { get; set; }

    public Category? Category { get; private set; }
}
