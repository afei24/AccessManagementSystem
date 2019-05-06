using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementData
{
    public class AccessManagementContext: DbContext
    {
        public AccessManagementContext(DbContextOptions<AccessManagementContext> options)
       : base(options)
        {
        }
        public virtual DbSet<Company> Company { get; set; }
    }
}
