
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ElectronNET.API;
using Newtonsoft.Json;
using Pixstock.Nc.App.Core.Dao;

namespace Pixstock.Nc.App.Controllers.Workflow
{
    public class ContentMainWorkflowEventEmiter
    {
        public void Initialize()
        {
            // 受信したいAppIfメッセージと、対応するハンドラの呼び出しをハードコーディングする

            Electron.IpcMain.On("async-msg", (args) =>
            {
                Console.WriteLine("async-msg = " + args.GetType());
            });

            Electron.IpcMain.OnSync("EVT_TRNS_CONTENTLIST", (args) =>
            {
                Console.WriteLine("[ContentMainWorkflowEventEmiter][Initialize] : IN");
                // TODO: ワークフロー制御

                var mainWindow = Electron.WindowManager.BrowserWindows.First();
                Electron.IpcMain.Send(mainWindow,"MSG_SHOW_CONTENLIST",args);

                Console.WriteLine("[ContentMainWorkflowEventEmiter][Initialize] : メッセージ送信");
                return 0;
            });

            Electron.IpcMain.OnSync("EVT_TRNS_CONTENTPREVIEW", (args) =>
            {
                Console.WriteLine("[ContentMainWorkflowEventEmiter][EVT_TRNS_CONTENTPREVIEW] : IN");
                // TODO: ワークフロー制御

                var mainWindow = Electron.WindowManager.BrowserWindows.First();
                Electron.IpcMain.Send(mainWindow,"MSG_SHOW_CONTENTPREVIEW",args);

                Console.WriteLine("[ContentMainWorkflowEventEmiter][EVT_TRNS_CONTENTPREVIEW] : メッセージ送信");
                return 0;
            });

            Electron.IpcMain.OnSync("EAV_GETCATEGORY", (args) =>
            {
                long categoryId = long.Parse(args.ToString());
                CategoryDao catgeoryDao = new CategoryDao();
                var category = catgeoryDao.LoadCategory(categoryId);
                return JsonConvert.SerializeObject(category);
            });
            
            Electron.IpcMain.OnSync("EAV_GETSUBCATEGORY", (args) =>
            {
                long categoryId = long.Parse(args.ToString());
                CategoryDao catgeoryDao = new CategoryDao();
                var categoryList = catgeoryDao.GetSubCategory(categoryId);
                return JsonConvert.SerializeObject(categoryList);
            });

            Electron.IpcMain.OnSync("EAV_GETTHUMBNAIL", (args) => {
                var thumbnailHash = args.ToString();
                ThumbnailDao thumbnailDao = new ThumbnailDao();
                var thumbnail = thumbnailDao.LoadByThumbnailKey(thumbnailHash);
                return JsonConvert.SerializeObject(thumbnail);
            });

            Electron.IpcMain.OnSync("EAV_GET_CONTENTPREVIEW", (args) => {
                long contentId = long.Parse(args.ToString());
                ContentDao contentDao = new ContentDao();
                var content = contentDao.LoadContentData(contentId);
                return content;
            });
        }

        public void Dispose()
        {
            Electron.IpcMain.RemoveAllListeners("EVT_TRNS_CONTENTPREVIEW");
            Electron.IpcMain.RemoveAllListeners("EVT_TRNS_CONTENTLIST");
            Electron.IpcMain.RemoveAllListeners("EAV_GETCATEGORY");
            Electron.IpcMain.RemoveAllListeners("EAV_GETSUBCATEGORY");
            Electron.IpcMain.RemoveAllListeners("EAV_GETTHUMBNAIL");
            Electron.IpcMain.RemoveAllListeners("EAV_GET_CONTENTPREVIEW");
        }
    }
}