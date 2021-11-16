using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Pusulam
{
    public class Baglanti : IDisposable
    {

        public string ConString { get; set; }

        public SqlConnection sqlBaglanti { get; set; }

        public SqlCommand sqlKomut { get; set; }

        public SqlDataReader sqlVeriOkuyucu { get; set; }

        public SqlTransaction sqlTran { get; set; }

        public String Mesajlar { get; set; }

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
            sqlBaglanti.InfoMessage += sqlBaglanti_InfoMessage;
            Mesajlar = "";
            parametreler = new Dictionary<string, object>();

        }

        void sqlBaglanti_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            Mesajlar += e.Message + "\n";
        }


        public void TranBaslat()
        {
            if (sqlBaglanti != null)
            {
                sqlTran = sqlBaglanti.BeginTransaction();
                sqlKomut.Transaction = sqlTran;
            }
        }

        public void TranCommit()
        {
            if (sqlTran != null)
            {
                sqlTran.Commit();
            }
        }

        public void TranRollback()
        {
            if (sqlTran != null)
            {
                sqlTran.Rollback();
            }
        }

        public void ParametreEkle(string ad, object ob)
        {
            if (ob != null)
            {
                var tip = ob.GetType();
                string kontrol = ob.ToString().Replace("<script", " ").Replace("</script>", " ");
                if (tip.Name != "DataTable")
                {
                    ob = System.Convert.ChangeType(kontrol, tip);
                }
            }
            if (parametreler.ContainsKey(ad)) // zaten varsa
            {
                parametreler[ad] = ob;
            }
            else
            {
                parametreler.Add(ad, ob);
            }
        }

        public void ParametreEkle2(string ad, object ob)
        {
            if (ob != null)
            {
                var tip = ob.GetType();
                string kontrol = ob.ToString().Replace("<", "").Replace(">", "");
                if (tip.Name != "DataTable")
                {
                    ob = System.Convert.ChangeType(kontrol, tip);
                }
            }
            if (parametreler.ContainsKey(ad)) // zaten varsa
            {
                parametreler[ad] = ob;
            }
            else
            {
                parametreler.Add(ad, ob);
            }
        }

        public void ParemetreTopluEkle(Dictionary<string, string> paras)
        {
            foreach (KeyValuePair<string, string> item in paras)
            {
                if (parametreler.ContainsKey(item.Key)) // zaten varsa
                {
                    parametreler[item.Key] = item.Value;
                }
                else
                {
                    parametreler.Add(item.Key, item.Value);
                }
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

        public void Ac()
        {
            sqlBaglanti.Open();
            sqlKomut = new SqlCommand();
            sqlKomut.Connection = sqlBaglanti;
        }

        public void Kapat()
        {
            sqlBaglanti.Close();
            sqlBaglanti.Dispose();
            sqlBaglanti = null;
        }

        public void YenidenBaslat()
        {
            sqlBaglanti.Close();
            sqlBaglanti.Open();
            sqlKomut = new SqlCommand { Connection = sqlBaglanti };
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

            if (this.sqlVeriOkuyucu != null)
            {
                this.sqlVeriOkuyucu.Dispose();
                this.sqlVeriOkuyucu = null;
            }

            if (this.sqlTran != null && sqlTran.Connection != null)
            {
                TranRollback();
            }
        }

        public DataTable SorguGetir(string komut, CommandType ct)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(komut, sqlBaglanti))
                {

                    da.SelectCommand.CommandType = ct;
                    da.SelectCommand.CommandTimeout = 240;
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

        public DataSet SorguGetir(string komut, CommandType ct, int bos)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(komut, sqlBaglanti);
            da.SelectCommand.CommandType = ct;
            da.SelectCommand.CommandTimeout = 240;
            da.SelectCommand.Parameters.AddRange(GetirParametreDizisi());
            da.Fill(ds);
            return ds;
        }

        public DataSet SorguGetir(string komut, CommandType ct, SqlParameter[] sqlp)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(komut, sqlBaglanti);
            da.SelectCommand.CommandType = ct;
            da.SelectCommand.CommandTimeout = 240;
            da.SelectCommand.Parameters.AddRange(sqlp);
            da.Fill(ds);
            return ds;
        }

        public int SorguGotur(string komut, int islemID)
        {
            this.ParametreEkle("@ISLEM", islemID);
            return SorguGotur(komut, CommandType.StoredProcedure);
        }

        public int SorguGotur(string komut, CommandType ct)
        {
            try
            {
                int sonuc = 0;
                sqlKomut.CommandText = komut;
                sqlKomut.Parameters.Clear();
                sqlKomut.Parameters.AddRange(GetirParametreDizisi());
                sqlKomut.CommandType = ct;
                sqlKomut.CommandTimeout = 600;
                sonuc = sqlKomut.ExecuteNonQuery();
                if (parametreler.ContainsKey("RT@RETURN"))
                    sonuc = (int)sqlKomut.Parameters["@RETURN"].Value;
                return sonuc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object TekSonucGetir(string komut)
        {
            object ob = new object();
            sqlKomut = new SqlCommand(komut, sqlBaglanti);
            sqlKomut.Parameters.AddRange(GetirParametreDizisi());
            ob = sqlKomut.ExecuteScalar();
            return ob;
        }

        public SqlDataReader VeriOkuyucuGetir(string komut, CommandType ct)
        {
            if (sqlKomut == null)
            {
                sqlKomut = new SqlCommand(komut, sqlBaglanti);
            }
            sqlKomut.CommandText = komut;
            sqlKomut.CommandType = ct;
            sqlKomut.Parameters.AddRange(GetirParametreDizisi());
            SqlDataReader sqldr = sqlKomut.ExecuteReader();
            return sqldr;
        }

        public List<string> JsonOlustur(DataSet ds)
        {
            //
            var Olist = new List<string>();
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = 2147483644;
            foreach (DataTable dt in ds.Tables)
            {
                var rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col].ToString());
                    }
                    rows.Add(row);
                    row = null;
                }
                Olist.Add(serializer.Serialize(rows));
                rows = null;
            }
            return Olist;
        }


        internal void ParametreEkle()
        {
            throw new NotImplementedException();
        }


        internal bool PushNotification(string pushMessage, string idUser, string header, string usertip, string sube, string link)
        {
            try
            {
                string mesaj = "";
                string baslik = "";

                if (pushMessage.IndexOf('-') > 0)
                {
                    baslik = pushMessage.Split('-')[0];
                    mesaj = pushMessage.Split('-')[1];
                }
                else
                {
                    baslik = "Yeni Bildirim";
                    mesaj = pushMessage;
                }

                //azure bildirim
                pushMessage = pushMessage.ToLower().Replace('ı', 'i').Replace('ö', 'o').Replace('ü', 'u').Replace('ğ', 'g').Replace('ç', 'c');
                //  AzureBildirimServisi.BildirimServisClient servis = new AzureBildirimServisi.BildirimServisClient();
                //servis.SendNotification(mesaj, "All", baslik, idUser.ToString(), "10D667B5-C3CB-4CF0-9E66-29F969E9DE24", idUser, link);
                return true;

                //parse bildirim
                //bool isPushMessageSend = false;

                //header = header.ToLower().Replace('ı', 'i').Replace('ö', 'o').Replace('ü', 'u').Replace('ğ', 'g').Replace('ç', 'c');
                //var unaccentedText = String.Join("", pushMessage.Normalize(NormalizationForm.FormD).Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
                //var unaccentedheader = String.Join("", header.Normalize(NormalizationForm.FormD).Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));

                //string urlpath = "https://api.parse.com/1/push";

                //var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlpath);


                //string postString = "{\"data\": { \"alert\":\"" + unaccentedText + "\" ,\"badge\": \"" + link + "\", \"title\": \"" + unaccentedheader + "\" },  \"channels\": [\"" + usertip + "\",\"" + sube + "\",\"" + idUser + "\" ] }";

                //httpWebRequest.ContentType = "application/json";
                //httpWebRequest.ContentLength = postString.Length;
                //httpWebRequest.Headers.Add("X-Parse-Application-Id", "ntg8Jgw3g06oS8FAAgkbeoaBGUqHl7ZD3jP12LjY");
                //httpWebRequest.Headers.Add("X-Parse-REST-API-KEY", "mtFZnmXtuTOS5G4kjyslLUqH9e7uCDQi4wVGxg0F");


                //httpWebRequest.Method = "POST";

                //StreamWriter requestWriter = new StreamWriter(httpWebRequest.GetRequestStream());

                //requestWriter.Write(postString);

                //requestWriter.Close();

                //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                //{

                //    var responseText = streamReader.ReadToEnd();
                //    JObject jObjRes = JObject.Parse(responseText);
                //    if (Convert.ToString(jObjRes).IndexOf("true") != -1)
                //    {
                //        isPushMessageSend = true;
                //    }
                //}

                //return isPushMessageSend;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


    }
}