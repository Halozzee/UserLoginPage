using System.Data.SqlClient;
using System.Text;

namespace UserLoginPage
{
	public static class DataBaseLogic
	{
		const string _connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=testdb;Trusted_Connection=True;";

		public static string RemoveSpecialCharacters(this string str)
		{
			StringBuilder sb = new StringBuilder();
			foreach (char c in str)
			{
				if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
				{
					sb.Append(c);
				}
			}
			return sb.ToString();
		}

		public static bool UserExists(string login) 
		{
			login = RemoveSpecialCharacters(login);

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				string sqlExpression = "SELECT * " +
					$"FROM site_users WHERE login=\'{login}\'";

				using (SqlCommand command = new SqlCommand(sqlExpression, connection))
				{
					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							return true;
						}
					}
				}
			}

			return false;
		}

		public static bool TryLogin(string login, string passwordHash)
		{
			login = RemoveSpecialCharacters(login);
			passwordHash = RemoveSpecialCharacters(passwordHash);

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				string sqlExpression = "SELECT * " +
					$"FROM site_users WHERE login=\'{login}\' AND password_hash=\'{passwordHash}\'";

				using (SqlCommand command = new SqlCommand(sqlExpression, connection))
				{
					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							return true;
						}
					}
				}
			}

			return false;
		}

		public static bool TryRegister(string login, string passwordHash) 
		{
			login = RemoveSpecialCharacters(login);
			passwordHash = RemoveSpecialCharacters(passwordHash);

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				string sqlExpression = "INSERT INTO site_users(login, password_hash)" +
					$"VALUES(\'{login}\',\'{passwordHash}\')";

				using (SqlCommand command = new SqlCommand(sqlExpression, connection))
				{
					int aff = command.ExecuteNonQuery();
					return aff > 0;
				}
			}
		}

		public static bool TryChangePassword(string login, string passwordHash)
		{
			login = RemoveSpecialCharacters(login);
			passwordHash = RemoveSpecialCharacters(passwordHash);

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				string sqlExpression = $"UPDATE site_users SET password_hash=\'{passwordHash}\' WHERE login=\'{login}\'";

				using (SqlCommand command = new SqlCommand(sqlExpression, connection))
				{
					int aff = command.ExecuteNonQuery();
					return aff > 0;
				}
			}
		}
	}
}
