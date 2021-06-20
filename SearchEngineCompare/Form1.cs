using SearchEngineCompare.DataAccess;
using SearchEngineCompare.Entity;
using SearchEngineCompare.Enums;
using SearchEngineCompare.Manager;
using SearchEngineCompare.Models.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchEngineCompare
{
    public partial class Form1 : Form
    {
        Guid setSearchTotalCount_searchCompareId;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            var resultFillComboBoxes = FillComboBoxes();
            if (!resultFillComboBoxes.Success)
            {
                MessageBox.Show($"{resultFillComboBoxes.Message}", $"Hata oluştu");
                return;
            }

            var resultFillDatagridview = FillDatagridview();
            if (!resultFillDatagridview.Success)
            {
                //MessageBox.Show($"{resultFillDatagridview.Message}", $"Hata oluştu");
                //return;
            }

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {

                // Genel kontrolleri bu aşamada yapıyoruz.
                var resultCheckPoint = CheckPointButtonSearch_Click();
                if (!resultCheckPoint.Success)
                {
                    MessageBox.Show($"{resultCheckPoint.Message}", $"Hata oluştu");
                    return;
                }


                string searchPhrase = textBoxSearchPhrase.Text.Trim();

                //Arama işlemini ve arama sonuçlarının kayıt işlemlerini bu aşamada yapıyoruz
                var resultSearchProcess = SearchProcess(searchPhrase);
                if (!resultCheckPoint.Success)
                {
                    MessageBox.Show($"{resultSearchProcess.Message}", $"Hata oluştu");
                    return;
                }

                var resultFillDatagridview = FillDatagridview();
                if (!resultFillDatagridview.Success)
                {
                    MessageBox.Show($"{resultFillDatagridview.Message}", $"Hata oluştu");
                    return;
                }



            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message}", $"Hata oluştu");

            }
            finally
            {
                // MessageBox.Show("İşlem bitti", "Bitti");
            }
        }



        //Genel kontrolleri burada yapacağız.
        private Result<string> CheckPointButtonSearch_Click()
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxSearchPhrase.Text.Trim()))
                {
                    return new Result<string>(false, string.Format("Arama alanı boş olamaz."), "Hatalı");
                }

                if (comboBoxSearchEngine1.SelectedIndex == 0)
                {
                    return new Result<string>(false, string.Format("Birinci arama motorunu seçiniz."), "Hatalı");
                }

                if (comboBoxSearchEngine2.SelectedIndex == 0)
                {
                    return new Result<string>(false, string.Format("İkinci arama motorunu seçiniz."), "Hatalı");
                }

                if (comboBoxSearchEngine1.SelectedIndex == comboBoxSearchEngine2.SelectedIndex)
                {
                    return new Result<string>(false, string.Format("Birinci ve ikinci arama motorunu aynı olamaz."), "Hatalı");
                }

                return new Result<string>(true, string.Format("Kontrol işlemleri başarılı"), "Başarılı");
            }
            catch (Exception exception)
            {

                return new Result<string>(false, $"Kontrol işlemlerinde hata oluştu.{Environment.NewLine} Hata:{exception.Message}", "Hatalı");
            }
        }

        // Birinci arama motoru için bilgileri dolduracağız
        private Result<double> FillWebBrowser1(string searchPhrase)
        {
            try
            {
                //webBrowser1.Navigate("about:blank");


                SearchEngineInfoManager searchEngineInfoManager = new SearchEngineInfoManager();

                SearchEngineInfoModel searchEngineInfoModel = searchEngineInfoManager.GetSearchEngineInfo((SearchEngineEnums)comboBoxSearchEngine1.SelectedIndex, searchPhrase);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                webBrowser1.Navigate(searchEngineInfoModel.SearchEngineFullUrl);

                while (webBrowser1.ReadyState!=WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }

                stopwatch.Stop();

                var resultTime = stopwatch.Elapsed.TotalMilliseconds;


                double webSearchResultCount;

                switch (comboBoxSearchEngine1.SelectedIndex)
                {
                    case (int)SearchEngineEnums.Google:
                        StringBuilder sb = new StringBuilder();
                        foreach (HtmlElement elm in webBrowser1.Document.All)
                            if (elm.GetAttribute("id") == "result-stats")
                                sb.Append(elm.InnerHtml);
                        var googleResultStats = sb.ToString();
                        if (!string.IsNullOrEmpty(googleResultStats))
                        {
                            string[] googleList = googleResultStats.Split(' ');
                            webSearchResultCount = double.Parse(googleList[1].ToString());
                            WebBrowser1SetSearchTotalCount(webSearchResultCount);
                        }


                        break;
                    case (int)SearchEngineEnums.Yandex:
                        StringBuilder sb2 = new StringBuilder();
                        foreach (HtmlElement elm in webBrowser1.Document.All)
                            if (elm.GetAttribute("className") == "serp-adv__found")
                                sb2.Append(elm.InnerText);
                        var yandexResult = sb2.ToString();
                        if (!string.IsNullOrEmpty(yandexResult))
                        {
                            string[] yandexList = yandexResult.Split(' ');

                            webSearchResultCount = yandexList[1] == "milyon" ? double.Parse(yandexList[0].ToString()) * 1000000 : double.Parse(yandexList[0].ToString()) * 1000;
                            WebBrowser1SetSearchTotalCount(webSearchResultCount);
                        }


                        break;
                    case (int)SearchEngineEnums.Bing:
                        StringBuilder sb3 = new StringBuilder();
                        foreach (HtmlElement elm in webBrowser1.Document.All)
                            if (elm.GetAttribute("className") == "sb_count")
                                sb3.Append(elm.InnerText);
                        var bingResult = sb3.ToString();
                        if (!string.IsNullOrEmpty(bingResult))
                        {
                            string[] bingList = bingResult.Split(' ');

                            webSearchResultCount = double.Parse(bingList[0].ToString());
                            WebBrowser1SetSearchTotalCount(webSearchResultCount);
                        }


                        break;
                    default:
                        break;
                }

                var resultFillDatagridview = FillDatagridview();
                if (!resultFillDatagridview.Success)
                {
                    MessageBox.Show($"{resultFillDatagridview.Message}", $"Hata oluştu");
                    
                }



                return new Result<double>(true, string.Format("ilk arama motoru işlemleri başarılı"), resultTime);
            }
            catch (Exception exception)
            {
                return new Result<double>(false, string.Format("İlk arama motoru işlemlerinde hata oluştu. Hata:{0}", exception.Message), 0);
            }
        }

        // İkinci arama motoru için bilgileri dolduracağız
        private Result<double> FillWebBrowser2(string searchPhrase)
        {
            try
            {
                //webBrowser2.Navigate("about:blank");
                SearchEngineInfoManager searchEngineInfoManager = new SearchEngineInfoManager();

                SearchEngineInfoModel searchEngineInfoModel = searchEngineInfoManager.GetSearchEngineInfo((SearchEngineEnums)comboBoxSearchEngine2.SelectedIndex, searchPhrase);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                webBrowser2.Navigate(searchEngineInfoModel.SearchEngineFullUrl);

                while (webBrowser2.ReadyState!=WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }

                stopwatch.Stop();

                var resultTime = stopwatch.Elapsed.TotalMilliseconds;

                double webSearchResultCount;

                switch (comboBoxSearchEngine2.SelectedIndex)
                {
                    case (int)SearchEngineEnums.Google:
                        StringBuilder sb = new StringBuilder();
                        foreach (HtmlElement elm in webBrowser2.Document.All)
                            if (elm.GetAttribute("id") == "result-stats")
                                sb.Append(elm.InnerHtml);
                        var googleResultStats = sb.ToString();
                        if (!string.IsNullOrEmpty(googleResultStats))
                        {
                            string[] googleList = googleResultStats.Split(' ');
                            webSearchResultCount = double.Parse(googleList[1].ToString());
                            WebBrowser2SetSearchTotalCount(webSearchResultCount);
                        }


                        break;
                    case (int)SearchEngineEnums.Yandex:
                        StringBuilder sb2 = new StringBuilder();
                        foreach (HtmlElement elm in webBrowser2.Document.All)
                            if (elm.GetAttribute("className") == "serp-adv__found")
                                sb2.Append(elm.InnerText);
                        var yandexResult = sb2.ToString();
                        if (!string.IsNullOrEmpty(yandexResult) )
                        {
                            string[] yandexList = yandexResult.Split(' ');

                            webSearchResultCount = yandexList[1] == "milyon" ? double.Parse(yandexList[0].ToString()) * 1000000 : double.Parse(yandexList[0].ToString()) * 1000;
                            WebBrowser2SetSearchTotalCount(webSearchResultCount);
                        }


                        break;
                    case (int)SearchEngineEnums.Bing:
                        StringBuilder sb3 = new StringBuilder();
                        foreach (HtmlElement elm in webBrowser2.Document.All)
                            if (elm.GetAttribute("className") == "sb_count")
                                sb3.Append(elm.InnerText);
                        var bingResult = sb3.ToString();
                        if (!string.IsNullOrEmpty(bingResult))
                        {
                            string[] bingList = bingResult.Split(' ');

                            webSearchResultCount = double.Parse(bingList[0].ToString());
                            WebBrowser2SetSearchTotalCount(webSearchResultCount);
                        }


                        break;
                    default:
                        break;

                }

                var resultFillDatagridview = FillDatagridview();
                if (!resultFillDatagridview.Success)
                {
                    MessageBox.Show($"{resultFillDatagridview.Message}", $"Hata oluştu");
                    
                }


                return new Result<double>(true, string.Format("İkinci arama motoru işlemleri başarılı"), resultTime);
            }
            catch (Exception exception)
            {
                return new Result<double>(false, string.Format("İkinci arama motoru işlemlerinde hata oluştu. Hata:{0}", exception.Message), 0);
            }
        }

        private Result<string> FillComboBoxes()
        {
            try
            {

                foreach (var item in Enum.GetValues(typeof(SearchEngineEnums)))
                {
                    comboBoxSearchEngine1.Items.Add(((SearchEngineEnums)item));
                    comboBoxSearchEngine2.Items.Add(((SearchEngineEnums)item));
                }

                comboBoxSearchEngine1.SelectedIndex = 0;
                comboBoxSearchEngine2.SelectedIndex = 0;


                return new Result<string>(true, string.Format("Seçim seçenekleri oluştu"), "Başarılı");
            }
            catch (Exception exception)
            {
                return new Result<string>(false, string.Format("Seçim seçenekleri oluşturulurken hata oluştu. Hata:{0}", exception.Message), "Hatalı");
            }
        }

        private Result<string> FillDatagridview()
        {
            try
            {


                var resultGetDatatable = SearchEngineDal.GetDatatableTop10Search();

                if (!resultGetDatatable.Success || resultGetDatatable.Data.Rows.Count <= 0)
                {
                    return new Result<string>(false, $"{resultGetDatatable.Message}", null);
                }

                dataGridView1.DataSource = resultGetDatatable.Data;


                return new Result<string>(true, string.Format("Kıyaslama seçenekleri oluştu"), "Başarılı");
            }
            catch (Exception exception)
            {
                return new Result<string>(false, string.Format("Kıyaslama seçenekleri oluşturulurken hata oluştu. Hata:{0}", exception.Message), "Hatalı");
            }
        }

        private Result<string> SearchProcess(string searchPhrase)
        {
            try
            {
                setSearchTotalCount_searchCompareId = Guid.NewGuid();

                Guid searchCompareId = Guid.NewGuid();

                string[] searchPhraseList = searchPhrase.Trim().Split(' ');
                int phraseTypeId = searchPhraseList.Count() > 1 ? 2 : 1;
                int measuredParameterId = 0;


                // birinci arama motoru için bilgileri dolduruyoruz.
                var resultFillWebBrowser1 = FillWebBrowser1(searchPhrase);
                if (!resultFillWebBrowser1.Success)
                {
                    //MessageBox.Show($"{resultFillWebBrowser1.Message}", $"Hata oluştu");
                    return new Result<string>(false, string.Format("İkinci arama motoru işlemlerinde hata oluştu. Hata:{0}", resultFillWebBrowser1.Message), "Hata"); ;
                }

                // birinci arama motoru için bilgileri dolduruyoruz.
                var resultFillWebBrowser2 = FillWebBrowser2(searchPhrase);
                if (!resultFillWebBrowser2.Success)
                {
                    //MessageBox.Show($"{resultFillWebBrowser2.Message}", $"Hata oluştu");
                    return new Result<string>(false, string.Format("İkinci arama motoru işlemlerinde hata oluştu. Hata:{0}", resultFillWebBrowser2.Message), "Hata");
                }



                //  ilk işlemi insert edeceğiz. 
                int searchEngineId1 = comboBoxSearchEngine1.SelectedIndex;
                double measuredParameterPerformance1 = resultFillWebBrowser1.Data;
                measuredParameterId = 1;

                Search search1 = new Search();
                search1.SearchCompareId = searchCompareId;
                search1.SearchPhrase = searchPhrase.Trim();
                search1.PhraseTypeId = phraseTypeId;
                search1.SearchEngineId = searchEngineId1;
                search1.MeasuredParameterId = measuredParameterId;
                search1.MeasuredParameterPerformance = measuredParameterPerformance1;

                var resultInsertSearch1 = SearchEngineDal.InsertSearch(search1);
                if (!resultInsertSearch1.Success)
                {
                    return new Result<string>(false, string.Format("Birinci arama motoru süre işlemi veritabanına ekleme hatası oluştu. Hata:{0}", resultInsertSearch1.Message), "Hata");
                }

                // ikinci işlemi insert edeceğiz.
                int searchEngineId2 = comboBoxSearchEngine2.SelectedIndex;
                double measuredParameterPerformance2 = resultFillWebBrowser2.Data;
                measuredParameterId = 1;

                Search search2 = new Search();
                search2.SearchCompareId = searchCompareId;
                search2.SearchPhrase = searchPhrase.Trim();
                search2.PhraseTypeId = phraseTypeId;
                search2.SearchEngineId = searchEngineId2;
                search2.MeasuredParameterId = measuredParameterId;
                search2.MeasuredParameterPerformance = measuredParameterPerformance2;

                var resultInsertSearch2 = SearchEngineDal.InsertSearch(search2);
                if (!resultInsertSearch2.Success)
                {
                    return new Result<string>(false, string.Format("İkinci arama motoru süre işlemi veritabanına ekleme hatası oluştu. Hata:{0}", resultInsertSearch2.Message), "Hata");
                }


                return new Result<string>(true, string.Format("Arama motoru işlemleri başarılı"), "Başarılı");
            }
            catch (Exception exception)
            {
                return new Result<string>(false, string.Format("Arama motoru işlemlerinde hata oluştu. Hata:{0}", exception.Message), "Hata");
            }
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
           

        }

        private void webBrowser2_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            
        }


        private Result<string> WebBrowser1SetSearchTotalCount(double webSearchResultCount)
        {

            try
            {


                Guid searchCompareId = setSearchTotalCount_searchCompareId;
                string searchPhrase = textBoxSearchPhrase.Text.Trim();

                string[] searchPhraseList = searchPhrase.Trim().Split(' ');
                int phraseTypeId = searchPhraseList.Count() > 1 ? 2 : 1;
                int measuredParameterId = 2;

                //  ilk işlemi insert edeceğiz. 
                int searchEngineId1 = comboBoxSearchEngine1.SelectedIndex;
                double measuredParameterPerformance1 = webSearchResultCount;
                measuredParameterId = 2;

                Search search1 = new Search();
                search1.SearchCompareId = searchCompareId;
                search1.SearchPhrase = searchPhrase.Trim();
                search1.PhraseTypeId = phraseTypeId;
                search1.SearchEngineId = searchEngineId1;
                search1.MeasuredParameterId = measuredParameterId;
                search1.MeasuredParameterPerformance = measuredParameterPerformance1;

                var resultInsertSearch1 = SearchEngineDal.InsertSearch(search1);
                if (!resultInsertSearch1.Success)
                {

                    return new Result<string>(false, string.Format("Birinci arama motoru süre işlemi veritabanına ekleme hatası oluştu. Hata:{0}", resultInsertSearch1.Message), "Hata");
                }


                return new Result<string>(true, string.Format("arama motoru işlemleri başarılı"), "Başarılı");
            }
            catch (Exception exception)
            {
                return new Result<string>(false, string.Format("arama motoru işlemlerinde hata oluştu. Hata:{0}", exception.Message), "Hata");
            }
        }

        private Result<string> WebBrowser2SetSearchTotalCount(double webSearchResultCount)
        {

            try
            {


                Guid searchCompareId = setSearchTotalCount_searchCompareId;
                string searchPhrase = textBoxSearchPhrase.Text.Trim();

                string[] searchPhraseList = searchPhrase.Trim().Split(' ');
                int phraseTypeId = searchPhraseList.Count() > 1 ? 2 : 1;
                int measuredParameterId = 2;

                //  ilk işlemi insert edeceğiz. 
                int searchEngineId1 = comboBoxSearchEngine2.SelectedIndex;
                double measuredParameterPerformance = webSearchResultCount;
                measuredParameterId = 2;

                Search search = new Search();
                search.SearchCompareId = searchCompareId;
                search.SearchPhrase = searchPhrase.Trim();
                search.PhraseTypeId = phraseTypeId;
                search.SearchEngineId = searchEngineId1;
                search.MeasuredParameterId = measuredParameterId;
                search.MeasuredParameterPerformance = measuredParameterPerformance;

                var resultInsertSearch = SearchEngineDal.InsertSearch(search);
                if (!resultInsertSearch.Success)
                {

                    return new Result<string>(false, string.Format("İkinci arama motoru sonuç sayısı işlemi veritabanına ekleme hatası oluştu. Hata:{0}", resultInsertSearch.Message), "Hata");
                }


                return new Result<string>(true, string.Format("arama motoru işlemleri başarılı"), "Başarılı");
            }
            catch (Exception exception)
            {
                return new Result<string>(false, string.Format("arama motoru işlemlerinde hata oluştu. Hata:{0}", exception.Message), "Hata");
            }
        }

        private void buttonForm2Open_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}