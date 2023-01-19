using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace V2.Models.SPResponse
{
    public class VendorGetForApproval
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
        public string ProfileImage { get; set; }
        public string GSTIN { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public DateTime UploadDate { get; set; }
        public int StatusId { get; set; }
    }
}