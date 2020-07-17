using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.GameSourceUser.UserRoleViewModel
{
    public class UserRoleEditViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
