using CustomProvider.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;



namespace CustomProvider.Provider
{
    public class UsersTable
    {
        private readonly AppDbContext _context;

        public UsersTable(AppDbContext context)
        {
            _context = context;
        }

        #region createuser
        public async Task<IdentityResult> CreateAsync(User user)
        {

            _context.Users.Add(user);


            if ( await _context.SaveChangesAsync() > 0)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError { Description = $"Could not insert user {user.Email}." });
        }
        #endregion

        public async Task<IdentityResult> DeleteAsync(User user)
        {
            _context.Users.Remove(user);


            if ( await _context.SaveChangesAsync() > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not delete user {user.Email}." });
        }

        public async Task<IdentityResult> UpdateAsync(User user)
        {
            _context.Users.Update(user);


            if (await _context.SaveChangesAsync() > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not update user {user.Email}." });
        }


        public async Task<User> FindByIdAsync(Guid userId)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
        }


        public async Task<User> FindByNameAsync(string userName)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
