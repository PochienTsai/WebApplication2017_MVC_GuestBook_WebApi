using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//************************************
using System.Collections.ObjectModel;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Script.Serialization;    //** 自己宣告、加入。JSON會用到。
                                                          //** 需自己動手「加入參考」System.Web.Extension.dll檔

// 針對 JavaScriptConverter類別提供的自訂轉換子。
// 本範例，資料來源：http://msdn.microsoft.com/zh-tw/library/system.web.script.serialization.javascriptconverter.aspx
// 當您繼承自 JavaScriptConverter 時，必須覆寫下列成員：
//       (1). SupportedTypes，請看下面的IEnumerable<Type>
//       (2). Serialize
//       (3). Deserialize
//************************************



namespace WindoswDesktopClassLibrary1
{

    public class ListItemCollectionConverter : JavaScriptConverter
    {

        //===(1). SupportedTypes ============================
        public override IEnumerable<Type> SupportedTypes
        {
            //Define the ListItemCollection as a supported type.
            get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(ListItemCollection) })); }
        }

        //===(2)=======================================   
        //Serialize，產生JSON格式的文字
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            ListItemCollection listType = obj as ListItemCollection;

            if (listType != null)
            {
                // Create the representation.
                Dictionary<string, object> result = new Dictionary<string, object>();
                ArrayList itemsList = new ArrayList();
                foreach (ListItem item in listType)
                {
                    //Add each entry to the dictionary.
                    Dictionary<string, object> listDict = new Dictionary<string, object>();
                    listDict.Add("Value", item.Value);
                    listDict.Add("Text", item.Text);
                    itemsList.Add(listDict);
                }
                result["List"] = itemsList;

                return result;
            }
            return new Dictionary<string, object>();
        }


        //===(3)=======================================
        //Deserialize，讀取JSON格式的文字
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            if (type == typeof(ListItemCollection))
            {
                // Create the instance to deserialize into.
                ListItemCollection list = new ListItemCollection();

                // Deserialize the ListItemCollection's items.
                ArrayList itemsList = (ArrayList)dictionary["List"];
                for (int i = 0; i < itemsList.Count; i++)
                    list.Add(serializer.ConvertToType<ListItem>(itemsList[i]));

                return list;
            }
            return null;
        }
    }
}
