using EduQuiz.Domain.Entities;
using EduQuiz.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Infrastructure.Context
{
    public class DbSeeder
    {
        public static async Task SeedAsync(EduQuizDbContext context)
        {
            if (!await context.Roles.AnyAsync())
            {
                var teacherRole = new Role { Id = Guid.NewGuid(), Name = "Teacher" };
                var studentRole = new Role { Id = Guid.NewGuid(), Name = "Student" };
                var adminRole = new Role { Id = Guid.NewGuid(), Name = "Admin" };

                await context.Roles.AddRangeAsync(teacherRole, studentRole, adminRole);
                await context.SaveChangesAsync();
            }

            var teacherRoleExisted = await context.Roles.FirstAsync(r => r.Name == "Teacher");
            var studentRoleExisted = await context.Roles.FirstAsync(r => r.Name == "Student");
            var adminRoleExisted = await context.Roles.FirstAsync(r => r.Name == "Admin");

            if (!await context.Accounts.AnyAsync())
            {
                var accounts = new List<Account>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Email = "teacher@test.com",
                        Password = PasswordHasher.Hash("123456"),
                        FirstName = "Anna",
                        LastName = "Nguyen",
                        Gender = "Female",
                        DateOfBirth = new DateTime(1995, 5, 12),
                        Address = "Hanoi, Vietnam",
                        PhoneNumber = "0901234567",
                        RoleId = teacherRoleExisted.Id,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Email = "student@test.com",
                        Password = PasswordHasher.Hash("123456"),
                        FirstName = "Minh",
                        LastName = "Tran",
                        Gender = "Male",
                        DateOfBirth = new DateTime(2005, 9, 20),
                        Address = "Ho Chi Minh City, Vietnam",
                        PhoneNumber = "0912345678",
                        RoleId = studentRoleExisted.Id,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Email = "admin@test.com",
                        Password = PasswordHasher.Hash("123456"),
                        FirstName = "System",
                        LastName = "Admin",
                        Gender = "Male",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        Address = "Head Office",
                        PhoneNumber = "0999999999",
                        RoleId = adminRoleExisted.Id,
                        CreatedAt = DateTime.UtcNow,
                        IsActive= true
                    }
                };

                await context.Accounts.AddRangeAsync(accounts);
                await context.SaveChangesAsync();
            }

            if (!await context.Teachers.AnyAsync())
            {
                var teacherAccount = await context.Accounts.FirstOrDefaultAsync(a => a.Email == "teacher@test.com");
                if (teacherAccount != null)
                {
                    var teacher = new Teacher
                    {
                        Id = Guid.NewGuid(),
                        AccountId = teacherAccount.Id,
                        Bio = "English teacher with 5 years experience",
                        Department = "English",                       
                    };

                    await context.Teachers.AddAsync(teacher);
                }
            }

            if (!await context.Students.AnyAsync())
            {
                var studentAccount = await context.Accounts.FirstOrDefaultAsync(a => a.Email == "student@test.com");
                if (studentAccount != null)
                {
                    var student = new Student
                    {
                        Id = Guid.NewGuid(),
                        AccountId = studentAccount.Id,
                        Grade = "10",
                        ParentPhoneNumer = "0909898989",
                        School = "Binh Phu Highschool"                       
                    };

                    await context.Students.AddAsync(student);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
