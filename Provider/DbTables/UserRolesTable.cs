using CustomProvider.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomProvider.Provider
{
    public class UserRolesTable
    {
        private readonly AppDbContext _context;

        public UserRolesTable(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IdentityResult> DeleteAsync(UserRole userRole)
        {
            _context.UserRoles.Remove(userRole);


            if (await _context.SaveChangesAsync() > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not delete {userRole.Role.Name} role for {userRole.User.Email}." });
        }

        public async Task<IdentityResult> UpdateAsync(UserRole userRole)
        {
            _context.UserRoles.Update(userRole);


            if (await _context.SaveChangesAsync() > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not update {userRole.Role.Name} role for {userRole.User.Email}." });
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string roleName)
        {
            var role = await _context.Roles.SingleOrDefaultAsync(u => u.Name == roleName);

            if (role == null) {
                return IdentityResult.Failed(new IdentityError { Description = $"Could not find {roleName} role" });
            }

            var userRole = new UserRole();

            userRole.UserId = user.Id;
            userRole.RoleId = role.Id;

            _context.UserRoles.Add(userRole);

            if (await _context.SaveChangesAsync() > 0) {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError { Description = $"Could not add {roleName} role for {user.Email}." });
        }

        public async Task<IList<string>> GetRolesByUserId(User user)
        {

            var userRoles = await _context.UserRoles.Where(u => u.UserId == user.Id).ToListAsync();

            List<string> roleNames = new List<string>();

            foreach (UserRole userRole in userRoles)
            {
                roleNames.Add(userRole.Role.Name);
            }

            return roleNames;
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName)
        {

            var userRoles = await _context.UserRoles.Where(u => u.Role.Name == roleName).ToListAsync();

            List<User> users = new List<User>();

            foreach (UserRole userRole in userRoles)
            {
                users.Add(userRole.User);
            }

            return users;

        }

        public async Task<bool> IsInRoleAsync(User user, string roleName) 
        {

            var userRole = await _context.UserRoles.Where(u => u.UserId == user.Id && u.Role.Name == roleName).FirstOrDefaultAsync();

            return userRole != null;
        }

        public async Task RemoveFromRoleAsync(User user, string roleName) 
        {
            var userRole = await _context.UserRoles.Where(u => u.UserId == user.Id && u.Role.Name == roleName).FirstOrDefaultAsync();
            
            _context.UserRoles.Remove(userRole);

            await _context.SaveChangesAsync();
               
        }

    }
}
