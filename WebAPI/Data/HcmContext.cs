using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data;

public partial class HcmContext : DbContext
{
    public HcmContext()
    {
    }

    public HcmContext(DbContextOptions<HcmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<EducationLevel> EducationLevels { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeesBranchesHistory> EmployeesBranchesHistory { get; set; }

    public virtual DbSet<EmployeesJobHistory> EmployeesJobHistory { get; set; }

    public virtual DbSet<ExperienceLevel> ExperienceLevels { get; set; }

    public virtual DbSet<HcmUser> HcmUsers { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Payroll> Payrolls { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectsTeamsHistory> ProjectsTeamsHistory { get; set; }

    public virtual DbSet<Training> Trainings { get; set; }

    public virtual DbSet<TrainingsHistory> TrainingsHistory { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("branches_pkey");

            entity.ToTable("branches", "hcm");

            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.BranchCountryId).HasColumnName("branch_country_id");
            entity.Property(e => e.BranchName)
                .HasMaxLength(50)
                .HasColumnName("branch_name");

            entity.HasOne(d => d.BranchCountry).WithMany(p => p.Branches)
                .HasForeignKey(d => d.BranchCountryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("branches_branch_country_id_fkey");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("countries_pkey");

            entity.ToTable("countries", "hcm");

            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(50)
                .HasColumnName("country_name");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("departments_pkey");

            entity.ToTable("departments", "hcm");

            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .HasColumnName("department_name");
        });

        modelBuilder.Entity<EducationLevel>(entity =>
        {
            entity.HasKey(e => e.EducationLevelId).HasName("education_levels_pkey");

            entity.ToTable("education_levels", "hcm");

            entity.Property(e => e.EducationLevelId).HasColumnName("education_level_id");
            entity.Property(e => e.EducationLevelTitle)
                .HasMaxLength(20)
                .HasColumnName("education_level_title");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employees_pkey");

            entity.ToTable("employees", "hcm");

            entity.HasIndex(e => e.PayrollId, "employees_payroll_id_key").IsUnique();

            entity.HasIndex(e => e.UserId, "employees_user_id_key").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.DateOfEmployment).HasColumnName("date_of_employment");
            entity.Property(e => e.DateOfLeaving).HasColumnName("date_of_leaving");
            entity.Property(e => e.EducationLevelId).HasColumnName("education_level_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.EmployeeAddress)
                .HasMaxLength(255)
                .HasColumnName("employee_address");
            entity.Property(e => e.ExperienceLevelId).HasColumnName("experience_level_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .HasColumnName("gender");
            entity.Property(e => e.JobId).HasColumnName("job_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.NationalityId).HasColumnName("nationality_id");
            entity.Property(e => e.PayrollId).HasColumnName("payroll_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Branch).WithMany(p => p.Employees)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employees_branch_id_fkey");

            entity.HasOne(d => d.ExperienceLevel).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ExperienceLevelId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("employees_experience_level_id_fkey");

            entity.HasOne(d => d.Job).WithMany(p => p.Employees)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("employees_job_id_fkey");

            entity.HasOne(d => d.Nationality).WithMany(p => p.Employees)
                .HasForeignKey(d => d.NationalityId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("employees_nationality_id_fkey");

            entity.HasOne(d => d.Payroll).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.PayrollId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employees_payroll_id_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("employees_project_id_fkey");

            entity.HasOne(d => d.User).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employees_user_id_fkey");
        });

        modelBuilder.Entity<EmployeesBranchesHistory>(entity =>
        {
            entity.HasKey(e => e.EmployeeBranchId).HasName("employees_branches_history_pkey");

            entity.ToTable("employees_branches_history", "hcm");

            entity.Property(e => e.EmployeeBranchId).HasColumnName("employee_branch_id");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.DateEnded).HasColumnName("date_ended");
            entity.Property(e => e.DateStarted).HasColumnName("date_started");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

            entity.HasOne(d => d.Branch).WithMany(p => p.EmployeesBranchesHistories)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("employees_branches_history_branch_id_fkey");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeesBranchesHistories)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("employees_branches_history_employee_id_fkey");
        });

        modelBuilder.Entity<EmployeesJobHistory>(entity =>
        {
            entity.HasKey(e => e.EmployeeJobId).HasName("employees_job_history_pkey");

            entity.ToTable("employees_job_history", "hcm");

            entity.Property(e => e.EmployeeJobId).HasColumnName("employee_job_id");
            entity.Property(e => e.DateEnded).HasColumnName("date_ended");
            entity.Property(e => e.DateStarted).HasColumnName("date_started");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.JobId).HasColumnName("job_id");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeesJobHistories)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("employees_job_history_employee_id_fkey");

            entity.HasOne(d => d.Job).WithMany(p => p.EmployeesJobHistories)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("employees_job_history_job_id_fkey");
        });

        modelBuilder.Entity<ExperienceLevel>(entity =>
        {
            entity.HasKey(e => e.ExperienceLevelId).HasName("experience_levels_pkey");

            entity.ToTable("experience_levels", "hcm");

            entity.Property(e => e.ExperienceLevelId).HasColumnName("experience_level_id");
            entity.Property(e => e.ExperienceLevelTitle)
                .HasMaxLength(20)
                .HasColumnName("experience_level_title");
        });

        modelBuilder.Entity<HcmUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("hcm_users_pkey");

            entity.ToTable("hcm_users", "hcm");

            entity.HasIndex(e => e.Username, "hcm_users_username_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(50)
                .HasColumnName("user_password");
            entity.Property(e => e.UserRoleId).HasColumnName("user_role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.UserRole).WithMany(p => p.HcmUsers)
                .HasForeignKey(d => d.UserRoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("hcm_users_user_role_id_fkey");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("jobs_pkey");

            entity.ToTable("jobs", "hcm");

            entity.Property(e => e.JobId).HasColumnName("job_id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(50)
                .HasColumnName("job_title");

            entity.HasOne(d => d.Department).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("jobs_department_id_fkey");
        });

        modelBuilder.Entity<Payroll>(entity =>
        {
            entity.HasKey(e => e.PayrollId).HasName("payrolls_pkey");

            entity.ToTable("payrolls", "hcm");

            entity.Property(e => e.PayrollId).HasColumnName("payroll_id");
            entity.Property(e => e.EffectiveDate).HasColumnName("effective_date");
            entity.Property(e => e.GrossSalary).HasColumnName("gross_salary");
            entity.Property(e => e.HourlyRate).HasColumnName("hourly_rate");
            entity.Property(e => e.LastChanged).HasColumnName("last_changed");
            entity.Property(e => e.NetSalary).HasColumnName("net_salary");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("projects_pkey");

            entity.ToTable("projects", "hcm");

            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.DateEnded).HasColumnName("date_ended");
            entity.Property(e => e.DateStarted).HasColumnName("date_started");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(50)
                .HasColumnName("project_name");
            entity.Property(e => e.ProjectStatus)
                .HasMaxLength(20)
                .HasColumnName("project_status");
        });

        modelBuilder.Entity<ProjectsTeamsHistory>(entity =>
        {
            entity.HasKey(e => e.EmployeeProjectId).HasName("projects_teams_history_pkey");

            entity.ToTable("projects_teams_history", "hcm");

            entity.Property(e => e.EmployeeProjectId).HasColumnName("employee_project_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");

            entity.HasOne(d => d.Employee).WithMany(p => p.ProjectsTeamsHistories)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("projects_teams_history_employee_id_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectsTeamsHistories)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("projects_teams_history_project_id_fkey");
        });

        modelBuilder.Entity<Training>(entity =>
        {
            entity.HasKey(e => e.TrainingId).HasName("trainings_pkey");

            entity.ToTable("trainings", "hcm");

            entity.Property(e => e.TrainingId).HasColumnName("training_id");
            entity.Property(e => e.SkillsCovered)
                .HasMaxLength(255)
                .HasColumnName("skills_covered");
            entity.Property(e => e.TrainingTitle)
                .HasMaxLength(20)
                .HasColumnName("training_title");
        });

        modelBuilder.Entity<TrainingsHistory>(entity =>
        {
            entity.HasKey(e => e.EmployeeTrainingId).HasName("trainings_history_pkey");

            entity.ToTable("trainings_history", "hcm");

            entity.Property(e => e.EmployeeTrainingId).HasColumnName("employee_training_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.TrainingId).HasColumnName("training_id");

            entity.HasOne(d => d.Employee).WithMany(p => p.TrainingsHistories)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("trainings_history_employee_id_fkey");

            entity.HasOne(d => d.Training).WithMany(p => p.TrainingsHistories)
                .HasForeignKey(d => d.TrainingId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("trainings_history_training_id_fkey");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("user_roles_pkey");

            entity.ToTable("user_roles", "hcm");

            entity.Property(e => e.UserRoleId).HasColumnName("user_role_id");
            entity.Property(e => e.UserRoleTitle)
                .HasMaxLength(21)
                .HasColumnName("user_role_title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
