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
        private string GetTagsSql = "SELECT * from Tags";
        private string GetTagNameFromIdSql = "SELECT * from TagNames WHERE Tag_ID = @ID";
        private string CreateTagNameSql = "INSERT into TagNames values(@Name)";
        private string CreateTagSql = "INSERT into Tags(Tag_ID, Event_ID) values(@TID ,@EID)";
        private string DeleteTagNameSql = "DELETE from Tags WHERE Tag_ID = @TID";
        private string DeleteTagSql = "DELETE from Tags where Event_ID = @EID AND Tag_ID = @TID";
        private IEventCatalog _ecatalog;

        public TagSelection(IConfiguration configuration, IEventCatalog ecatolog) : base(configuration)
        {
        }
        public TagSelection(string connectionstring, IEventCatalog ecatolog) : base(connectionstring)
        {
        }

        public async Task<List<TagName>> GetTagNames()
        {
            List<TagName> tnl = new List<TagName>();
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
                            int tagid = reader.GetInt32(i: 0);
                            string tagname = reader.GetString(i: 1);

                            tnl.Add(new TagName(tagid,tagname));
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

        public async Task<List<Tag>> GetAllTags()
        {
            List<Tag> tl = new List<Tag>();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {

                await using (SqlCommand command = new SqlCommand(GetTagsSql, connection))
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
                            string name = GetTagNameFromId(Tagid).Result.Name;
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

        public async Task<TagName> GetTagNameFromId(int id)
        {
            TagName tag = null;
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {

                await using (SqlCommand command = new SqlCommand(GetTagNameFromIdSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int tagid = reader.GetInt32(i: 0);
                            string name = reader.GetString(i: 1);

                            tag = new TagName(tagid, name);
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
            return tag;
        }

        public async Task<int> GetTagNameFromName(string name)
        {
            int? id = new int();
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {

                await using (SqlCommand command = new SqlCommand(GetTagNameFromIdSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            id = reader.GetInt32(i: 0);

                        }
                    }
                    catch (SqlException sx)
                    {
                        Console.WriteLine("Database Fejl");
                        return id.Value;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel Fejl");
                        return id.Value;
                    }
                }
            }
            ;

            return id.Value;
        }


        public async void CreateNewTagName(string tag)
        {
            // INSERT into Tags values(@Name)";

            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(CreateTagNameSql, connection))
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


        public async void CreateNewTagToEvent(Event e, TagName tag)
        {
            //@EID AND TagName = @Name
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(CreateTagSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@TID", tag.Tag_ID);
                        command.Parameters.AddWithValue("@EID", e.Event_ID);
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

        public async void DeleteTagName(int id)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await using (SqlCommand command = new SqlCommand(DeleteTagNameSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@TID", id);
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
