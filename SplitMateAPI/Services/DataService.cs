using SplitMateAPI.Models;

namespace SplitMateAPI.Services
{
    public class DataService
    {
        private readonly List<Group> _groups = new List<Group>();

        public List<Group> GetAllGroups()
        {
            return _groups;
        }

        public Group? GetGroupById(string groupId)
        {
            return _groups.FirstOrDefault(g => g.GroupId == groupId);
        }

        public Group CreateGroup(string groupName)
        {
            var group = new Group
            {
                GroupId = Guid.NewGuid().ToString(),
                GroupName = groupName,
                Members = new List<string>(),
                Expenses = new List<Expense>(),
                Settlements = new List<Settlement>()
            };
            _groups.Add(group);
            return group;
        }

        public bool DeleteGroup(string groupId)
        {
            var group = GetGroupById(groupId);
            if (group != null)
            {
                _groups.Remove(group);
                return true;
            }
            return false;
        }

        public bool AddMember(string groupId, string memberName)
        {
            var group = GetGroupById(groupId);
            if (group != null && !group.Members.Contains(memberName))
            {
                group.Members.Add(memberName);
                return true;
            }
            return false;
        }

        public bool RemoveMember(string groupId, string memberName)
        {
            var group = GetGroupById(groupId);
            if (group != null)
            {
                return group.Members.Remove(memberName);
            }
            return false;
        }

        public Expense? AddExpense(string groupId, Expense expense)
        {
            var group = GetGroupById(groupId);
            if (group != null)
            {
                expense.ExpenseId = Guid.NewGuid().ToString();
                expense.Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                group.Expenses.Add(expense);
                return expense;
            }
            return null;
        }

        public bool DeleteExpense(string groupId, string expenseId)
        {
            var group = GetGroupById(groupId);
            if (group != null)
            {
                var expense = group.Expenses.FirstOrDefault(e => e.ExpenseId == expenseId);
                if (expense != null)
                {
                    group.Expenses.Remove(expense);
                    return true;
                }
            }
            return false;
        }

        public Settlement? AddSettlement(string groupId, Settlement settlement)
        {
            var group = GetGroupById(groupId);
            if (group != null)
            {
                settlement.Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                settlement.Completed = true;
                group.Settlements.Add(settlement);
                return settlement;
            }
            return null;
        }
    }
}
