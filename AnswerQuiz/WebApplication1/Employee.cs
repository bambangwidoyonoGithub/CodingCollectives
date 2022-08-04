using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebApplication1
{
    public class Employee
    {
        public string EmployeeName { get; set; }
        public string Jobtitle { get; set; }
        public string Salary { get; set; }

        public DataTable CurentListEmployee()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.TableName = "ListEmployee";
            dt.Columns.Add("EmployeeName");
            dt.Columns.Add("Jobtitle");
            dt.Columns.Add("Salary");
            DataRow xrow = dt.NewRow();
            dt.Rows.Add("Mr A", "CEO", "200000000");
            dt.Rows.Add("Mr B", "PM", "150000000");
            dt.Rows.Add("Mrs C", "BA", "100000000");
            dt.Rows.Add("Mr D", "QC", "70000000");
            dt.Rows.Add("Mr E", "Dev1", "170000000");
            dt.Rows.Add("Mr F", "Dev2", "110000000");
            return dt;
        }

    }
}