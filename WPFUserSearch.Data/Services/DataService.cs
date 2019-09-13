using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;

using WPFUserSearch.Data.Helpers;
using WPFUserSearch.Data.Models.DB;

namespace WPFUserSearch.Data.Services
{
    public class DataService : IDataService
    {
        public async Task<List<User>> GetUsers()
        {
            await Task.Delay(300);

            return await Task.Run(() =>
            {
                using (var context = new WPFUserEntities())
                {
                    return context.Users
                        .OrderBy(x => x.LastName)
                        .ToList();
                }
            });
        }

        public async Task<List<User>> GetUsers(string fullNameLike)
        {
            await Task.Delay(300);

            return await Task.Run(() =>
            {

                using (var context = new WPFUserEntities())
                {
                    List<User> list = context.Users.ToList();

                    if (string.IsNullOrEmpty(fullNameLike))
                    {
                        return list;
                    }
                    else
                    {
                        return list
                            .Where(x => FormatUserFullName(x).ToLower().Contains(fullNameLike.ToLower()))
                            .OrderBy(x => x.LastName)
                            .ToList();
                    }
                }
            });
        }

        public async Task<string> CreateUser(User user)
        {
            string returnValue = string.Empty;

            // Update user info - replace blanks with null
            FormatUserValues(ref user);

            try
            {
                using (var context = new WPFUserEntities())
                {
                    context.Users.Add(user);
                    var result = await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                returnValue = "CreateUser failed:" + Environment.NewLine + Environment.NewLine;

                if (ex.GetType().Equals(typeof(DbEntityValidationException)))
                {
                    DbEntityValidationException exTyped = (DbEntityValidationException)ex;

                    if (exTyped.EntityValidationErrors != null &&
                        exTyped.EntityValidationErrors.Count() > 0 &&
                        exTyped.EntityValidationErrors.ElementAt(0).ValidationErrors != null &&
                        exTyped.EntityValidationErrors.ElementAt(0).ValidationErrors.Count() > 0)
                    {
                        returnValue += string.Join(Environment.NewLine, exTyped.EntityValidationErrors.ElementAt(0).ValidationErrors.Select(x => x.ErrorMessage));
                    }
                    else
                    {
                        returnValue += exTyped.Message;
                    }
                }
                else
                {
                    returnValue += ex.Message +
                        ((ex.InnerException == null || ex.InnerException.InnerException == null || ex.InnerException.InnerException.Message == null)
                            ? ""
                            : Environment.NewLine + ex.InnerException.InnerException.Message);
                }
            }

            return returnValue;
        }

        public async Task<string> UpdateUser(User user)
        {
            string returnValue = string.Empty;
            bool updateRequired = false;

            // Update user info - replace blanks with null
            FormatUserValues(ref user);

            try
            {
                using (var context = new WPFUserEntities())
                {
                    User userSave = context.Users.Where(x => x.PK_User == user.PK_User).FirstOrDefault();

                    updateRequired =
                        userSave.FirstName != user.FirstName ||
                        userSave.MiddleName != user.MiddleName ||
                        userSave.LastName != user.LastName ||
                        userSave.Address != user.Address ||
                        userSave.City != user.City ||
                        userSave.State != user.State ||
                        userSave.Zip != user.Zip ||
                        userSave.Age != user.Age ||
                        userSave.Interests != user.Interests ||
                        !userSave.Photo.SequenceEqual(user.Photo);

                    if (updateRequired)
                    {
                        userSave.FirstName = user.FirstName;
                        userSave.MiddleName = user.MiddleName;
                        userSave.LastName = user.LastName;
                        userSave.Address = user.Address;
                        userSave.City = user.City;
                        userSave.State = user.State;
                        userSave.Zip = user.Zip;
                        userSave.Age = user.Age;
                        userSave.Interests = user.Interests;
                        userSave.Photo = user.Photo;

                        var result = await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                returnValue = "UpdateUser failed:" + Environment.NewLine + Environment.NewLine;

                if (ex.GetType().Equals(typeof(DbEntityValidationException)))
                {
                    DbEntityValidationException exTyped = (DbEntityValidationException)ex;

                    if (exTyped.EntityValidationErrors != null &&
                        exTyped.EntityValidationErrors.Count() > 0 &&
                        exTyped.EntityValidationErrors.ElementAt(0).ValidationErrors != null &&
                        exTyped.EntityValidationErrors.ElementAt(0).ValidationErrors.Count() > 0)
                    {
                        returnValue += string.Join(Environment.NewLine, exTyped.EntityValidationErrors.ElementAt(0).ValidationErrors.Select(x => x.ErrorMessage));
                    }
                    else
                    {
                        returnValue += exTyped.Message;
                    }
                }
                else
                {
                    returnValue += ex.Message +
                        ((ex.InnerException == null || ex.InnerException.InnerException == null || ex.InnerException.InnerException.Message == null)
                            ? ""
                            : Environment.NewLine + ex.InnerException.InnerException.Message);
                }
            }

            if (!updateRequired)
            {
                returnValue = string.Empty;
            }
            else if (string.IsNullOrEmpty(returnValue))
            {
                returnValue = "UpdateSuccess";
            }

            return returnValue;
        }

        public async Task<List<string>> GetUsersFullNames()
        {
            await Task.Delay(300);

            return await Task.Run(() =>
            {
                List<string> list = new List<string>();

                using (var context = new WPFUserEntities())
                {
                    foreach (var v in context.Users.ToList())
                    {
                        list.Add(Helpers.Various.GetUserFullName(v));
                    }
                }

                return list;
            });
        }

        public string FormatUserFullName(User user)
        {
            // Format full name with proper number of spaces between FirstName, LastName, and possible MiddleName
            string middleName = string.IsNullOrEmpty(user.MiddleName) ? " " : " " + user.MiddleName + " ";
            return user.FirstName + middleName + user.LastName;
        }

        private void FormatUserValues(ref User user)
        {
            if (string.IsNullOrEmpty(user.FirstName)) user.FirstName = null;
            if (string.IsNullOrEmpty(user.LastName)) user.LastName = null;
            if (string.IsNullOrEmpty(user.MiddleName)) user.MiddleName = null;
            if (string.IsNullOrEmpty(user.Address)) user.Address = null;
            if (string.IsNullOrEmpty(user.City)) user.City = null;
            if (string.IsNullOrEmpty(user.State)) user.State = null;
            if (string.IsNullOrEmpty(user.Zip)) user.Zip = null;
            if (string.IsNullOrEmpty(user.Interests)) user.Interests = null;
        }
    }
}
