using IDP_Back_End.Models;
using IDP_Back_End.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDP_Back_End.Repository
{
  public class DBInit
  {
    public static void SeedDB(DBContext ctx)
    {
          ctx.Database.EnsureDeleted();
          ctx.Database.EnsureCreated();

        //Hashing the password "1234"
        //Will have to be changed Terrible password
        string password = "aztamindenit1";
        byte[] passwordHashAdmin, passwordSaltAdmin, passwordHashUser, passwordSaltUser;
        CreatePasswordHash(password, out passwordHashAdmin, out passwordSaltAdmin);
        CreatePasswordHash(password, out passwordHashUser, out passwordSaltUser);

        // Adding 2 Test Users
        var user1 = ctx.Users.Add(new User()
        {
            UserName = "admin",
            PasswordHash = passwordHashAdmin,
            PasswordSalt = passwordSaltAdmin,
            Admin = true
        }).Entity;

        var user0 = ctx.Users.Add(new User()
        {
            UserName = "Fr4ce",
            PasswordHash = passwordHashAdmin,
            PasswordSalt = passwordSaltAdmin,
            Admin = true
        }).Entity;

        // Adding dummy CheckListItems
        var cli0 = ctx.CheckListItems.Add(new CheckListItem()
        {
            Done = false,
            Text = "Do some research"
        }).Entity;

        var cli1 = ctx.CheckListItems.Add(new CheckListItem()
        {
            Done = false,
            Text = "Do that one thing that we need"
        }).Entity;

        var cli2 = ctx.CheckListItems.Add(new CheckListItem()
        {
            Done = false,
            Text = "Do that other very important thing"
        }).Entity;

        var cli3 = ctx.CheckListItems.Add(new CheckListItem()
        {
            Done = false,
            Text = "Do the one thing where the fate of the universe hangs in the balance"
        }).Entity;

        var cli4 = ctx.CheckListItems.Add(new CheckListItem()
        {
            Done = false,
            Text = "Create the protomolecule sample that can be used on humans"
        }).Entity;

        // Adding Dummy Task comments
        var comment0 = ctx.Comments.Add(new Comment() {
            Text = "Oh wow this is much important!!!",
            User = user0,
            TimePosted = DateTime.UtcNow
        }).Entity;
        var comment1 = ctx.Comments.Add(new Comment() {
            Text = "I think is Heresy of the highest order and will ignore it from now on.",
            User = user0,
            TimePosted = DateTime.UtcNow
        }).Entity;
        var comment2 = ctx.Comments.Add(new Comment() {
            Text = "The Fate of the universe does no longer hang in balance, please delete",
            User = user1,
            TimePosted = DateTime.UtcNow
        }).Entity;
        var comment3 = ctx.Comments.Add(new Comment() {
            Text = "I would not give my name to this now",
            User = user1,
            TimePosted = DateTime.UtcNow
        }).Entity;
        var comment4 = ctx.Comments.Add(new Comment() {
            Text = "This is a problem right here because: dksbajldbwjahvfgjavjwdhzbaikduwbajfhbwakidbahbvkajwbdizagbwidhbajgsbdikagolieokoujhbawdhzvcbajkhbaudbwagdiwza",
            User = user1,
            TimePosted = DateTime.UtcNow
        }).Entity;

            // Adding Dummy Tasks
            var task0 = ctx.Tasks.Add(new Task() {
                Title = "Create planet to play on.",
                Description = "You know the thing. We need a thing where we can do the activity that we talked about that one time. If you are on this page you must know what I mean.",
                TaskOf = user0,
                CreatedBy = user1,
                CheckListItems = new List<CheckListItem>(),
                Comments = new List<Comment>(),
                TimeCreated = DateTime.UtcNow,
                Done = true
            }).Entity;
            var task1 = ctx.Tasks.Add(new Task() {
                Title = "Create the sky that we need to look at.",
                Description = "I think this one if pretty self explanitory....",
                CreatedBy = user1,
                CheckListItems = new List<CheckListItem>(),
                Comments = new List<Comment>(),
                TimeCreated = DateTime.UtcNow,
                Done = true
            }).Entity;
            var task2 = ctx.Tasks.Add(new Task() {
                Title = "Do something for fun",
                Description = "We should find a way to have fun, maybe look at the entities we put on the planet?",
                TaskOf = user1,
                CreatedBy = user0,
                CheckListItems = new List<CheckListItem>(),
                Comments = new List<Comment>(),
                TimeCreated = DateTime.UtcNow,
                Done = true
            }).Entity;


            // Adding connections of the models to eachother
            task0.CheckListItems.Add(cli0);
            task0.CheckListItems.Add(cli1);
            task1.CheckListItems.Add(cli2);
            task1.CheckListItems.Add(cli3);
            task1.CheckListItems.Add(cli4);

            task0.Comments.Add(comment0);
            task0.Comments.Add(comment1);
            task0.Comments.Add(comment2);
            task1.Comments.Add(comment3);
            task1.Comments.Add(comment4);

        // Adding 2 Categories
        var category0= ctx.Categories.Add(new TaskCategory()
        {
            Title = "Research",
            Tasks = new List<Task>()
        }).Entity;
        var category1 = ctx.Categories.Add(new TaskCategory()
        {
            Title = "To Do List",
            Tasks = new List<Task>()
        }).Entity;
        var category2 = ctx.Categories.Add(new TaskCategory()
        {
            Title = "In Progress",
            Tasks = new List<Task>()
        }).Entity;
        var category3 = ctx.Categories.Add(new TaskCategory()
        {
            Title = "Completed Tasks",
            Tasks = new List<Task>()
        }).Entity;
            category0.Tasks.Add(task0);
            category0.Tasks.Add(task1);
            category3.Tasks.Add(task2);
            ctx.SaveChanges();
    }
    #region Methods needed
    //Hashing method
    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }
    #endregion
  }
}
