﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebApplication2017_MVC_GuestBook.WCF
{
    // 注意: 您可以使用 [重構] 功能表上的 [重新命名] 命令同時變更程式碼和組態檔中的介面名稱 "IService123"。
    [ServiceContract]
    public interface IService123
    {
        [OperationContract]
        string DoWork();
        //void DoWork();

        [OperationContract]
        string DoWork2(int a, int b);

        //[OperationContract]
        //string GetDateTime();
    }
}
