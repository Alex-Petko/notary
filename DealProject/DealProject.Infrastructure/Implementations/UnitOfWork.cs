﻿using Shared.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace DealProject.Infrastructure;

[ExcludeFromCodeCoverage]
internal sealed class UnitOfWork : UnitOfWorkBase, IUnitOfWork
{
    public IDebtRepository Debts { get; }

    public UnitOfWork(DealContext context) : base(context)
    {
        Debts = new DebtRepository(context);
    }
}