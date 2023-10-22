using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using BNI_Users_backend.Data;
using BNI_Users_backend.Models;

namespace BNI_Users_backend.Controllers
{
  class UsersController
  {

    public static async Task<IResult> GetUsers(
      [FromServices] UserContext db, 
      [FromQuery(Name = "p")] int? pageNumber
    )
    {
      int numRecordsPerPage = 10;
      var count = db.User.Count();
      int numPages = (int)Math.Ceiling((decimal)count / numRecordsPerPage);
      var data = await db.User.
        OrderBy(item => item.id).Reverse().
        Skip(((pageNumber ?? 1) - 1) * numRecordsPerPage).
        Take(numRecordsPerPage).
        ToListAsync();
      return Results.Ok(new { NumPages = numPages, Data = data });
    }

    public static async Task<IResult> GetUserById(
      [FromServices] UserContext db, 
      [FromRoute] int id
    )
    {
      var findUser = await db.User.FindAsync(id);
      if (findUser is null) return Results.NotFound(new { Message = "Data tidak ditemukan" });
      return Results.Ok(findUser);
    }

    public static async Task<IResult> AddUser(
      [FromServices] UserContext db,
      [FromServices] IValidator<User> validator,
      User user
    )
    {
      var validationResult = await validator.ValidateAsync(user);
      if (!validationResult.IsValid)
      {
        return Results.ValidationProblem(validationResult.ToDictionary());
      }
      await db.User.AddAsync(user);
      await db.SaveChangesAsync();
      return Results.Created($"/{user.id}", user);
    }

    public static async Task<IResult> UpdateUser(
      [FromServices] UserContext db, 
      [FromServices] IValidator<User> validator,
      [FromRoute] int id,
      User user
    )
    {
      var findUser = await db.User.FindAsync(id);
      if (findUser is null) return Results.NotFound(new { Message = "Data tidak ditemukan" });
      var validationResult = await validator.ValidateAsync(user);
      if (!validationResult.IsValid)
      {
        return Results.ValidationProblem(validationResult.ToDictionary());
      }
      findUser.fName = user.fName;
      findUser.lName = user.lName;
      findUser.telephone = user.telephone;
      findUser.email = user.email;
      findUser.dateOfBirth = user.dateOfBirth;
      findUser.description = user.description;
      findUser.questionSecondAuthentication = user.questionSecondAuthentication;
      await db.SaveChangesAsync();
      return Results.Ok(new { Message = "Data telah sukses terupdate" });
    }

    public static async Task<IResult> DeleteUser(
      [FromServices] UserContext db, 
      [FromRoute] int id
    )
    {
      var findUser = await db.User.FindAsync(id);
      if (findUser is null) return Results.NotFound(new { Message = "Data tidak ditemukan" });
      db.User.Remove(findUser);
      await db.SaveChangesAsync();
      return Results.Ok(new { Message = "Data telah sukses terdelete" });
    }
  }
}