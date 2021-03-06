﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;  
using System.Xml.Linq;

namespace WindowsFormsApplication1
{
  class DatabaseConnectivity
  { 
    public SqlConnection Connection;
    public SqlDataAdapter DataAdapter;
    public SqlCommandBuilder CommandBuilder;

    public void Initialize(string sqlcommand, string connectionstring) {
      try
	{
	  this.Connection = new SqlConnection(connectionstring);
	  this.DataAdapter = new SqlDataAdapter(sqlcommand,connectionstring);
	  this.CommandBuilder = new SqlCommandBuilder(DataAdapter);
	}
      catch (Exception e) {
	System.Console.WriteLine(e.ToString());
      }
    }

    public SqlDataAdapter datafrom()
    {
      return this.DataAdapter;
    }

    public void datato(SqlDataAdapter& datato)
    {
      this.DataAdapter=datato;

    }

  }
}
