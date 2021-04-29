﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Exceptions;
using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Coordinare.Services
{
    public class UserCatalog : Connection, IUserCatalog
    {
        private string queryString = "Select * from Users";
        private string queryStringFromID = "select * from Users where User_ID = @ID";
        private string insertSql = "insert into Users Values(@ID, @Name, @Username, " +
                                   "@Password, @Phone, @Email, @Speaker, @Specialaid)";
        private string deleteSql = "delete from Users where User_ID = @ID";
        private string updateSql = "update Users set User_ID=@ID, Name=@Name, " +
                                   "Username=@Username, Password=@Password, Phone=@Phone, " +
                                   "Email=@Email, Speaker=@Speaker, Specialaid=@Specialaid)";

        public UserCatalog(IConfiguration configuration) : base(configuration)
        {
        }

        public UserCatalog(string connectionString) : base(connectionString)
        {
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = new List<User>();

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using SqlCommand command = new SqlCommand(queryString, connection);
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            int userID = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string username = reader.GetString(2);
                            string password = reader.GetString(3);
                            string phone = reader.GetString(4);
                            string email = reader.GetString(5);
                            byte speaker = reader.GetByte(6);
                            byte special = reader.GetByte(7);

                            //User user = new User(userID, name, username, password, phone, email, speaker, special);
                            //users.Add(user);
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }

                }
            }
            return users;
        }

        public async Task<User> GetUserFromIdAsync(int id)
        {
            User user = new User();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using SqlCommand command = new SqlCommand(queryStringFromID, connection);
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        command.Parameters.AddWithValue("@ID", id);
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        if (reader.Read())
                        {
                            int userID = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string username = reader.GetString(2);
                            string password = reader.GetString(3);
                            string phone = reader.GetString(4);
                            string email = reader.GetString(5);
                            bool speaker = reader.GetBoolean(6);
                            bool special = reader.GetBoolean(7);
                            user = new User(userID, name, username, password, phone, email, speaker, special);
                        }
                        else
                        {
                            user = null;
                        }
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }

            return user;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using SqlCommand command = new SqlCommand(insertSql, connection);
                {
                    if (IdExist(user.User_ID))
                    {
                        throw new ExistsException("Hotel number already exists, please choose another.");
                    }
                    command.Parameters.AddWithValue("@ID", user.User_ID);
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Phone", user.Phone);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Speaker", user.Speaker);
                    command.Parameters.AddWithValue("@Specialaid", user.Specialaid);
                    
                    await command.Connection.OpenAsync();
                    int noOfRows = await command.ExecuteNonQueryAsync();
                    if (noOfRows == 1)
                    {
                        return true;
                    }

                }

                return false;
            }
        }

        private bool IdExist(int id)
        {
            foreach (User u in GetAllUsersAsync().Result)
            {
                if (u.User_ID == id)
                    return true;
            }
            return false;
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            User user = await GetUserFromIdAsync(id);
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using SqlCommand command = new SqlCommand(deleteSql, connection);
                {

                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@ID", id);
                    int noOfRows = await command.ExecuteNonQueryAsync();
                    if (noOfRows == 1)
                    {

                        return user;
                    }

                }

            }

            return null;
        }

        public async Task<bool> UpdateUserAsync(User user, int id)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using SqlCommand command = new SqlCommand(updateSql, connection);
                {

                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@ID", user.User_ID);
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Phone", user.Phone);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Speaker", user.Speaker);
                    command.Parameters.AddWithValue("@Specialaid", user.Specialaid);
                    int noOfRows = await command.ExecuteNonQueryAsync();
                    if (noOfRows == 1)
                    {
                        return true;
                    }

                }
            }

            return false;
        }
    }
}
