﻿using CMPH_Financial.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMPH_Financial.Helpers
{
        [Authorize]
        public class UserHelper
        {
            private UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            private static ApplicationDbContext db = new ApplicationDbContext();

            public static string GetProfileImagePath(string userId)
            {
                var defaultPath = "/Uploads/default.jpg";
                if (string.IsNullOrEmpty(userId))
                    return defaultPath;

                var profileImagePath = db.Users.Find(userId).ProfileImagePath;
                if (string.IsNullOrEmpty(profileImagePath))
                    return profileImagePath;

                return profileImagePath;
            }

            public static string GetName(string userId)
            {
                var defaultName = "Guest";
                var user = db.Users.Find(userId).DisplayName;
                if (user == null)
                {
                    return defaultName;
                }
                if (string.IsNullOrEmpty(user))
                {
                    return user;
                }
                return user;
            }

            public static string GetUserHousehold(string userId)
            {
                var defaultName = "None";
                var household = db.Households.Find(userId).Name;
                if (household == null)
                {
                    return defaultName;
                }
                if (string.IsNullOrEmpty(household))
                {
                    return defaultName;
                }
                return household;
            }

            public static bool IsWebFriendlyImage(HttpPostedFileBase file)
            {
                    if (file == null)
                        return false;

                    if (file.ContentLength > 2 * 1024 * 1024 || file.ContentLength < 1024)
                        return false;

                    try
                    {
                        using (var img = Image.FromStream(file.InputStream))
                        {
                            return ImageFormat.Jpeg.Equals(img.RawFormat) ||
                                   ImageFormat.Png.Equals(img.RawFormat) ||
                                   ImageFormat.Gif.Equals(img.RawFormat);
                        }
                    }
                    catch
                    {
                        return false;
                    }

            }
        }
}
