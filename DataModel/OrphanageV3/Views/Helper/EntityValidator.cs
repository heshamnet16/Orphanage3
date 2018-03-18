using OrphanageV3.Views.Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrphanageV3.Views.Helper
{
    public class EntityValidator : IEntityValidator
    {
        private Control.ControlCollection _controlCollection;
        private object _DataEntity;

        public EntityValidator()
        {
            _DataEntity = null;
            _controlCollection = null;
        }
        public Control.ControlCollection controlCollection { get => _controlCollection; set { _controlCollection = value; } }

        public object DataEntity { get => _DataEntity; set { _DataEntity = value; } }

        public IEnumerable<KeyValuePair<Control, string>> ErrorsControls()
        {
            if (_DataEntity == null || controlCollection == null) yield return new KeyValuePair<Control, string>();
            ValidationContext validationContext = new ValidationContext(_DataEntity, null, null);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(_DataEntity, validationContext, results, true))
            {
                foreach (var result in results)
                {
                    foreach (string dataMember in result.MemberNames)
                    {
                        foreach (Control cont in controlCollection)
                        {
                            IList<Control> listControls = new List<Control>();
                            findControl(cont, dataMember, ref listControls);
                            foreach(var retControl in listControls)
                            {
                                yield return new KeyValuePair<Control, string>(retControl,result.ErrorMessage);
                            }
                        }
                    }
                }
            }
        }

        private void findControl(Control control, string comparableString, ref IList<Control> controlList)
        {
            if (!control.Name.ToLower().Contains("lbl") && control.Name.ToLower().Contains(comparableString.ToLower()))
                controlList.Add(control);
            else
            {
                if (control.HasChildren)
                    foreach (Control cont in control.Controls)
                        findControl(cont, comparableString, ref controlList);
            }
        }
        public bool IsValid()
        {
            if (_DataEntity == null || controlCollection == null) return true;
            ValidationContext validationContext = new ValidationContext(_DataEntity, null, null);
            var results = new List<ValidationResult>();
            bool ret = true;
            if (!Validator.TryValidateObject(_DataEntity, validationContext, results, true))
            {
                ret = false;
            }
            return ret;
        }

        public void SetErrorProvider(ErrorProvider errorProvider)
        {
            errorProvider.Clear();
            if (_DataEntity == null || controlCollection == null) return ;
            var controls = ErrorsControls();
            foreach(var control in controls)
            {
                errorProvider.SetError(control.Key,control.Value);
                errorProvider.SetIconAlignment(control.Key, ErrorIconAlignment.MiddleLeft);
            }
        }
    }
}
