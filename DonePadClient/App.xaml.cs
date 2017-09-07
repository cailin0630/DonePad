using DonePadClient.Config;
using System.Diagnostics;
using System.Windows;

namespace DonePadClient
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //var stopwatch = new Stopwatch();
            //stopwatch.Start();
            //for (int i = 0; i < 100; i++)
            //{
            //    MySqlWithDapperSimple.InsertWithEntity(new todoinfo
            //    {
            //        title = "123",
            //        text = "qwe",

            //        insertDateTime = DateTime.Now,

            //        estimateDateTime = DateTime.Now.AddDays(2),

            //        userName = "jy",
            //    });
            //}
            //var ret = MySqlWithDapperSimple.GetList<todoinfo>();
            //var ret = MySqlWithDapperSimple.GetSingle<todoinfo>(3248);

            //var ret = MySqlWithDapperSimple.DeleteListWithEntity<todoinfo>(new{userName = "jy"});
            //var ret = MySqlWithDapperSimple.UpdateSingle(new todoinfo
            //{
            //    infoId = 3449 ,
            //    title = "123456",
            //    text = "qwe",

            //    insertDateTime = DateTime.Now,

            //    estimateDateTime = DateTime.Now.AddDays(2),

            //    userName = "jy",
            //});
            //stopwatch.Stop();
            //var timespan = stopwatch.ElapsedMilliseconds;
            LoginConfigHelper.ReadConfig();
            MongoDbConfigHelper.ReadConfig();
            base.OnStartup(e);
        }
    }
}