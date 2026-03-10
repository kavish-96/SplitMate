using SplitMateAPI.Models;

namespace SplitMateAPI.Services
{
    public class BalanceService
    {
        public List<Balance> CalculateBalances(Group group)
        {
            var balanceMap = new Dictionary<string, Balance>();

            // Initialize balances for all members
            foreach (var member in group.Members)
            {
                balanceMap[member] = new Balance
                {
                    Member = member,
                    TotalPaid = 0,
                    TotalOwed = 0,
                    NetBalance = 0
                };
            }

            // Calculate from expenses
            foreach (var expense in group.Expenses)
            {
                var sharePerPerson = expense.Amount / expense.SplitAmong.Count;

                // Add contributions (what each person paid)
                foreach (var kvp in expense.ContributionMap)
                {
                    if (balanceMap.ContainsKey(kvp.Key))
                    {
                        balanceMap[kvp.Key].TotalPaid += kvp.Value;
                    }
                }

                // Add shares (what each person owes)
                foreach (var member in expense.SplitAmong)
                {
                    if (balanceMap.ContainsKey(member))
                    {
                        balanceMap[member].TotalOwed += sharePerPerson;
                    }
                }
            }

            // Calculate net balance from expenses
            foreach (var balance in balanceMap.Values)
            {
                balance.NetBalance = balance.TotalPaid - balance.TotalOwed;
            }

            // Adjust balances for completed settlements
            foreach (var settlement in group.Settlements.Where(s => s.Completed))
            {
                if (balanceMap.ContainsKey(settlement.From))
                {
                    balanceMap[settlement.From].NetBalance += settlement.Amount;
                }
                if (balanceMap.ContainsKey(settlement.To))
                {
                    balanceMap[settlement.To].NetBalance -= settlement.Amount;
                }
            }

            return balanceMap.Values.ToList();
        }
    }
}
