using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Interfaces;
using Coordinare.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Coordinare.Services
{
    public class TagSelection : Connection, ITagSelection
    {
        private string GetTagNamesSql = "SELECT * from TagNames";
        private string CreateTagNameSql = "INSERT into Tags values(@EID,@TID)";
        private string CreateTagNameSql = "INSERT into Tags values(@EID,@TID)";
        private string GetTagSql = "SELECT * from tags";
        private string CreateTagSql = "INSERT into Tags values(@EID, @TID)";
        private string DeleteTagSql = "DELETE from events where Event_ID = @EID AND Tag_ID =@TID";
        private IEventCatalog _ecatalog;

        public TagSelection(IConfiguration configuration, IEventCatalog ecatolog) : base(configuration)
        {
        }
        public TagSelection(string connectionstring, IEventCatalog ecatolog) : base(connectionstring)
        {
        }

        public async Task<List<string>> GetTagNames()
        {
            List<string> tnl = new List<string>();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {

                await using (SqlCommand command = new SqlCommand(GetTagNamesSql, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            string tagname = reader.GetString(i: 1);

                            tnl.Add(tagname);
                        }
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                        return null;
                    }
                }
            };
            return tnl;
        }

        public Task<List<string>> GetTags()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Tag>> GetAllTags()
        {
            List<Tag> tl = new List<Tag>();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {

                await using (SqlCommand command = new SqlCommand(GetTagSql, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int taggedid = reader.GetInt32(i: 0);
                            int Tagid = reader.GetInt32(i: 1);
                            int eventid = reader.GetInt32(i: 2);

                            Tag t = new Tag(Tagid, eventid, name);

                            tl.Add(t);
                        }
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                        return null;
                    }
                }
            };
            return tl;
        }

        public void AddNewTag(string tag)
        {
            tags.Add(tag);
        }

        public void RemoveTag(string tag)
        {
            if (tags.Contains(tag))
            {
                tags.Remove(tag);
            }
        }
        public async void CreateNewTagName(string tag)
        {
            // INSERT into Tags values(@Name)";

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(CreateTagSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@Name", tag);
                        await command.Connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                    }
                }
            }
        }
        public async void CreateNewTagToEvent(Event e, string tag)
        {
            //@EID AND TagName = @Name

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(CreateTagSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@EID", e.Event_ID);
                        command.Parameters.AddWithValue("@TID", tag);
                        await command.Connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                    }
                }
            }
        }

        public async void DeleteTag(Event e, string tag)
        {
            //@EID AND TagName = @Name
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(DeleteTagSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@EID", e.Event_ID);
                        command.Parameters.AddWithValue("@Name", tag);
                        await command.Connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (SqlException)
                    {
                        Console.WriteLine("Database Fejl");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Generel Fejl");
                    }
                }
            }
        }
    }
}
