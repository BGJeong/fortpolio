﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class planview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetViewItems();
        }
    }
    protected void GetViewItems()
    {
        string id = Request.QueryString["param"];
        string Userid = null;
        string is_Login;

        string connectionString = "server =(local) ; uid=Basic; pwd=1234; database=Basic;";
        SqlConnection scon = new SqlConnection(connectionString);
        SqlCommand scom = new SqlCommand();

        scom.Connection = scon;
        scom.CommandText = "SELECT uid,title,lat,lng,addr FROM planner WHERE id = '" + id + "'";
        //scom.CommandText = "SELECT Name,PostDate,Title,Content FROM board";
        scon.Open();

        SqlDataReader sdr = scom.ExecuteReader();

        while (sdr.Read())
        {
            LaB_Writer.Text = sdr["uid"].ToString();
            LaB_Title.Text = sdr["title"].ToString();
            LaB_lat.Text = sdr["lat"].ToString();
            LaB_lng.Text = sdr["lng"].ToString();
            LaB_addr.Text = sdr["addr"].ToString();
            Userid = sdr["uid"].ToString();
        }

        if (Session["uid"] != null)
        {
            if (Session["uid"].ToString() == Userid)
            {
                is_Login = "true";
            }
            else
            {
                is_Login = "false";
            }
        }
        else {
            is_Login = "false";
        }


        sdr.Close();
        scon.Close();
    }

    protected void Logout(object sender, EventArgs e)
    {
        Session["uid"] = null;
        Response.Redirect("/Main.aspx");
    }

    protected void btn_List_Click(object sender, EventArgs e)
    {
        Response.Redirect("planboard/list.aspx");
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        int num = int.Parse(Request["param"].ToString());

        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = "server =(local) ; uid=Basic; pwd=1234; database=Basic;";
        conn.Open();


        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = "Delete from planner where id like " + num;

        cmd.ExecuteNonQuery();

        conn.Close();

        Response.Redirect("planboard/list.aspx");
    }

    protected void Modify_Click(object sender, EventArgs e)
    {
        string id = Request.QueryString["param"];

        Response.Redirect("planboard/modify.aspx?param=" + id);
    }
}