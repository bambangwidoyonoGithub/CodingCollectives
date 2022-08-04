using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Services;
using System.Data;
using System.Reflection;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace WebApplication1
{
    public partial class Index : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (Session["UserName"] != null)
                {
                    lblUser.Text = Session["UserName"].ToString();
                    if (!this.IsPostBack)
                    {
                        //DataTable dummy = new DataTable();
                        //dummy.Columns.Add("EmployeeName");
                        //dummy.Columns.Add("Jobtitle");
                        //dummy.Columns.Add("Salary");
                        //dummy.Rows.Add();
                        //gvEmployee.DataSource = dummy;
                        Employee xEmployeeClass = new Employee();
                        gvEmployee.DataSource = xEmployeeClass.CurentListEmployee();
                        gvEmployee.DataBind();

                        //Required for jQuery DataTables to work.
                        gvEmployee.UseAccessibleHeader = true;
                        gvEmployee.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }

            }
        }

        protected void LogOut(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string GetListEmployee()
        {
            string xresult = "";
            Employee xEmployeeClass = new Employee();

            //xresult = DataTableToJSONWithJavaScriptSerializer(xEmployeeClass.CurentListEmployee());
            xresult = DataTableToJSONWithJSONNet(xEmployeeClass.CurentListEmployee());
            return xresult;

        }

        public static string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Serialize(parentRow);
        }

        public static string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static List<Employee> GetListEmployeeFix()
        {
            Employee xEmployeeClass = new Employee();
            List<Employee> ListEmployee = new List<Employee>();
            for (int i=0; i<xEmployeeClass.CurentListEmployee().Rows.Count; i++)
            ListEmployee.Add(new Employee
            {
                EmployeeName = xEmployeeClass.CurentListEmployee().Rows[i][0].ToString(),
                Jobtitle = xEmployeeClass.CurentListEmployee().Rows[i][0].ToString(),
                Salary = xEmployeeClass.CurentListEmployee().Rows[i][0].ToString()
            }); 
            return ListEmployee;

        }


        public static List<Employee> ConvertDataTable<Employee>(DataTable dt)
        {
            List<Employee> data = new List<Employee>();
            foreach (DataRow row in dt.Rows)
            {
                Employee item = GetItem<Employee>(row);
                data.Add(item);
            }
            return data;
        }
        private static Employee GetItem<Employee>(DataRow dr)
        {
            Type temp = typeof(Employee);
            Employee obj = Activator.CreateInstance<Employee>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

    }
}