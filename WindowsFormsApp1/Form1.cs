using Fizzler.Systems.HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadSearches();
        }

        private void LoadSearches()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\eSearch\";
            string[] files = Directory.GetFiles(path, "*.txt");
            var buttonArray = new Button[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i];
                var filename = Path.GetFileName(file);
                buttonArray[i] = new Button
                {
                    Text = filename,
                    ForeColor = Color.Black,
                    BackColor = Color.White,
                    AutoSize = true
                };
                buttonArray[i].Click += (s1, e1) =>
                {
                    LoadSearch(file);
                };
            }
            flowLayoutPanel2.Controls.AddRange(buttonArray);
            Application.DoEvents();
        }
        private void LoadSearch(string filePath)
        {
            string[] lines = System.IO.File.ReadAllLines(filePath);
            Button refresh = new Button
            {
                Text = "Refresh All",
                ForeColor = Color.White,
                BackColor = Color.LightSkyBlue,
                AutoSize = true
            };
            refresh.Click += (s, e) =>
            {
                var web = new HtmlAgilityPack.HtmlWeb();
                var document = new HtmlAgilityPack.HtmlDocument();
                var cnt = flowLayoutPanel1.Controls.Count;
                for (int i = 3; i < cnt; i++)
                {
                    Button b = flowLayoutPanel1.Controls[i] as Button;
                    Color saveColor = b.BackColor;
                    b.BackColor = Color.Yellow;
                    b.Refresh();
                    Application.DoEvents();
                    try
                    {
                        document = web.Load("https://www.ebay.com/sch/i.html?_from=R40&_sacat=0&_nkw=" + b.Text);
                        var page = document.DocumentNode;
                        var count = page.QuerySelector(".srp-controls__count-heading span:first-child").InnerText;
                        if (count != "0")
                        {
                            b.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            b.BackColor = saveColor;
                        }
                    }
                    catch (Exception)
                    {
                        b.BackColor = Color.Pink;
                    }
                    Application.DoEvents();
                }
            };
            flowLayoutPanel1.Controls.Add(refresh);
            Button openfound = new Button
            {
                Text = "Open Found",
                ForeColor = Color.White,
                BackColor = Color.LightGreen,
                AutoSize = true
            };
            openfound.Click += (s, e) =>
            {
                var cnt = flowLayoutPanel1.Controls.Count;
                for (int i = 3; i < cnt; i++)
                {
                    Button b = flowLayoutPanel1.Controls[i] as Button;
                    if (b.BackColor == Color.LightGreen)
                    {
                        System.Diagnostics.ProcessStartInfo sInfo = new ProcessStartInfo("https://www.ebay.com/sch/i.html?_from=R40&_sacat=0&_nkw=" + b.Text);
                        Process.Start(sInfo);
                    }
                }
            };
            flowLayoutPanel1.Controls.Add(openfound);
            Button openerrors = new Button
            {
                Text = "Open Errors",
                ForeColor = Color.White,
                BackColor = Color.Pink,
                AutoSize = true
            };
            openerrors.Click += (s, e) =>
            {
                var cnt = flowLayoutPanel1.Controls.Count;
                for (int i = 3; i < cnt; i++)
                {
                    Button b = flowLayoutPanel1.Controls[i] as Button;
                    if (b.BackColor == Color.Pink)
                    {
                        System.Diagnostics.ProcessStartInfo sInfo = new ProcessStartInfo("https://www.ebay.com/sch/i.html?_from=R40&_sacat=0&_nkw=" + b.Text);
                        Process.Start(sInfo);
                    }
                }
            };
            flowLayoutPanel1.Controls.Add(openerrors);
            Application.DoEvents();
            var buttonArray = new Button[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                String s = line.Replace("\"", "'");
                s = s.Replace("\n", " ");
                s = s.Replace("\r", " ");
                s = s.Replace("\t", " ");
                s = s.Replace("\"", "'");
                s = Regex.Replace(s, @"\s+", " ");
                s = s.Trim();
                buttonArray[i] = new Button
                {
                    Text = s,
                    AutoSize = true
                };
                buttonArray[i].Click += (s1, e1) =>
                {
                    System.Diagnostics.ProcessStartInfo sInfo = new ProcessStartInfo("https://www.ebay.com/sch/i.html?_from=R40&_sacat=0&_nkw=" + s);
                    Process.Start(sInfo);
                };
            }
            flowLayoutPanel1.Controls.AddRange(buttonArray);
            Application.DoEvents();
        }
    }
}
 
