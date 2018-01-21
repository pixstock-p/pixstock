namespace Pixstock.Nc.App.Core.Dao {
    public class ContentDao {
        
        /// <summary>
        /// コンテント情報を読み込みます。
        /// </summary>
        /// <param name="contentId"></param>
        public string LoadContentData(long contentId) {
            return "cli/Sample01/ContentImageFile/" + contentId;
        }
    }
}