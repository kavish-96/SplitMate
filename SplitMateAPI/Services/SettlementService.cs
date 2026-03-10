using SplitMateAPI.Models;

namespace SplitMateAPI.Services
{
    public class SettlementService
    {
        private readonly BalanceService _balanceService;

        public SettlementService(BalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        public List<Transaction> CalculateOptimalSettlements(Group group)
        {
            var balances = _balanceService.CalculateBalances(group);
            var transactions = new List<Transaction>();

            // Separate creditors and debtors
            var creditors = balances
                .Where(b => b.NetBalance > 0.01m)
                .OrderByDescending(b => b.NetBalance)
                .Select(b => new { Member = b.Member, Amount = b.NetBalance })
                .ToList();

            var debtors = balances
                .Where(b => b.NetBalance < -0.01m)
                .OrderBy(b => b.NetBalance)
                .Select(b => new { Member = b.Member, Amount = Math.Abs(b.NetBalance) })
                .ToList();

            int i = 0, j = 0;

            while (i < creditors.Count && j < debtors.Count)
            {
                var creditor = creditors[i];
                var debtor = debtors[j];

                var settlementAmount = Math.Min(creditor.Amount, debtor.Amount);

                transactions.Add(new Transaction
                {
                    From = debtor.Member,
                    To = creditor.Member,
                    Amount = Math.Round(settlementAmount, 2)
                });

                creditors[i] = new { creditor.Member, Amount = creditor.Amount - settlementAmount };
                debtors[j] = new { debtor.Member, Amount = debtor.Amount - settlementAmount };

                if (creditors[i].Amount < 0.01m) i++;
                if (debtors[j].Amount < 0.01m) j++;
            }

            return transactions;
        }
    }
}
