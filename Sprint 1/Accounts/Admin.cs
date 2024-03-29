﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Quic;
using System.Text;
public class Admin : IAccount
{
    public static int nextID = Manager.Admins.Count;
    public int ID { get; }
    public string Name { get; set; }
    public string Password { get; private set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public Admin(string name, string email, string password, string role = "Admin")
    {
        ID = Interlocked.Increment(ref nextID);
        Name = name;
        Password = password;
        Email = email;
        Role = role;
    }
    public static Admin CreateAdmin(string name, string email, string password)
    {
        Admin admin = new Admin(name, email, password);
        Manager.Admins.Add(admin);
        ControllerJson.WriteJson(Manager.Admins, "Admins.json");
        return admin;
    }
    public static Admin? GetAdminByID(int id)
    {
        foreach (Admin admin in Manager.Admins)
        {
            if (admin.ID == id)
            {
                return admin;
            }
        }
        return null;
    }
    public static string? ChangePassword(int ID, string password)
    {
        foreach (Admin admin in Manager.Admins)
        {
            if (admin.ID == ID && admin.Password == password)
            {
                Console.Write("Please enter your new password: ");
                admin.Password = Console.ReadLine() ?? password;
                ControllerJson.WriteJson(Manager.Admins, "Admins.json");
                return admin.Password;
            }
        }
        return null;
    }
    public static Admin? Log_in(string name, string email, string password)
    {
        foreach (Admin admin in Manager.Admins)
        {
            if (admin.Name == name && admin.Password == password && admin.Email == email)
            {
                return admin;
            }
        }
        return null;
    }
}
