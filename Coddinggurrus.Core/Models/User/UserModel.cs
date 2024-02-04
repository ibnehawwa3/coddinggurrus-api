﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coddinggurrus.Core.Models.User
{
    public class UserModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public DateTime DateRegisteration { get; set; }
        public int TotalRecords { get; set; }
    }
}
