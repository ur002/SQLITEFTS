using FarsiLibrary.Win;
using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLITEFTS
{
    public partial class Form1 : Form
    {

       static string  _DATABASE_NAME = "FTS.db3";
       static string _FTS_TABLE = "FTS";
       static string _workpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FTS_tool");
       static string _dbfullpath = Path.Combine(_workpath, _DATABASE_NAME);
        private SQLiteConnection sqlcon;
        private List<SRCResult> FoundFiles = new List<SRCResult>();
        private bool _load = false;
        private bool _dosrc = false;
         FastColoredTextBox _tb = new FastColoredTextBox();



        public Form1()
        {
            InitializeComponent();
        }

        private void badd_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.Cancel)
            {
                txtpath4src.Text = fbd.SelectedPath;                
                Task.Run(async () => add2index());
            }
        }

        private void add2index()
        {
            SetStatus("Scan for files");
            var files = Directory.GetFiles(txtpath4src.Text, txtmask.Text, System.IO.SearchOption.AllDirectories);
            if (files.Length == 0) return;
            Invoke((MethodInvoker)delegate
            {
                pb1.Visible = true;
                pb1.Maximum = files.Length;
            });
            SetStatus("Scan complite");
            string qry = "";
            int i = 0;
            foreach (string fname in files)
            {
                SetStatus($"Add '{fname}' to index.");
                if (File.Exists(fname) && GetRowID(fname)==-1)
                    try
                    {
                     using (SQLiteTransaction myTransaction = sqlcon.BeginTransaction())
                        {
                            using (SQLiteCommand servCommand = new SQLiteCommand(sqlcon))
                            {
                                SQLiteParameter FileName = new SQLiteParameter();
                                

                                servCommand.CommandText = $"INSERT INTO {_FTS_TABLE}_Files (FileName) VALUES (?)";
                                FileName.Value = fname;
                                

                                servCommand.Parameters.Add(FileName);
                                var result = servCommand.ExecuteNonQueryAsync();
                                if (result.IsFaulted)
                                {
                                    Invoke((MethodInvoker)delegate
                                    {
                                        rlog.Visible = true;
                                        rlog.AppendText($"InsertFileName fault:{result.Exception.InnerException.Message}");
                                        rlog.Height = 55;
                                    });
                                    break;
                                }
                                long rid = servCommand.Connection.LastInsertRowId;
                                int newrowid = GetRowID(fname);
                                SQLiteCommand insCommand = new SQLiteCommand(sqlcon);
                                insCommand.CommandText = $"INSERT OR REPLACE INTO {_FTS_TABLE} (fileid, content) VALUES(?, ?)";

                                SQLiteParameter FileData = new SQLiteParameter();
                                SQLiteParameter FileID = new SQLiteParameter();
                                FileData.Value = File.ReadAllText(fname);
                                FileID.Value = newrowid;
                                insCommand.Parameters.Add(FileID);
                                insCommand.Parameters.Add(FileData);

                                result = insCommand.ExecuteNonQueryAsync();
                                if (result.IsFaulted)
                                {
                                    Invoke((MethodInvoker)delegate
                                    {
                                        rlog.Visible = true;
                                        rlog.AppendText($"InsertFileName fault:{result.Exception.InnerException.Message}");
                                        rlog.Height = 55;
                                    });
                                    break;
                                }


                            }
                            myTransaction.Commit();
                        }



                    }
                    catch (Exception ex)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            rlog.Visible = true;
                            rlog.AppendText("Query ERR " + ex.Message + Environment.NewLine + qry);
                            rlog.Height = 55;
                        });

                    }
                SetStatus(i + "/" + files.Length);
                UPDPB1(i);
                i++;
                


            }
            SQLiteCommand srvCommand = new SQLiteCommand(sqlcon);
            srvCommand.CommandText = $"INSERT INTO {_FTS_TABLE} VALUES('optimize','optimize')";
            var res = srvCommand.ExecuteNonQueryAsync();
            if (res.IsFaulted)
            {
                Invoke((MethodInvoker)delegate
                {
                    rlog.Visible = true;
                    rlog.AppendText($"optimize fault:{res.Exception.InnerException.Message}");
                    rlog.Height = 55;
                });

            }
            SetStatus("Ожидание");
            UPDPB1(0);
            GC.Collect();
            Invoke((MethodInvoker)delegate
            {
                pb1.Visible = false;                
            });
        }

        private int GetRowID(string fname)
        {

            int res = -1;

            if (sqlcon.State == ConnectionState.Closed) SQLiteinit();


            using (SQLiteTransaction myTransaction = sqlcon.BeginTransaction())
            {
                using (SQLiteCommand servCommand = new SQLiteCommand(sqlcon))
                {
                    SQLiteParameter FileNameParam = new SQLiteParameter();

                    servCommand.CommandText = $"SELECT RowID FROM {_FTS_TABLE}_Files WHERE FileName=?";
                    FileNameParam.Value = fname;
                    servCommand.Parameters.Add(FileNameParam);

                   var rowid = servCommand.ExecuteScalarAsync();
                    res = Convert.ToInt32(rowid.Result?? -1);
                }
                myTransaction.Commit();
            }

            return res;
        }
        private void SetStatus(string statustext)
        {
            Invoke((MethodInvoker)delegate
            {
                mainstatuslabel.Text = statustext;
            });
        }

        private void UPDPB1(int value)
        {
            Invoke((MethodInvoker)delegate
            {
                pb1.Value = value;
            });

        }
        private void createdb(ref SQLiteConnection sqlcon)
        {
            if (!Directory.Exists(_workpath))
            {
                Directory.CreateDirectory(_workpath);
            }


            SetStatus("Creating new tables ...");
            
            try
            { 
                //SQLiteConnection.CreateFile(_dbfullpath);
                string qry = $"CREATE VIRTUAL TABLE IF NOT EXISTS { _FTS_TABLE} " +
                "USING FTS5(fileid, content, tokenize = 'porter')";
                sql3runquery(qry, ref sqlcon);
                qry = $"CREATE TABLE IF NOT EXISTS {_FTS_TABLE}_Files (FileID INTEGER PRIMARY KEY,FileName TEXT UNIQUE); ";
                sql3runquery(qry, ref sqlcon);

            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)delegate
                {
                    rlog.Visible = true;
                    rlog.AppendText("createdb."+ex.Message + Environment.NewLine );
                    rlog.Height = 55;
                });
            }
        }

        private void sql3runquery(string qry, ref SQLiteConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
            {
                SQLiteinit();
            }

            try
            {
                var command = new SQLiteCommand(qry, conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)delegate
                {
                    rlog.Visible = true;
                    rlog.AppendText("Query ERR " + ex.Message + Environment.NewLine + qry);
                    rlog.Height = 55;
                });
            }
        }

        private string sql3scalar(string qry,ref SQLiteConnection conn)
        {
            string r = "0";
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    SQLiteinit();
                }

                // conn.Open();
                var command = new SQLiteCommand(qry, conn);
                r = Convert.ToString(command.ExecuteScalar());
                conn.Close();
                //if (r == System.DBNull.Value) r = "null";

                //  r = Convert.IsDBNull(r);
                if (r == "") r = "null";

                return r;
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)delegate
                {
                    rlog.Visible = true;
                    rlog.AppendText("Query ERR " + ex.Message + Environment.NewLine + qry);
                    rlog.Height = 55;
                });
            }

            return r;
        }


        public static string ToDateSQLite(DateTime value)
        {
            string format_date = "yyyy-MM-dd HH:mm:ss.fff";
            return value.ToString(format_date);
        }

        private string UnicodeToUTF8(string strFrom)
        {
            Encoding utf8 = Encoding.GetEncoding("utf-8");
            Encoding win1251 = Encoding.GetEncoding("windows-1251");
            byte[] utf8Bytes = win1251.GetBytes(strFrom);
            byte[] win1251Bytes = Encoding.Convert(win1251, utf8, utf8Bytes);
            strFrom = win1251.GetString(win1251Bytes);
            return strFrom;
        }

        private string Win1251ToUTF8(string source)
        {
            Encoding utf8 = Encoding.GetEncoding("utf-8");
            Encoding win1251 = Encoding.GetEncoding("windows-1251");

            byte[] utf8Bytes = win1251.GetBytes(source);
            byte[] win1251Bytes = Encoding.Convert(win1251, utf8, utf8Bytes);
            source = win1251.GetString(win1251Bytes);
            return source;
        }

        string KiloByteText(long ABytes)
        {
        return Convert.ToInt64((ABytes + 1023)/1024).ToString() + " KB";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SQLiteinit();
           
            _tb.Font = new Font("Consolas", 9.75f);
            _tb.Dock = DockStyle.Fill;
            _tb.BorderStyle = BorderStyle.None;
            _tb.VirtualSpace = true;
            _tb.LeftPadding = 17;
            _tb.Language = Language.CSharp;
            pntext.Controls.Add(_tb);
            _tb.Text = "";
            _tb.Focus();
            _tb.ReadOnly = true;

        }

        void SQLiteinit()
        {

            var cs = string.Format("Data Source={0};Version=3;", _dbfullpath);
            var sqLiteConnectionStringBuilder = new SQLiteConnectionStringBuilder(cs) { JournalMode = SQLiteJournalModeEnum.Wal };
            cs = sqLiteConnectionStringBuilder.ToString();
            var path = Environment.Is64BitProcess ? "x64" : "x86";
            var dllFullFileName = Path.Combine(path, "SQLite.Interop.dll");

            sqlcon = new SQLiteConnection(cs);

            sqlcon.Open();
            sqlcon.EnableExtensions(true);
            sqlcon.LoadExtension(dllFullFileName, "sqlite3_fts5_init");
            //if (!File.Exists(Path.Combine(_workpath, _DATABASE_NAME)))
            createdb(ref sqlcon);
            SetStatus("Ready to work");



        }

     

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlcon.State == ConnectionState.Open)  sqlcon.Close();
            sqlcon.Dispose();
        }

        private void txtText4SEarch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter )
            {
               _dosrc = true;
                lfilrs.DataSource = null;
                lfilrs.DisplayMember = "FileName";
               var res = Task.Run(async () => SrcFiles(txtText4SEarch.Text));
               Task.WaitAll();                
               GC.Collect();

                _dosrc = false;
            }

        }

        private void SrcFiles(string text)
        {
            FoundFiles.Clear();
            string qry = "";
            try
            {
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = sqlcon;
                qry = $"SELECT {_FTS_TABLE}_Files.FileID,{_FTS_TABLE}_Files.FileName FROM {_FTS_TABLE} left join {_FTS_TABLE}_Files on {_FTS_TABLE}.fileid={_FTS_TABLE}_Files.FileID WHERE {_FTS_TABLE} MATCH '{text}';";
                cmd.CommandText = qry;
                SQLiteDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    SRCResult f = new SRCResult();
                    f.FileID = Convert.ToInt32(r["FileID"]);
                    f.FileName = r["FileName"].ToString();
                    // f.content = r["content"]?.ToString() ?? "null";//.ToString();
                    FoundFiles.Add(f);
                }

                Invoke((MethodInvoker)delegate
                {
                    lfilrs.DataSource = FoundFiles;
                    this.Text = $"FTS5 Engine:{FoundFiles.Count} files count";
                });
            }
            catch(Exception ex)
            {
                Invoke((MethodInvoker)delegate
                {
                    rlog.Visible = true;
                    rlog.AppendText("SrcFiles." + ex.Message + Environment.NewLine);
                    rlog.AppendText($"qry={qry}"+ Environment.NewLine);

                  rlog.Height = 55;
                });
            }
            
        }

        private void lfilrs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dosrc) return;
            try
            {
                SRCResult selitem = lfilrs.SelectedItem as SRCResult;
                if (chdatafromDB.Checked)
                {


                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = sqlcon;
                    cmd.CommandText = $"SELECT content  FROM {_FTS_TABLE} where {_FTS_TABLE}.fileid={selitem.FileID};";
                    SQLiteDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        //      txtdata.Text = r["content"].ToString();
                        //CreateTab(selitem.FileName, r[1].ToString());

                        //txtdata.Text = "";

                        _tb.Text = r[0].ToString();



                    }
                }
                else
                {
                    if (File.Exists(selitem.FileName))
                        _tb.Text = File.ReadAllText(selitem.FileName);
                    else
                    {

                        Invoke((MethodInvoker)delegate
                        {
                            rlog.Visible = true;
                            rlog.AppendText($"File not found {selitem.FileName}" + Environment.NewLine);
                            rlog.Height = 55;
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)delegate
                {
                    rlog.Visible = true;
                    rlog.AppendText("SrcFiles." + ex.Message + Environment.NewLine);
                    rlog.Height = 55;
                });
            }
        }
    }

    class SRCResult {

        public int FileID { get; set; }
        public string FileName { get; set; }
        public string content { get; set; }
    }

}
