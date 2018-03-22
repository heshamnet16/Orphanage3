using OrphanageV3.Extensions;
using OrphanageV3.Services.Interfaces;
using OrphanageV3.Views.Helper.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace OrphanageV3.Controlls
{
    public partial class NameForm : UserControl
    {
        private IAutoCompleteService _AutoCompleteServic /*= Program.Factory.Resolve<IAutoCompleteService>()*/;
        private IDataFormatterService _DataFormatterService  /*= Program.Factory.Resolve<IDataFormatterService>()*/;
        private IEntityValidator _entityValidator;

        public object NameDataSource
        {
            get => nameBindingSource.DataSource;
            set
            {
                nameBindingSource.DataSource = value;
            }
        }

        private bool _ShowMovement;
        private bool isSHown = false;
        private int hi;
        private int wi;
        private int lef;
        private int tp;
        private int _MoveFactor = 10;

        public enum _MoveType
        {
            LeftToRight,

            RightToLeft,

            UpToDown,

            DownToUp,
        }

        public string FullName
        {
            get
            {
                if (NameDataSource != null && NameDataSource is OrphanageDataModel.RegularData.Name)
                    return ((OrphanageDataModel.RegularData.Name)NameDataSource).FullName();
                else
                    return null;
            }
        }

        public bool FocusWhenShow { get; set; } = true;

        public int Id
        {
            get
            {
                if (nameBindingSource != null)
                {
                    try
                    {
                        return ((OrphanageDataModel.RegularData.Name)nameBindingSource.DataSource).Id;
                    }
                    catch { return -1; }
                }
                else
                    return -1;
            }
            set
            {
                if (nameBindingSource != null)
                {
                    try
                    {
                        ((OrphanageDataModel.RegularData.Name)nameBindingSource.DataSource).Id = value;
                    }
                    catch { }
                }
            }
        }

        public _MoveType MoveType
        {
            get
            {
                return this._Mtype;
            }
            set
            {
                this._Mtype = value;
            }
        }

        private _MoveType _Mtype = _MoveType.UpToDown;

        public int MoveFactor
        {
            get
            {
                return this._MoveFactor;
            }
            set
            {
                this._MoveFactor = value;
            }
        }

        public bool ShowMovement
        {
            get
            {
                return this._ShowMovement;
            }
            set
            {
                this._ShowMovement = value;
            }
        }

        private bool _HideOnEnter = false;

        public bool HideOnEnter
        {
            get
            {
                return _HideOnEnter;
            }
            set
            {
                _HideOnEnter = value;
            }
        }

        public NameForm()
        {
            InitializeComponent();
            try
            {
                _entityValidator = Program.Factory.Resolve<IEntityValidator>();
            }
            catch
            {
                _entityValidator = null;
            }
            try
            {
                _DataFormatterService = Program.Factory.Resolve<IDataFormatterService>();
            }
            catch
            {
                _DataFormatterService = null;
            }
            try
            {
                _AutoCompleteServic = Program.Factory.Resolve<IAutoCompleteService>();
            }
            catch
            {
                _AutoCompleteServic = null;
            }
        }

        private void txtFatherNameF_Enter(object sender, System.EventArgs e)
        {
            LangChanger.CurLang.SaveCurrentLanguage();
            LangChanger.CurLang.ChangeToArabic();
        }

        private void txtFAtherNameEf_Enter(object sender, System.EventArgs e)
        {
            LangChanger.CurLang.SaveCurrentLanguage();
            LangChanger.CurLang.ChangeToEnglish();
        }

        private void ValidateAndShowError()
        {
            NameerrorProvider1.Clear();
            if (_entityValidator != null)
            {
                _entityValidator.controlCollection = Controls;
                _entityValidator.DataEntity = this.NameDataSource;
                _entityValidator.SetErrorProvider(NameerrorProvider1);
            }
        }

        private void txtFAtherNameEf_Leave(object sender, System.EventArgs e)
        {
            LangChanger.CurLang.ReturnToSavedLanguage();
            if ((sender is Telerik.WinControls.UI.RadTextBoxControl))
            {
                Telerik.WinControls.UI.RadTextBoxControl txt = ((Telerik.WinControls.UI.RadTextBoxControl)(sender));
                string ret;
                if ((txt.Text.Length > 0))
                {
                    ret = char.ToUpper(txt.Text[0]).ToString();
                    ret = (ret + txt.Text.Substring(1, (txt.Text.Length - 1)));
                    txt.Text = ret;
                }
            }
            ValidateAndShowError();
        }

        private void txtFatherNameF_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (_HideOnEnter)
            {
                if ((e.KeyCode == Keys.Enter))
                {
                    this.Visible = false;
                }
            }
        }

        public void HideMe()
        {
            if ((isSHown == true) && _entityValidator.IsValid())
            {
                if ((this._Mtype == _MoveType.UpToDown))
                {
                    this.Width = wi;
                    for (int i = hi; (i <= 0); i = (i
                                + (_MoveFactor * -1)))
                    {
                        this.Height = i;
                        Application.DoEvents();
                        this.Refresh();
                    }

                    this.Height = 0;
                }
                else if ((this._Mtype == _MoveType.DownToUp))
                {
                    this.Width = wi;
                    for (int i = hi; (i <= 0); i = (i
                                + (_MoveFactor * -1)))
                    {
                        this.Height = i;
                        Application.DoEvents();
                        this.Refresh();
                        this.Top = (this.Top
                                    + (_MoveFactor / 2));
                    }

                    this.Height = 0;
                    this.Top = (this.tp
                                + (this.hi / 2));
                }
                else if ((this._Mtype == _MoveType.LeftToRight))
                {
                    this.Height = hi;
                    for (int i = wi; (i <= 0); i = (i
                                + (_MoveFactor * -1)))
                    {
                        this.Width = i;
                        Application.DoEvents();
                        this.Refresh();
                    }

                    this.Width = 0;
                }
                else
                {
                    // '''''''''''''''''''''
                    this.Height = hi;
                    for (int i = wi; (i <= 0); i = (i
                                + (_MoveFactor * -1)))
                    {
                        this.Width = i;
                        Application.DoEvents();
                        this.Refresh();
                        this.Left = (this.Left
                                    + (_MoveFactor / 2));
                    }

                    this.Width = 0;
                    this.Left = (this.lef
                                + (this.wi / 2));
                }

                _ShowMovement = false;
                this.Visible = false;
                _ShowMovement = true;
                isSHown = false;
            }
            else
                ValidateAndShowError();
        }

        public void ShowMe()
        {
            _ShowMovement = false;
            this.Visible = true;
            _ShowMovement = true;
            if (this.FocusWhenShow)
            {
                txtFirst.Focus();
            }

            if ((isSHown == false))
            {
                if ((this._Mtype == _MoveType.UpToDown))
                {
                    this.Width = wi;
                    this.Left = this.lef;
                    this.Top = this.tp;
                    for (int i = 0; (i <= hi); i = (i + _MoveFactor))
                    {
                        this.Height = i;
                        Application.DoEvents();
                        this.Refresh();
                    }

                    this.Height = hi;
                }
                else if ((this._Mtype == _MoveType.DownToUp))
                {
                    this.Width = wi;
                    this.Left = this.lef;
                    this.Top = (this.tp
                                + (this.hi / 2));
                    for (int i = 0; (i <= hi); i = (i + _MoveFactor))
                    {
                        this.Height = i;
                        Application.DoEvents();
                        this.Refresh();
                        this.Top = (this.Top
                                    - (_MoveFactor / 2));
                    }

                    this.Height = hi;
                    this.Top = this.tp;
                }
                else if ((this._Mtype == _MoveType.LeftToRight))
                {
                    this.Height = hi;
                    this.Left = this.lef;
                    this.Top = this.tp;
                    for (int i = 0; (i <= wi); i = (i + _MoveFactor))
                    {
                        this.Width = i;
                        Application.DoEvents();
                        this.Refresh();
                        // Me.Left += (i / 2)
                    }

                    this.Width = wi;
                }
                else
                {
                    this.Height = hi;
                    this.Left = (this.lef
                                + (this.wi / 2));
                    for (int i = 0; (i <= wi); i = (i + _MoveFactor))
                    {
                        this.Width = i;
                        Application.DoEvents();
                        this.Refresh();
                        this.Left = (this.Left
                                    - (_MoveFactor / 2));
                    }

                    this.Width = wi;
                    this.Left = lef;
                }

                isSHown = true;
            }
        }

        private void NameForm_Load(object sender, EventArgs e)
        {
            hi = Height;
            wi = Width;
            tp = Top;
            lef = Left;
            grpName.Text = grpName.HeaderText = Properties.Resources.Name;
            lblFatherName.Text = Properties.Resources.FatherName;
            lblFirstName.Text = Properties.Resources.FirstName;
            lblLastName.Text = Properties.Resources.LastName;
            lblFatherNameE.Text = Properties.Resources.FatherNameE;
            lblFirstNameE.Text = Properties.Resources.FirstNameE;
            lblLastNameE.Text = Properties.Resources.LastNameE;

            if (_AutoCompleteServic != null)
            {
                _AutoCompleteServic.DataLoaded += _AutoCompleteServic_DataLoaded;
                _AutoCompleteServic.LoadData();
            }
        }

        private void _AutoCompleteServic_DataLoaded(object sender, EventArgs e)
        {
            var stringArray = _AutoCompleteServic.EnglishNameStrings.ToArray();

            if (txtEnglishFirst.AutoCompleteCustomSource.Count != stringArray.Length)
            {
                txtEnglishFirst.AutoCompleteCustomSource.AddRange(stringArray);
                txtEnglishFather.AutoCompleteCustomSource.AddRange(stringArray);
                txtEnglishLast.AutoCompleteCustomSource.AddRange(stringArray);
            }
        }

        private void NameForm_VisibleChanged(object sender, System.EventArgs e)
        {
            if (_ShowMovement)
            {
                if (this.Visible)
                {
                    ShowMe();
                }
                else
                {
                    _ShowMovement = false;
                    this.Visible = true;
                    HideMe();
                    this.Visible = false;
                    _ShowMovement = true;
                }
            }
        }

        private void nameBindingSource_DataSourceChanged(object sender, EventArgs e)
        {
        }
    }
}