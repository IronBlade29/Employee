using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web.Mvc;

namespace Employee.Models
{
    public class EmployeePortfolio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        [DataType(DataType.MultilineText)]
        public  string Projects { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public string gender { get; set; }
        public string Education { get; set; }
        public string Achievements { get; set; }
        public string References { get; set; }
    }
    public class Skill
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Projects { get; set; }
        public string gender { get; set; }
        public string Education { get; set; }
        public string Achievements { get; set; }
        public string References { get; set; }
        public IEnumerable<int> SelectedSkillIds { get; set; }
        public IEnumerable<SelectListItem> AvailableSkills { get; set; }

    }
    public class EmployeePortfolioContext : DbContext
    {
        public DbSet<EmployeePortfolio> EmployeePortfolios { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}