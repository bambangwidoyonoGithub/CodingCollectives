using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.User.Identity.IsAuthenticated)
            {
                Response.Redirect(FormsAuthentication.DefaultUrl);
            }

        }

        public Boolean IsValidEmail() {
            string email = txtUsername.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success) { 
                return true;
            }else
            {
                dvMessage.Visible = true;
                lblMessage.Text = "Username just using email";
                return false;
            }
        }

        protected void ValidateUser(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            if (IsValidEmail())
            {
                UserAccount xUserAccount = IsUserExist(username, password);
                if (xUserAccount.IsExist)
                {
                    if (xUserAccount.IsActive)
                    {
                        Session["UserName"] = username;
                        //FormsAuthentication.SetAuthCookie(username, chkRememberMe.Checked);
                        Response.Redirect("Index.aspx");
                    }
                    else
                    {
                        dvMessage.Visible = true;
                        lblMessage.Text = "Account has not been activated.";
                    }
                }
                else
                {
                    dvMessage.Visible = true;
                    lblMessage.Text = "Username and/or password is incorrect.";
                }
            }
        }


        private string CreateEncodeDecodeBase64(string xValue, Boolean isToEncode)
        {
            string xResult = "";
            if (isToEncode)
            {
                var ByteEncodeValues = System.Text.Encoding.UTF8.GetBytes(xValue);
                string EncodeValues = System.Convert.ToBase64String(ByteEncodeValues);
                xResult = EncodeValues;
            }
            else
            {
                var ByteDecodeValues = System.Convert.FromBase64String(xValue);
                string DecodeValues = System.Text.Encoding.UTF8.GetString(ByteDecodeValues);
                xResult = DecodeValues;

            }
            return xResult;
        }


        private UserAccount IsUserExist(string xUser, string xPassword) {
            UserAccount xResult = new UserAccount();
            xResult.IsExist = false;
            xResult.IsActive = false;
            try
            {
                string xInputEncode = CreateEncodeDecodeBase64(xUser + ';' + xPassword, true);
                string AppDir = AppDomain.CurrentDomain.BaseDirectory;
                string xPathFileLocation = AppDir+"/"+System.Configuration.ConfigurationManager.AppSettings["ListUser"];
                if (File.Exists(xPathFileLocation))
                {
                    string[] lines = File.ReadAllLines(xPathFileLocation);
                    foreach (string line in lines) {
                        string xEncodeUserExisting = CreateEncodeDecodeBase64(line.Substring(2, line.Length - 2), true);
                        if (xEncodeUserExisting == xInputEncode) {
                            xResult.IsExist = true;
                            if (line.Substring(0, 1).ToString() == "1" || line.Substring(0, 1).ToString() == "+") {
                                xResult.IsActive = true;
                            }
                            return xResult;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                //ignore

            }
            return xResult;
        }

    }
}