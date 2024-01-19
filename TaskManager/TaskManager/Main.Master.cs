using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaskManager
{
	public partial class Main : System.Web.UI.MasterPage
	{
		
		string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
		protected void Page_Load(object sender, EventArgs e)
		{
			string userRole = Session["role"]?.ToString();
			string userID = Session["user_id"]?.ToString();
			Label3.Text = "Hi, " + userID;

			if (adduser != null)
			{
				// Check if the user has the 'Edit Complaint Details' permission
				bool hasEditPermission = IsAddUserPermissionEnabled(userRole);

				if (userRole != null && userRole.Equals("User") && hasEditPermission)
				{
					adduser.Visible = true;
				}
				else if (userRole != null && userRole.Equals("Admin") && hasEditPermission)
				{
					adduser.Visible = true;
				}
				else
				{
					adduser.Visible = false;
				}
			}
			else
			{
				Response.Write("Add User box not found!");
			}

			if (rolemanagement != null)
			{
				// Check if the user has the 'Edit Complaint Details' permission
				bool hasEditrolePermission = IsRoleManagementPermissionEnabled(userRole);

				if (userRole != null && userRole.Equals("User") && hasEditrolePermission)
				{
					rolemanagement.Visible = true;
				}
				else if (userRole != null && userRole.Equals("Admin") && hasEditrolePermission)
				{
					rolemanagement.Visible = true;
				}
				else
				{
					rolemanagement.Visible = false;
				}
			}
			else
			{
				Response.Write("Role Management box not found!");
			}

			if (assigntask != null)
			{
				// Check if the user has the 'Edit Complaint Details' permission
				bool hasEditrolePermission = IsAssignTaskPermissionEnabled(userRole);

				if (userRole != null && userRole.Equals("User") && hasEditrolePermission)
				{
					assigntask.Visible = true;
				}
				else if (userRole != null && userRole.Equals("Admin") && hasEditrolePermission)
				{
					assigntask.Visible = true;
				}
				else
				{
					assigntask.Visible = false;
				}
			}
			else
			{
				Response.Write("Assign Tasks box not found!");
			}


		}

		private bool IsAddUserPermissionEnabled(string roleName)
		{
			bool isPermissionEnabled = false;
			if (string.IsNullOrEmpty(roleName))
			{
				// Handle the case where roleName is null or empty.
				// You might want to log this situation or throw an exception.
				return false;
			}

			using (SqlConnection conn = new SqlConnection("Data Source=taskgroup.database.windows.net;Initial Catalog=TaskMaker;Persist Security Info=True;User ID=taskgroup;Password=Task@group"))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand("SELECT Permissions FROM RolePermissions WHERE Role = @RoleName", conn);
				cmd.Parameters.AddWithValue("@RoleName", roleName);

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						string permissions = reader.GetString(0);
						var permissionList = permissions.Split(',');
						isPermissionEnabled = permissionList.Contains("Add User");
					}
				}
			}

			return isPermissionEnabled;
		}

		private bool IsRoleManagementPermissionEnabled(string roleName)
		{
			bool isPermissionEnabled = false;
			if (string.IsNullOrEmpty(roleName))
			{
				// Handle the case where roleName is null or empty.
				// You might want to log this situation or throw an exception.
				return false;
			}

			using (SqlConnection conn = new SqlConnection("Data Source=taskgroup.database.windows.net;Initial Catalog=TaskMaker;Persist Security Info=True;User ID=taskgroup;Password=Task@group"))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand("SELECT Permissions FROM RolePermissions WHERE Role = @RoleName", conn);
				cmd.Parameters.AddWithValue("@RoleName", roleName);

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						string permissions = reader.GetString(0);
						var permissionList = permissions.Split(',');
						isPermissionEnabled = permissionList.Contains("Allow Role Management");
					}
				}
			}

			return isPermissionEnabled;
		}

		private bool IsAssignTaskPermissionEnabled(string roleName)
		{
			bool isPermissionEnabled = false;
			if (string.IsNullOrEmpty(roleName))
			{
				// Handle the case where roleName is null or empty.
				// You might want to log this situation or throw an exception.
				return false;
			}

			using (SqlConnection conn = new SqlConnection("Data Source=taskgroup.database.windows.net;Initial Catalog=TaskMaker;Persist Security Info=True;User ID=taskgroup;Password=Task@group"))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand("SELECT Permissions FROM RolePermissions WHERE Role = @RoleName", conn);
				cmd.Parameters.AddWithValue("@RoleName", roleName);

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						string permissions = reader.GetString(0);
						var permissionList = permissions.Split(',');
						isPermissionEnabled = permissionList.Contains("Assign Tasks");
					}
				}
			}

			return isPermissionEnabled;
		}

		protected void logoutButton_Click(object sender, EventArgs e)
		{
			Session.Clear();
			Response.Redirect("Login_Page.aspx");
		}

		protected void home_Click(object sender, EventArgs e)
		{
			Response.Redirect("Home_Page.aspx");
		}

		protected void adduser_Click(object sender, EventArgs e)
		{
			Response.Redirect("Add_User.aspx");
		}

		protected void rolemanagement_Click(object sender, EventArgs e)
		{
			Response.Redirect("Role_Management.aspx");
		}

		protected void assigntask_Click(object sender, EventArgs e)
		{
			Response.Redirect("Assign_Task.aspx");
		}

		protected void manageusers_Click(object sender, EventArgs e)
		{
			Response.Redirect("User_Management.aspx");
		}
	}
}