namespace WebApplication2017_MVC_GuestBook.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;   // �C�����W�誺 [ ]�Ÿ��A�N�o�f�t�o�өR�W�Ŷ�
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;   // �f�t [Bind(Include=.......)]  ���R�W�Ŷ�

    [Table("UserTable")]
    // �i�H�Ѧҳo�g�峹�A�ܲM��  https://dotblogs.com.tw/supershowwei/2016/04/16/231826


    // �g�b Model�����O�ɸ̭��A�N���έ��Ʀa�g�b�s�W�B�R���B�ק�C�Ӱʧ@�����C
    // [Bind(Include = "UserId, UserName, UserSex, UserBirthDay, UserMobilePhone")]
    // �i�H�קK overposting attacks �]�L�h�o�G�^����  http://www.cnblogs.com/Erik_Xu/p/5497501.html
    public partial class UserTable
    {
        [Key]    // �D������]P.K.�^
        public int UserId { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(1)]
        public string UserSex { get; set; }

        public DateTime? UserBirthDay { get; set; }

        [StringLength(15)]
        public string UserMobilePhone { get; set; }
    }
}
