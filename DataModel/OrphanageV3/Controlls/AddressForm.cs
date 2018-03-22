using OrphanageV3.Extensions;
using OrphanageV3.Services.Interfaces;
using OrphanageV3.Views.Helper.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace OrphanageV3.Controlls
{
    public partial class AddressForm : UserControl
    {
        private bool _ShowMovement;
        private int _MoveFactor = 10;
        private IAutoCompleteService _AutoCompleteServic /*= Program.Factory.Resolve<IAutoCompleteService>()*/;
        private IDataFormatterService _DataFormatterService  /*= Program.Factory.Resolve<IDataFormatterService>()*/;
        private IEntityValidator _entityValidator;

        public object AddressDataSource
        {
            get => addressBindingSource.DataSource;
            set
            {
                if(value != null)
                    addressBindingSource.DataSource = value;
            }
        }

        public enum _MoveType
        {
            LeftToRight,

            RightToLeft,

            UpToDown,

            DownToUp,
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

        public int Id
        {
            get
            {
                if (addressBindingSource != null)
                {
                    try
                    {
                        return ((OrphanageDataModel.RegularData.Address)addressBindingSource.DataSource).Id;
                    }
                    catch { return -1; }
                }
                else
                    return -1;
            }
            set
            {
                if (addressBindingSource != null)
                {
                    try
                    {
                        ((OrphanageDataModel.RegularData.Address)addressBindingSource.DataSource).Id = value;
                    }
                    catch { }
                }
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

        public string FullAddress
        {
            get
            {
                if (_DataFormatterService != null && addressBindingSource.DataSource != null && addressBindingSource.DataSource is OrphanageDataModel.RegularData.Address)
                {
                    var addObject = (OrphanageDataModel.RegularData.Address)addressBindingSource.DataSource;
                    return _DataFormatterService.GetAddressString(addObject);
                }
                else
                    return null;
            }
        }

        private bool isSHown = false;

        private int hi;

        private int wi;

        private int lef;

        private int tp;

        public AddressForm()
        {
            InitializeComponent();
            TranslateControls();
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

        private void _AutoCompleteServic_DataLoaded(object sender, EventArgs e)
        {
            //fired when string is loaded
            txtCity.AutoCompleteCustomSource.AddRange(_AutoCompleteServic.Cities.ToArray());
            txtCountry.AutoCompleteCustomSource.AddRange(_AutoCompleteServic.Countries.ToArray());
            txtTown.AutoCompleteCustomSource.AddRange(_AutoCompleteServic.Towns.ToArray());
            txtStreet.AutoCompleteCustomSource.AddRange(_AutoCompleteServic.Streets.ToArray());
        }

        private void TranslateControls()
        {
            lblCity.Text = Properties.Resources.City.getDobblePunkt();
            lblEmail.Text = Properties.Resources.Email.getDobblePunkt();
            lblFacebook.Text = Properties.Resources.Facebook.getDobblePunkt();
            lblFaxNumber.Text = Properties.Resources.FaxNumber.getDobblePunkt();
            lblHomePhone.Text = Properties.Resources.HomePhone.getDobblePunkt();
            lblMobileNumber.Text = Properties.Resources.MobileNumber.getDobblePunkt();
            lblNotes.Text = Properties.Resources.Notes.getDobblePunkt();
            lblSkype.Text = Properties.Resources.Twitter.getDobblePunkt();
            lblState.Text = Properties.Resources.State.getDobblePunkt();
            lblStreet.Text = Properties.Resources.Street.getDobblePunkt();
            lblTown.Text = Properties.Resources.Town.getDobblePunkt();
            lblWorkPhone.Text = Properties.Resources.WorkPhone.getDobblePunkt();
            grpAddress.Text = Properties.Resources.Address;
            grpAddress.HeaderText = Properties.Resources.Address;
            grpInternet.Text = Properties.Resources.Internet;
            grpInternet.HeaderText = Properties.Resources.Internet;
            grpPhoneNumbers.Text = Properties.Resources.PhoneNumbers;
            grpPhoneNumbers.HeaderText = Properties.Resources.PhoneNumbers;
        }

        private void txtCountry_Enter(object sender, EventArgs e)
        {
            LangChanger.CurLang.SaveCurrentLanguage();
            LangChanger.CurLang.ChangeToArabic();
        }

        private void txtCountry_Leave(object sender, EventArgs e)
        {
            LangChanger.CurLang.ReturnToSavedLanguage();
            if ((sender == txtFacebook))
            {
                if ((txtFacebook.Text == "Https://www.Facebook.com/"))
                {
                    txtFacebook.Text = null;
                }
            }
            if ((sender == txtTwitter))
            {
                if ((txtTwitter.Text == "Https://www.twitter.com/"))
                {
                    txtTwitter.Text = null;
                }
            }
            ValidateAndShowError();
        }

        private void ValidateAndShowError()
        {
            addressErrorProvider1.Clear();
            if (_entityValidator != null)
            {
                _entityValidator.controlCollection = Controls;
                _entityValidator.DataEntity = this.AddressDataSource;
                _entityValidator.SetErrorProvider(addressErrorProvider1);
            }
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            LangChanger.CurLang.SaveCurrentLanguage();
            LangChanger.CurLang.ChangeToEnglish();
            if ((sender == txtFacebook))
            {
                if (((txtFacebook.Text == null)
                            || (txtFacebook.Text == "")))
                {
                    txtFacebook.Text = "Https://www.Facebook.com/";
                    txtFacebook.SelectionStart = txtFacebook.Text.Length;
                }
            }
            if ((sender == txtTwitter))
            {
                if (((txtTwitter.Text == null)
                            || (txtTwitter.Text == "")))
                {
                    txtTwitter.Text = "Https://www.twitter.com/";
                    txtTwitter.SelectionStart = txtTwitter.Text.Length;
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

        private void AddressFrom_Load(object sender, EventArgs e)
        {
            hi = Height;
            this.wi = this.Width;
            this.tp = this.Top;
            this.lef = this.Left;

            if (_AutoCompleteServic != null)
            {
                _AutoCompleteServic.DataLoaded += _AutoCompleteServic_DataLoaded;
                _AutoCompleteServic.LoadData();
            }
        }

        private void AddressFrom_VisibleChanged(object sender, EventArgs e)
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

        private void txtCountry_KeyUp(object sender, KeyEventArgs e)
        {
            if (_HideOnEnter)
            {
                if ((e.KeyCode == Keys.Enter))
                {
                    this.Visible = false;
                }
            }
        }

        private void addressBindingSource_DataSourceChanged(object sender, EventArgs e)
        {
        }
    }
}