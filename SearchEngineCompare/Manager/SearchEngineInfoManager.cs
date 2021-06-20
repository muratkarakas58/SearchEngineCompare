using SearchEngineCompare.Enums;
using SearchEngineCompare.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngineCompare.Manager
{
    public class SearchEngineInfoManager
    {
        public SearchEngineInfoModel GetSearchEngineInfo(SearchEngineEnums searchEngineEnums, string searchPhrase)
        {
            SearchEngineInfoModel searchEngineInfoModel = new SearchEngineInfoModel();

            switch (searchEngineEnums)
            {
                case SearchEngineEnums.Google:
                    searchEngineInfoModel.SearchEnginePhrase = searchPhrase;
                    searchEngineInfoModel.SearchEngineBaseUrl = string.Format(@"https://www.google.com.tr/search?q=");
                    searchEngineInfoModel.SearchEngineFullUrl = string.Format("{0}{1}", searchEngineInfoModel.SearchEngineBaseUrl, searchEngineInfoModel.SearchEnginePhrase);
                    break;
                case SearchEngineEnums.Yandex:
                    searchEngineInfoModel.SearchEnginePhrase = searchPhrase;
                    searchEngineInfoModel.SearchEngineBaseUrl = string.Format(@"https://yandex.com.tr/search/?text=");
                    searchEngineInfoModel.SearchEngineFullUrl = string.Format("{0}{1}", searchEngineInfoModel.SearchEngineBaseUrl, searchEngineInfoModel.SearchEnginePhrase);
                    break;
                case SearchEngineEnums.Bing:
                    searchEngineInfoModel.SearchEnginePhrase = searchPhrase;
                    searchEngineInfoModel.SearchEngineBaseUrl = string.Format(@"https://www.bing.com/search?q=");
                    searchEngineInfoModel.SearchEngineFullUrl = string.Format("{0}{1}", searchEngineInfoModel.SearchEngineBaseUrl, searchEngineInfoModel.SearchEnginePhrase);
                    break;
                
                default:
                    //searchEngineInfoModel = SearchEngineEnums.NoSelection;
                    break;

            }


            return searchEngineInfoModel;
        }
    }
}
