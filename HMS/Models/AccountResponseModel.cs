using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class AccountResponseModel
    {
        #region login  || Forgot Pass
        public bool Success { get; set; }

        public string Message { get; set; } 
        #endregion
    }
}