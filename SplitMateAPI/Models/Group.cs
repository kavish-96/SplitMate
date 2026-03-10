namespace SplitMateAPI.Models
{
    public class Group
    {
        public string GroupId { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public List<string> Members { get; set; } = new List<string>();
        public List<Expense> Expenses { get; set; } = new List<Expense>();
        public List<Settlement> Settlements { get; set; } = new List<Settlement>();
    }

    public class Expense
    {
        public string ExpenseId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string PaidBy { get; set; } = string.Empty;
        public List<string> SplitAmong { get; set; } = new List<string>();
        public Dictionary<string, decimal> ContributionMap { get; set; } = new Dictionary<string, decimal>();
        public long Timestamp { get; set; }
    }

    public class Settlement
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public long Timestamp { get; set; }
        public bool Completed { get; set; }
    }

    public class Balance
    {
        public string Member { get; set; } = string.Empty;
        public decimal TotalPaid { get; set; }
        public decimal TotalOwed { get; set; }
        public decimal NetBalance { get; set; }
    }

    public class Transaction
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
