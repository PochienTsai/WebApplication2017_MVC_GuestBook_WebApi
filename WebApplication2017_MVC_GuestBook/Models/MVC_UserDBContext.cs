namespace WebApplication2017_MVC_GuestBook.Models
{
    using System;
    using System.Data.Entity;    // *** 重點！！ DbContext需要用到！
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    // 可以參考這篇文章，很清楚  https://dotblogs.com.tw/supershowwei/2016/04/16/231826

    public partial class MVC_UserDBContext : DbContext
    {
        public MVC_UserDBContext()
            : base("name=MVC_UserDB")   // DB連結字串，務必與「資料庫名稱」相同！！
        {   // 否則將會尋找「專案名稱 . Models . EF檔名」這樣的資料庫名稱。因為找不到，就會報錯！
            // 以本範例而言，將會尋找「WebApplication2017_MVC_GuestBook.Models.MVC_UserDBContext」這個資料庫名稱。
        }

        //**************************************************************
        public virtual DbSet<UserTable> UserTables { get; set; }   // 這裡的名稱是複數（s）
        // (1) UserTable 表示 資料表裡面的「一筆記錄」。
        // (2) DbSet<UserTable> 表示 「UserTable資料表」。
        // (3) virtual的用意？？
        // 答： Navigation properties are typically defined as "virtual" 
        //         so that they can take advantage of certain Entity Framework functionality such as "lazy loading". 
        //**************************************************************
                
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTable>()
                .Property(e => e.UserSex)
                .IsFixedLength();
            // 可參閱這影片 https://www.youtube.com/watch?v=Sj-6MCmWqGc
        }

        //public System.Data.Entity.DbSet<WebApplication2017_MVC_GuestBook.ModelsLogin.db_user> db_user { get; set; }

        public System.Data.Entity.DbSet<WebApplication2017_MVC_GuestBook.Models2.UserTable2> UserTable2 { get; set; }

        public System.Data.Entity.DbSet<WebApplication2017_MVC_GuestBook.Models2.DepartmentTable2> DepartmentTable2 { get; set; }
    }
}
