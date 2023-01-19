using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace V2.Models.Account
{
    public class User
    {
        [Required(ErrorMessage ="Please enter company name")]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter email address")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Your email address is not in a valid format.")]
        [DataType(DataType.EmailAddress)]
        [Remote("IsAlreadySigned", "Register", HttpMethod = "POST", ErrorMessage = "EmailId already exists in database.")]
        public string EmailId { get; set; }
        [Required(ErrorMessage ="Please enter mobile number")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        //[StringLength(10,ErrorMessage ="Please enter valid mobile number")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$", ErrorMessage = "Password must be 8 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter confirm password")]
        [Compare("Password", ErrorMessage = "Password doesn't match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="Please select your city")]
        public int CityId { get; set; }

        [StringLength(6)]
        public string OTP { get; set; }

        [Required(ErrorMessage ="Gst number is required")]  
        [RegularExpression(@"^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$", ErrorMessage = "Invalid GST Number.")]
        [StringLength(15)]
        public String GSTIN { get; set; }
    }
}
