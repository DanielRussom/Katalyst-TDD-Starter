﻿namespace Katalyst_TDD_Starter.Bank
{
    public class AccountService : IAccountService
    {
        private readonly IStatementPrinter statementPrinter;
        private readonly ITimeGetter timeGetter;
        private readonly IStatementLog statementLog;
        private int currentBalance = 0;

        public AccountService(IStatementPrinter statementPrinter, ITimeGetter timeGetter, IStatementLog statementLog)
        {
            this.statementPrinter = statementPrinter;
            this.timeGetter = timeGetter;
            this.statementLog = statementLog;
        }

        public List<StatementEntry> StatementLog { get; set; } = new();

        public void Deposit(int amount)
        {
            statementLog.AddEntry(amount);
        }

        public void PrintStatement()
        {
            statementPrinter.PrintStatement(StatementLog);
        }

        public void Withdraw(int amount)
        {
            currentBalance -= amount;

            var statement = new StatementEntry
            {
                Amount = -amount,
                Balance = currentBalance,
                Timestamp = timeGetter.GetTime()
            };

            StatementLog.Add(statement);
        }
    }
}