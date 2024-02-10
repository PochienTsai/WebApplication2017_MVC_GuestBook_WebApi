namespace WebApplication2017_MVC_GuestBook.Models
{
    using System;
    using System.Data.Entity;    // *** ���I�I�I DbContext�ݭn�Ψ�I
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    // �i�H�Ѧҳo�g�峹�A�ܲM��  https://dotblogs.com.tw/supershowwei/2016/04/16/231826

    public partial class MVC_UserDBContext : DbContext
    {
        public MVC_UserDBContext()
            : base("name=MVC_UserDB")   // DB�s���r��A�ȥ��P�u��Ʈw�W�١v�ۦP�I�I
        {   // �_�h�N�|�M��u�M�צW�� . Models . EF�ɦW�v�o�˪���Ʈw�W�١C�]���䤣��A�N�|�����I
            // �H���d�ҦӨ��A�N�|�M��uWebApplication2017_MVC_GuestBook.Models.MVC_UserDBContext�v�o�Ӹ�Ʈw�W�١C
        }

        //**************************************************************
        public virtual DbSet<UserTable> UserTables { get; set; }   // �o�̪��W�٬O�Ƽơ]s�^
        // (1) UserTable ��� ��ƪ�̭����u�@���O���v�C
        // (2) DbSet<UserTable> ��� �uUserTable��ƪ�v�C
        // (3) virtual���ηN�H�H
        // ���G Navigation properties are typically defined as "virtual" 
        //         so that they can take advantage of certain Entity Framework functionality such as "lazy loading". 
        //**************************************************************
                
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTable>()
                .Property(e => e.UserSex)
                .IsFixedLength();
            // �i�Ѿ\�o�v�� https://www.youtube.com/watch?v=Sj-6MCmWqGc
        }

        //public System.Data.Entity.DbSet<WebApplication2017_MVC_GuestBook.ModelsLogin.db_user> db_user { get; set; }

        public System.Data.Entity.DbSet<WebApplication2017_MVC_GuestBook.Models2.UserTable2> UserTable2 { get; set; }

        public System.Data.Entity.DbSet<WebApplication2017_MVC_GuestBook.Models2.DepartmentTable2> DepartmentTable2 { get; set; }
    }
}
