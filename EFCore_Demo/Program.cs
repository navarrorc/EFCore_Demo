using System;
using Microsoft.EntityFrameworkCore;
using EFCore_Demo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCore_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string command = "";

            while (command != "exit")
            {
                Console.WriteLine("Please make a selection:");
                Console.WriteLine("1. Add a new blog.");
                Console.WriteLine("2. View all blogs.");
                command = Console.ReadLine();

                if (command == "1")
                {
                    Console.Write("What is the Url of your blog? ");
                    var url = Console.ReadLine();
                    AddBlog(url);
                    Console.WriteLine("The blog has been added successfully!");
                    Console.WriteLine("Press [Enter] to continue.");
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (command == "2")
                {
                    Console.WriteLine("Retriving the blogs...");
                    GetBlogs();
                    Console.WriteLine("Press [Enter] to continue.");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    if (command != "exit")
                    {
                        Console.WriteLine("You have entered an invalid command, please try again!");
                    }
                }
            }

            //Console.ReadLine();

        }

        static void AddBlog(string url = "https://myblog.com/new-blog")
        {
            var blog = new Blog
            {
                Url = url
            };

            using (var db = new BloggingContext())
            {
                db.Add(blog);
                db.SaveChanges();
            }
        }

        static void GetBlogs()
        {
            using (var db = new BloggingContext())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var blog in db.Blogs)
                {
                    Console.WriteLine(blog.Url);
                }
                Console.ResetColor();

            }

        }

        static async Task GetBlogsAsync()
        {
            List<Blog> blogs;
            using (var db = new BloggingContext())
            {
                blogs = await db.Blogs.ToListAsync();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            blogs.ForEach(x => Console.WriteLine(x.Url));
            Console.ResetColor();
        }
    }
}
