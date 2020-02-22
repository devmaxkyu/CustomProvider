using CustomProvider.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;



namespace CustomProvider.Data
{
    public class RolesTable
    {
        private readonly AppDbContext _context;

        public RolesTable(AppDbContext context)
        {
            _context = context;
        }

        #region createrole
        public async Task<IdentityResult> CreateAsync(Role role)
        {

            _context.Roles.Add(role);


            if (await _context.SaveChangesAsync() > 0)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError { Description = $"Could not insert role {role.Name}." });
        }
        #endregion

        public async Task<IdentityResult> DeleteAsync(Role role)
        {
            _context.Roles.Remove(role);


            if (await _context.SaveChangesAsync() > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not delete role {role.Name}." });
        }

        public async Task<IdentityResult> UpdateAsync(Role role)
        {
            _context.Roles.Update(role);


            if (await _context.SaveChangesAsync() > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Could not update role {role.Name}." });
        }


        public async Task<Role> FindByIdAsync(Guid roleId)
        {
            return await _context.Roles.SingleOrDefaultAsync(r => r.Id == roleId);
        }


        public async Task<Role> FindByNameAsync(string roleName)
        {
            return await _context.Roles.SingleOrDefaultAsync(u => u.Name == roleName);
        }
    }
}
