using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;


namespace OnlineStore.Objects
{
    public class currentUser : User
    {
        public static CurrentUser currentUser = new User("default", "default", "default", "default", false, 0);
    }
}
