using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxEditors;

namespace Vozila.Controllers
{
    public class EditorsDemosHelper
    {
        static ValidationSettings nameValidationSettings;
        public static ValidationSettings NameValidationSettings
        {
            get
            {
                if (nameValidationSettings == null)
                {
                    nameValidationSettings = ValidationSettings.CreateValidationSettings();
                    nameValidationSettings.Display = Display.Dynamic;
                    nameValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithText;
                    nameValidationSettings.ErrorText = "Polje je prazno";
                }
                return nameValidationSettings;
            }
        }
        static ValidationSettings ageValidationSettings;
        public static ValidationSettings AgeValidationSettings
        {
            get
            {
                if (ageValidationSettings == null)
                {
                    ageValidationSettings = ValidationSettings.CreateValidationSettings();
                    ageValidationSettings.Display = Display.Dynamic;
                    ageValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithText;
                    ageValidationSettings.ErrorText = "Broj između 1 i 6";
                }
                return ageValidationSettings;
            }
        }

        static ValidationSettings _brPutnika;
        public static ValidationSettings BrojPutnika
        {
            get
            {
                if (_brPutnika == null)
                {
                    _brPutnika = ValidationSettings.CreateValidationSettings();
                    _brPutnika.Display = Display.Dynamic;
                    _brPutnika.ErrorDisplayMode = ErrorDisplayMode.ImageWithText;
                    _brPutnika.ErrorText = "Broj između 1 i 6";
                }
                return _brPutnika;
            }
        }

        static ValidationSettings emailValidationSettings;
        public static ValidationSettings EmailValidationSettings
        {
            get
            {
                if (emailValidationSettings == null)
                {
                    emailValidationSettings = ValidationSettings.CreateValidationSettings();
                    emailValidationSettings.Display = Display.Dynamic;
                    emailValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithText;
                    emailValidationSettings.RequiredField.IsRequired = true;
                    emailValidationSettings.RequiredField.ErrorText = "Email is required";
                    emailValidationSettings.RegularExpression.ValidationExpression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                    emailValidationSettings.RegularExpression.ErrorText = "Email is invalid";
                }
                return emailValidationSettings;
            }
        }
        static ValidationSettings arrivalDateValidationSettings;
        public static ValidationSettings ArrivalDateValidationSettings
        {
            get
            {
                if (arrivalDateValidationSettings == null)
                {
                    arrivalDateValidationSettings = ValidationSettings.CreateValidationSettings();
                    arrivalDateValidationSettings.Display = Display.Dynamic;
                    arrivalDateValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    arrivalDateValidationSettings.ErrorText = "Unesite datum";
                    arrivalDateValidationSettings.RequiredField.IsRequired = true;
                    arrivalDateValidationSettings.RequiredField.ErrorText = "";
                }
                return arrivalDateValidationSettings;
            }
        }

        public static void OnNameValidation(object sender, ValidationEventArgs e)
        {
            if (e.Value == null)
            {
                e.IsValid = false;
                return;
            }
            var name = e.Value.ToString();
            if (name == string.Empty)
                e.IsValid = false;
            if (name.Length > 50)
            {
                e.IsValid = false;
                e.ErrorText = "Must be under 50 characters";
            }
        }
        public static void OnAgeValidation(object sender, ValidationEventArgs e)
        {
            if (e.Value == null)
                return;
            var age = int.Parse(e.Value.ToString());
            if (age < 18 || age > 100)
                e.IsValid = false;
        }
    }
}