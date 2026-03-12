using Microsoft.EntityFrameworkCore;
using SplitMateAPI.Data;
using SplitMateAPI.Models;

namespace SplitMateAPI.Services
{
    public class DataService
    {
        private readonly AppDbContext _context;

        public DataService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Group>> GetAllGroupsAsync()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<Group?> GetGroupByIdAsync(string groupId)
        {
            return await _context.Groups.FirstOrDefaultAsync(g => g.GroupId == groupId);
        }

        public async Task<Group> CreateGroupAsync(string groupName)
        {
            var group = new Group
            {
                GroupId = Guid.NewGuid().ToString(),
                GroupName = groupName,
                Members = new List<string>(),
                Expenses = new List<Expense>(),
                Settlements = new List<Settlement>()
            };
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<bool> DeleteGroupAsync(string groupId)
        {
            var group = await GetGroupByIdAsync(groupId);
            if (group != null)
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> AddMemberAsync(string groupId, string memberName)
        {
            var group = await GetGroupByIdAsync(groupId);
            if (group != null && !group.Members.Contains(memberName))
            {
                group.Members.Add(memberName);
                _context.Groups.Update(group);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveMemberAsync(string groupId, string memberName)
        {
            var group = await GetGroupByIdAsync(groupId);
            if (group != null)
            {
                var removed = group.Members.Remove(memberName);
                if (removed)
                {
                    _context.Groups.Update(group);
                    await _context.SaveChangesAsync();
                }
                return removed;
            }
            return false;
        }

        public async Task<Expense?> AddExpenseAsync(string groupId, Expense expense)
        {
            var group = await GetGroupByIdAsync(groupId);
            if (group != null)
            {
                expense.ExpenseId = Guid.NewGuid().ToString();
                expense.Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                group.Expenses.Add(expense);
                _context.Groups.Update(group);
                await _context.SaveChangesAsync();
                return expense;
            }
            return null;
        }

        public async Task<bool> DeleteExpenseAsync(string groupId, string expenseId)
        {
            var group = await GetGroupByIdAsync(groupId);
            if (group != null)
            {
                var expense = group.Expenses.FirstOrDefault(e => e.ExpenseId == expenseId);
                if (expense != null)
                {
                    group.Expenses.Remove(expense);
                    _context.Groups.Update(group);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<Settlement?> AddSettlementAsync(string groupId, Settlement settlement)
        {
            var group = await GetGroupByIdAsync(groupId);
            if (group != null)
            {
                settlement.Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                settlement.Completed = true;
                group.Settlements.Add(settlement);
                _context.Groups.Update(group);
                await _context.SaveChangesAsync();
                return settlement;
            }
            return null;
        }
    }
}
