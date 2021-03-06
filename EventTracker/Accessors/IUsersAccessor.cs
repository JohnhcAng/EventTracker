﻿using EventTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTracker.Accessors
{
    public interface IUsersAccessor
    {
        User FindUser(string enteredUserName);
        void InsertUser(User newUser);
        string ComputeSha256Hash(string password);
    }
}
