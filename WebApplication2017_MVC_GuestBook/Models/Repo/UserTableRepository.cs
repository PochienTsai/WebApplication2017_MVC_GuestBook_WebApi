using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//********************************************************
using WebApplication2017_MVC_GuestBook.Models;  // 自己動手寫上命名空間 -- 「專案名稱.Models」。
//********************************************************

namespace WebApplication2017_MVC_GuestBook.Models.Repo
{
                        
    public class UserTableRepository : IUserTableRepository, IDisposable    // *** 重點 ******
    {    //                                          // **********************   **********不寫IDisposable會出現這種「警告」https://msdn.microsoft.com/library/ms182172.aspx

        //**********   連結 MVC_UserDB 資料庫  *********************** (start)
        public MVC_UserDBContext _db =  new MVC_UserDBContext();
        //**********   連結 MVC_UserDB 資料庫  *********************** (end)

        public bool AddUser(UserTable _userTable)
        {
            try   {
                _db.UserTables.Add(_userTable);
                _db.SaveChanges();
                return true;
            }
            catch   {
                //throw new NotImplementedException();
                return false;
            }            
        }

        public bool DeleteUser(UserTable _userTable)
        {
            try
            {
                _db.UserTables.Remove(_userTable);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                //throw new NotImplementedException();
                return false;
            }
        }

        // Details。主表明細的「明細」。
        public UserTable GetUserById(int id)
        {
                return (_db.UserTables.Find(id));
                //throw new NotImplementedException();
        }


        // 搜尋。 
        public IQueryable<UserTable> GetUserByName(string id)
        {
                return (_db.UserTables.Where(s => s.UserName.Contains(id)));
                //throw new NotImplementedException();
            }

        public IQueryable<UserTable> ListAllUsers()
        {
            return (_db.UserTables);
            //throw new NotImplementedException();        
        }


        //public void Db_Dispose()
        //{   // 關閉資料庫的連結並釋放資源。
        //        _db.Dispose();
        //        throw new NotImplementedException();
        //}

        //========================================================
        // 不寫下面這一段IDisposable，
        // 「建置」=>「程式碼效能分析」會出現 "警告"訊息 https://msdn.microsoft.com/library/ms182172.aspx

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {   // dispose managed resources
                _db.Dispose();
            }
            // free native resources
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

}