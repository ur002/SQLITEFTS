
using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
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

        static string _DATABASE_NAME = "FTS.db3";
        static string _FTS_TABLE = "FTS";
        static string _workpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FTS_tool");
        static string _dbfullpath = Path.Combine(_workpath, _DATABASE_NAME);
        private SQLiteConnection sqlcon;
        private List<SRCResult> _FoundFiles = new List<SRCResult>();
        private List<SRCResult> _FilesInDB = new List<SRCResult>();
        private bool _dosrc = false;
        FastColoredTextBox _tb = new FastColoredTextBox();
        private List<int> _srcw = new List<int>();
        private int _srcw_curr_pos = 0;



        public Form1()
        {
            InitializeComponent();
        }

        private void badd_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.Cancel)
            {
                badd.Enabled = false;
                txtpath4src.Text = fbd.SelectedPath;
                Task.Run(() => add2index(chUPDContentIfHashDiff.Checked));
            }
        }

        private void add2index(bool updcontent = false)
        {
            SetStatus("Scanning for files");
            var files = Directory.GetFiles(txtpath4src.Text, txtmask.Text, System.IO.SearchOption.AllDirectories);
            if (files.Length == 0) return;

            SetPBVisible(true);
            SetPBMax(files.Length);

            SetStatus("Scan complite");
            string qry = "";
            int i = 0;
            foreach (string fname in files)
            {
                SetStatus($"Add '{fname}' to index.");
                if (File.Exists(fname))
                    try
                    {
                        SRCResult FndData = _FilesInDB.FirstOrDefault(x => x.FileName == fname);
                        int rowid = FndData != null ? FndData.FileID : -1;
                        string FileHash = TMD5.ComputeFilesMD5(fname);
                        string runqry = "";
                        if ((updcontent && FileHash != FndData?.FileHash) || rowid == -1)

                            using (SQLiteTransaction myTransaction = sqlcon.BeginTransaction())
                            {
                                using (SQLiteCommand servCommand = new SQLiteCommand(sqlcon))
                                {
                                    SQLiteParameter FileName = new SQLiteParameter();
                                    if (rowid != -1)
                                    {
                                        sql3runquery($"delete from {_FTS_TABLE} where fileid={rowid}", ref sqlcon);
                                        sql3runquery($"delete from {_FTS_TABLE}_Files where fileid={rowid}", ref sqlcon);
                                    }

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

                                    servCommand.CommandText = $"Update {_FTS_TABLE}_Files set FileHash='(?)' where FileID=(?)";
                                    servCommand.Parameters.Add(new SQLiteParameter("FileHash", FileHash));
                                    servCommand.Parameters.Add(new SQLiteParameter("FileID", rowid));
                                    var result3 = servCommand.ExecuteNonQueryAsync();
                                    if (result3.IsFaulted)
                                    {
                                        Invoke((MethodInvoker)delegate
                                        {
                                            rlog.Visible = true;
                                            rlog.AppendText($"Update FileHash fault:{result3.Exception.InnerException.Message}");
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
                            badd.Enabled = true;
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
            SetPBVisible(false);
            Task.Run(() => initDBFiles());
            Invoke((MethodInvoker)delegate
            {
                badd.Enabled = true;
            });
        }

        private void SetPBVisible(bool v)
        {
            Invoke((MethodInvoker)delegate
            {
                pb1.Visible = v;

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
                    res = Convert.ToInt32(rowid.Result ?? -1);
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

        private void UpdateHeader(string statustext)
        {
            Invoke((MethodInvoker)delegate
            {
                this.Text = statustext;
            });
        }

        private void SetPBMax(int value)
        {
            Invoke((MethodInvoker)delegate
            {
                pb1.Maximum = value;
            });
        }

        private void UPDPB1(int value)
        {
            Invoke((MethodInvoker)delegate
            {
                pb1.Value = value;
            });

        }


        private void UPDFNDText(string result,string ext)
        {
            Invoke((MethodInvoker)delegate
            {
                _tb.Text = result;
                _tb.ClearStylesBuffer();
                _tb.Range.ClearStyle(StyleIndex.All);
                _tb.Language = Language.CSharp;
                switch (ext)
                {

                    case ".vb": _tb.Language = Language.VB; break;
                    case ".hml": _tb.Language = Language.HTML; break;
                    case ".xml": _tb.Language = Language.XML; break;
                    case ".sql": _tb.Language = Language.SQL; break;
                    case ".php": _tb.Language = Language.PHP; break;
                    case ".js": _tb.Language = Language.JS; break;
                    case ".lua": _tb.Language = Language.Lua; break;
                }
                _tb.OnSyntaxHighlight(new TextChangedEventArgs(_tb.Range));
               
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
                qry = $"CREATE TABLE IF NOT EXISTS {_FTS_TABLE}_Files (FileID INTEGER PRIMARY KEY,FileName TEXT UNIQUE,FileHash TEXT); ";
                sql3runquery(qry, ref sqlcon);

            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)delegate
                {
                    rlog.Visible = true;
                    rlog.AppendText("createdb." + ex.Message + Environment.NewLine);
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

        private string sql3scalar(string qry, ref SQLiteConnection conn)
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
            return Convert.ToInt64((ABytes + 1023) / 1024).ToString() + " KB";
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
            var res = Task.Run(() => initDBFiles());

        }

        void initDBFiles()
        {
            _FilesInDB.Clear();
            SetStatus("Load Data from DB");
            string qry = $"Select FileID ,FileName ,FileHash from { _FTS_TABLE}_Files";

            try
            {
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = sqlcon;

                cmd.CommandText = qry;
                SQLiteDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    SRCResult f = new SRCResult();
                    f.FileID = Convert.ToInt32(r["FileID"]);
                    f.FileName = r["FileName"].ToString();
                    f.FileHash = r["FileHash"].ToString();
                    _FoundFiles.Add(f);
                }
            }
            catch (Exception ex)
            {

                Invoke((MethodInvoker)delegate
                {
                    rlog.Visible = true;
                    rlog.AppendText("HardFileHashRecalc." + ex.Message + Environment.NewLine);
                    rlog.Height = 55;
                });
            }
            UPDPB1(0);
            SetPBVisible(false);
            SetStatus("Ready for work");
            UpdateHeader($"FTS5 Engine. Files in DB:{_FoundFiles.Count}");
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
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlcon.State == ConnectionState.Open) sqlcon.Close();
            sqlcon.Dispose();
        }

        private void txtText4SEarch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtText4SEarch.Text.Length == 0) return;
                _dosrc = true;
                this.Text = $"FTS5 Engine";
                lfilrs.DataSource = null;
                lfilrs.DisplayMember = "FileName";
                lfilrs.ValueMember = "FileID";
                var res = Task.Run(async () => SrcFiles(txtText4SEarch.Text));
                Task.WaitAll();
                GC.Collect();
                _dosrc = false;
            }

        }

        private void SrcFiles(string text)
        {
            _FoundFiles.Clear();
            string qry = "";
            try
            {
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = sqlcon;
                qry = $"SELECT {_FTS_TABLE}_Files.FileID,{_FTS_TABLE}_Files.FileName,{_FTS_TABLE}_Files.FileHash   FROM {_FTS_TABLE} left join {_FTS_TABLE}_Files on {_FTS_TABLE}.fileid={_FTS_TABLE}_Files.FileID WHERE {_FTS_TABLE} MATCH '{text}' ";
                if (txtmask.Text != "*.*") qry += $"  and filename like '{txtmask.Text.Replace("*", "%")}' ";
                cmd.CommandText = qry;
                SQLiteDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    SRCResult f = new SRCResult();
                    f.FileID = Convert.ToInt32(r["FileID"]);
                    f.FileName = r["FileName"].ToString();
                    f.FileHash = r["FileHash"].ToString();
                    // f.content = r["content"]?.ToString() ?? "null";//.ToString();
                    _FoundFiles.Add(f);
                }

            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)delegate
                {
                    rlog.Visible = true;
                    rlog.AppendText("SrcFiles." + ex.Message + Environment.NewLine);
                    rlog.AppendText($"qry={qry}" + Environment.NewLine);

                    rlog.Height = 55;
                });
            }
            finally
            {
                Invoke((MethodInvoker)delegate
                {

                    lfilrs.DataSource = _FoundFiles;
                    this.Text = $"FTS5 Engine:{_FoundFiles.Count} files count";
                });
            }

        }


        void GetText(SRCResult item)
        {
            try
            {

                string ext = Path.GetExtension(Path.GetExtension(item.FileName)).ToLower();
                string result = "";

                if (File.Exists(item.FileName))
                {
                    if (chdatafromDB.Checked)
                    {
                        if (item.FileHash != TMD5.ComputeFilesMD5(item.FileName))
                        {
                            if (MessageBox.Show("Данные в файле отличаются от сохраненной копии, загрузить данные из файла?", "Найдено измененное содержимое файла", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                result = File.ReadAllText(item.FileName);
                            }
                        }

                        if (result == "")
                        {
                            SQLiteCommand cmd = new SQLiteCommand();
                            cmd.Connection = sqlcon;
                            cmd.CommandText = $"SELECT content  FROM {_FTS_TABLE} where {_FTS_TABLE}.fileid={item.FileID};";
                            SQLiteDataReader r = cmd.ExecuteReader();
                            while (r.Read())
                            {
                                result = r[0].ToString();
                            }
                        }
                    }
                    else
                        result = File.ReadAllText(item.FileName);
                }
                else
                {
                    Invoke((MethodInvoker)delegate
                    {
                        rlog.Visible = true;
                        rlog.AppendText($"File not found {item.FileName}" + Environment.NewLine);
                        rlog.Height = 55;
                    });
                }
                UPDFNDText(result, ext);
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

            Invoke((MethodInvoker)delegate
            {
                try
                {
                    _srcw_curr_pos = 0;
                    var ll = _tb.SelectNext(txtText4SEarch.Text, false, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    _srcw = _tb.FindLines(txtText4SEarch.Text, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    selword(_srcw_curr_pos);
                }
                catch (Exception ex)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        rlog.Visible = true;
                        rlog.AppendText("SelectNext." + ex.Message + Environment.NewLine);
                        rlog.Height = 55;
                    });
                }

            });
        }

        private void lfilrs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dosrc) return;
            SRCResult selitem = lfilrs.SelectedItem as SRCResult;
            var res = Task.Run(() => GetText(selitem));
            Task.WaitAll();

        }
    

        private void bprev_Click(object sender, EventArgs e)
        {
            if (_srcw_curr_pos>0)
                _srcw_curr_pos--;
            selword(_srcw_curr_pos);

        }

        void selword(int linenumber)
        {
            if (linenumber == 0) return;
            _tb.PlaceToPoint(new Place(1, _srcw[linenumber]));
            _tb.Navigate(_srcw[linenumber]);        
            _tb.Selection.Start = new Place(1, _srcw[linenumber]);
            _tb.Selection.End = new Place(_tb.Lines[_srcw[linenumber]].Length, _srcw[linenumber]);
        }

        private void bnext_Click(object sender, EventArgs e)
        {
            if (_srcw_curr_pos < _srcw.Count-1)
                _srcw_curr_pos++;
            selword(_srcw_curr_pos);
        }

        private void rlog_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Process.Start("explorer.exe", @"https://www.sqlite.org/fts5.html#full_text_query_syntax");
        }

        void HardFileHashRecalc()
        {
            SetStatus("Recalc hash of files in progress");
            string qry = $" select FileID ,FileName ,FileHash from { _FTS_TABLE}_Files";

            try
            {
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = sqlcon;

                cmd.CommandText = qry;
                SQLiteDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    SRCResult f = new SRCResult();
                    f.FileID = Convert.ToInt32(r["FileID"]);
                    f.FileName = r["FileName"].ToString();
                    f.FileHash = r["FileHash"].ToString();
                    _FoundFiles.Add(f);
                }

                int i = 0;
                SetPBVisible(true);
                SetPBMax(_FoundFiles.Count);
                foreach (var fl in _FoundFiles)
                {
                    try
                    {
                        if (File.Exists(fl.FileName))
                        {
                            string filehash = TMD5.ComputeFilesMD5(fl.FileName);
                            qry = $"update {_FTS_TABLE}_Files set FileHash='{filehash}' where FileID={fl.FileID}";
                            sql3runquery(qry, ref sqlcon);
                        }
                        UPDPB1(i);
                        i++;
                    }
                    catch (Exception ex)
                    {

                        Invoke((MethodInvoker)delegate
                        {
                            rlog.Visible = true;
                            rlog.AppendText("HardFileHashRecalc." + ex.Message + Environment.NewLine);
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
                    rlog.AppendText("HardFileHashRecalc." + ex.Message + Environment.NewLine);
                    rlog.Height = 55;
                });
            }
            UPDPB1(0);
            SetPBVisible(false);
            SetStatus("Ready for work");
        }


        private void bUPDHASH_Click(object sender, EventArgs e)
        {
            var res = Task.Run(() => HardFileHashRecalc());
        }
    }


    class SRCResult {

        public int FileID { get; set; }
        public string FileName { get; set; }
        public string content { get; set; }
        public string FileHash { get; set; }
    }

}
