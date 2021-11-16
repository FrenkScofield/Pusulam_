using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace PusulamRapor
{
    public class Baglanti : IDisposable
    {

        public string ConString { get; set; }

        public SqlConnection sqlBaglanti { get; set; }

        public SqlCommand sqlKomut { get; set; }

        private Dictionary<string, object> parametreler;
        public Dictionary<string, object> Parametreler
        {
            get { return parametreler; }
            set { parametreler = value; }
        }

        public Baglanti()
        {
            bool isTest = ConfigurationManager.AppSettings["IsTest"] == null ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["IsTest"]);
            ConString = ConfigurationManager.ConnectionStrings["pusulamCS"].ConnectionString;

            if (isTest)
            {
                ConString = ConfigurationManager.ConnectionStrings["pusulamTest"].ConnectionString;
            }
                      
            sqlBaglanti = new SqlConnection { ConnectionString = ConString };
            parametreler = new Dictionary<string, object>();
        }

        public void ParametreEkle(string ad, object ob)
        {
            if (parametreler.ContainsKey(ad)) // zaten varsa
            {
                parametreler[ad] = ob;
            }
            else
            {
                parametreler.Add(ad, ob);
            }
        }

        public void Dispose()
        {
            this.ConString = string.Empty;

            if (this.sqlBaglanti != null)
            {
                this.sqlBaglanti.Dispose();
                this.sqlBaglanti = null;
            }

            if (this.sqlKomut != null)
            {
                this.sqlKomut.Dispose();
                this.sqlKomut = null;
            }

        }

        private SqlParameter[] GetirParametreDizisi()
        {
            SqlParameter[] sqlpd = new SqlParameter[parametreler.Count];

            int i = 0;
            foreach (KeyValuePair<string, object> nv in parametreler)
            {
                if (nv.Key.Contains("OO@"))
                {
                    SqlParameter sp = new SqlParameter(nv.Key.Replace("OO@", "@"), nv.Value);
                    sp.Direction = ParameterDirection.Output;
                    sqlpd[i] = sp;
                }
                else if (nv.Key.Contains("OU@"))
                {
                    SqlParameter sp = new SqlParameter(nv.Key.Replace("OU@", "@"), nv.Value);
                    sp.Direction = ParameterDirection.InputOutput;
                    sqlpd[i] = sp;
                }
                else if (nv.Key.Contains("RT@"))
                {
                    SqlParameter sp = new SqlParameter(nv.Key.Replace("RT@", "@"), nv.Value);
                    sp.Direction = ParameterDirection.ReturnValue;
                    sqlpd[i] = sp;
                }
                else
                {
                    sqlpd[i] = new SqlParameter(nv.Key, nv.Value);
                }
                i++;
            }

            return sqlpd;
        }

        public DataTable SorguGetir(string komut, CommandType ct)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(komut, sqlBaglanti))
                {

                    da.SelectCommand.CommandType = ct;
                    da.SelectCommand.CommandTimeout = 9999;
                    da.SelectCommand.Parameters.AddRange(GetirParametreDizisi());
                    da.Fill(dt);
                }
                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public DataSet SorguGetir(string komut)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(komut, sqlBaglanti);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = 9999;
            da.SelectCommand.Parameters.AddRange(GetirParametreDizisi());
            da.Fill(ds);
            return ds;
        }

        public DataSet SorguGetir(string komut, SqlParameter[] sqlp)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(komut, sqlBaglanti);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddRange(sqlp);
            da.Fill(ds);
            return ds;
        }

    }
}
