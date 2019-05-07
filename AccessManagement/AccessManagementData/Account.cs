using AccessManagementServices.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccessManagementData
{
    public class Account
    {
        public Account()
        {
            //Branches = new HashSet<Branch>();
        }
        public int Id { get; set; }

        public string AccountName { get; set; }

        public string Password { get; set; }
        public string Name { get; set; }

        public AccountType Type { get; set; }

        public AccountStatus Status { get; set; }

        //public int? CurrentRegionId { get; set; }

        public string City { get; set; }
        public string Phone { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreateUserId { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }


        public virtual Branch Branch { get; set; }


        public int BranchId { get; set; }


        /// <summary>
        ///   返回当前的Branch的id以及所有第一层子branch的id
        /// </summary>
        /// <returns></returns>
        public List<int> GetBranchIds()
        {
            //var branchIds = new List<int>();
            //if (this.Branches.Any())
            //{
            //    branchIds = Branches.Select(x => x.Id).ToList();
            //}
            var currentBranch = Branch;
            var branchIds = currentBranch.Branches.Select(x => x.Id).ToList();
            branchIds.Add(currentBranch.Id);

            return branchIds;
        }
        public List<Branch> GetBranchs()
        {
            //var branchIds = new List<int>();
            //if (this.Branches.Any())
            //{
            //    branchIds = Branches.Select(x => x.Id).ToList();
            //}
            var currentBranch = Branch;
            var branchIds = currentBranch.Branches.ToList();
            branchIds.Add(currentBranch);
            return branchIds;
        }

        public List<string> GetBranchNames()
        {
            //var branchIds = new List<int>();
            //if (this.Branches.Any())
            //{
            //    branchIds = Branches.Select(x => x.Id).ToList();
            //}
            var currentBranch = Branch;
            var branchNames = currentBranch.Branches.Select(x => x.Name).ToList();
            branchNames.Add(currentBranch.Name);
            return branchNames;
        }
    }
}