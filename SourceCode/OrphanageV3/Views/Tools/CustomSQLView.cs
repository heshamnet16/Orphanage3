using OrphanageV3.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinForms.Documents;
using Telerik.WinForms.Documents.FormatProviders.Txt;
using Unity;

namespace OrphanageV3.Views.Tools
{
    public partial class CustomSQLView : Telerik.WinControls.UI.RadForm
    {
        private IApiClient _apiClient = Program.Factory.Resolve<IApiClient>();

        public CustomSQLView()
        {
            InitializeComponent();
            TranslateControls();
        }

        private void TranslateControls()
        {
            radButton1.Text = Properties.Resources.Send;
            this.Text = Properties.Resources.RunCustomSQL;
            CenterControls();
            StopWaiting();
        }

        private void StartWaiting()
        {
            radWaitingBar1.StartWaiting();
            radWaitingBar1.Visible = true;
            tableLayoutPanel1.Enabled = false;
        }

        private void StopWaiting()
        {
            radWaitingBar1.StartWaiting();
            radWaitingBar1.Visible = false;
            tableLayoutPanel1.Enabled = true;
        }

        private void CenterControls()
        {
            radButton1.Left = radPanel1.Width / 2 - (radButton1.Width / 2);
            radButton1.Top = radPanel1.Height / 2 - (radButton1.Height / 2);
            radWaitingBar1.Left = txtSourceSQL.Width / 2 - (radWaitingBar1.Width / 2);
            radWaitingBar1.Top = txtSourceSQL.Height / 2 - (radWaitingBar1.Height / 2);
        }

        private void radTextBox1_TextChanged(object sender, EventArgs e)
        {
            TetraSQLFormatter.Formatters.TSqlStandardFormatterOptions standardFormatterOptions = new TetraSQLFormatter.Formatters.TSqlStandardFormatterOptions();
            standardFormatterOptions.HTMLColoring = true;
            standardFormatterOptions.UppercaseKeywords = true;
            standardFormatterOptions.BreakJoinOnSections = false;
            standardFormatterOptions.ExpandBetweenConditions = true;
            standardFormatterOptions.ExpandBooleanExpressions = true;
            standardFormatterOptions.ExpandCommaLists = true;
            standardFormatterOptions.ExpandInLists = true;
            standardFormatterOptions.NewClauseLineBreaks = 10;

            TetraSQLFormatter.Formatters.TSqlStandardFormatter sqlStandardFormatter = new TetraSQLFormatter.Formatters.TSqlStandardFormatter(standardFormatterOptions);
            TetraSQLFormatter.Parsers.TSqlStandardParser sqlStandardParser = new TetraSQLFormatter.Parsers.TSqlStandardParser();
            TetraSQLFormatter.Tokenizers.TSqlStandardTokenizer sqlStandardTokenizer = new TetraSQLFormatter.Tokenizers.TSqlStandardTokenizer();

            var tokens = sqlStandardTokenizer.TokenizeSQL(GetText());
            var ret = sqlStandardParser.ParseSQL(tokens);
            var formattedSQL = sqlStandardFormatter.FormatSQLTree(ret);
            txtSourceSQL.Text = CssStyle(makeNewLinesOnGoKeyword(formattedSQL));
        }

        private string makeNewLinesOnGoKeyword(string str)
        {
            int i = 0;
            while (i < str.Length - 2)
            {
                int goPos = str.IndexOf("GO", i, StringComparison.OrdinalIgnoreCase);
                if (goPos <= 0)
                    break;
                str = str.Insert(goPos + 2, "</br></br>");
                str = str.Insert(goPos, "</br></br>");
                i = goPos + 15;
            }
            return str;
        }

        private string CssStyle(string body)
        {
            txtSourceSQL.ChangeFontSize(11);
            string head = "<HTML><HEAD><STYLE> .SQLOperator {color: red;} .SQLKeyword {color: blue;} .SQLString{color: Violet;} .span{font-size: 11px;} </STYLE></HEAD><BODY>";
            string tail = "</BODY></HEAD>";
            return head + body + tail;
        }

        private string GetText()
        {
            TxtFormatProvider provider = new TxtFormatProvider();
            Telerik.WinForms.Documents.Model.RadDocument document = this.txtSourceSQL.Document;
            return provider.Export(document);
        }

        private void txtSourceSQL_Leave(object sender, EventArgs e)
        {
        }

        private void radPanel1_SizeChanged(object sender, EventArgs e)
        {
            CenterControls();
        }

        private void CustomSQLView_Load(object sender, EventArgs e)
        {
        }

        private bool checkScript()
        {
            if (txtSourceSQL.Text.ToLower().Contains("drop"))
            {
                DocumentPosition position = new DocumentPosition(this.txtSourceSQL.Document);
                do
                {
                    string word = position.GetCurrentSpanBox().Text;
                    if (word.ToLower() == "drop")
                    {
                        DocumentPosition wordEndPosition = new DocumentPosition(position);
                        wordEndPosition.MoveToCurrentWordEnd();
                        txtSourceSQL.Document.Selection.AddSelectionStart(position);
                        txtSourceSQL.Document.Selection.AddSelectionEnd(wordEndPosition);
                    }
                }
                while (position.MoveToNextWordStart());
                return true;
            }
            else
                return false;
        }

        private async void radButton1_Click(object sender, EventArgs e)
        {
            string commands = GetText();
            if (checkScript())
            {
                return;
            }
            try
            {
                StartWaiting();
                int ret = await _apiClient.DataBanks_ExecuteScriptAsync(commands);
                MessageBox.Show(Properties.Resources.SQLReturnMessage.Replace("[UPDATE]", ret.ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                StopWaiting();
            }
        }
    }
}