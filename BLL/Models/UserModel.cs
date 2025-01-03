﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BLL.Models
{
    public class UserModel
    {
            public User Record { get; set; }
        public string RoleName => Record.Role?.Name;

            // Add display and validation attributes for the view
            [Required(ErrorMessage = "Username is required")]
            [Display(Name = "Username")]
            public string UserName
            {
                get => Record?.UserName;
                set { if (Record != null) Record.UserName = value; }
            }

            [Display(Name = "Role")]
            public int RoleId
            {
                get => Record?.RoleId ?? 0;
                set { if (Record != null) Record.RoleId = value; }
            }

            [Display(Name = "Active")]
            public bool IsActive
            {
                get => Record?.IsActive ?? false;
                set { if (Record != null) Record.IsActive = value; }
            }

            // Only used for create/update operations
            [Display(Name = "Password")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Confirm Password")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
    }
