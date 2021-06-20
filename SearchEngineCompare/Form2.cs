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
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SearchEngineCompare
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
        }



        private void Form2_Load(object sender, EventArgs e)
        {
           

            var resultFillComboBoxes = FillComboBoxes();
            if (!resultFillComboBoxes.Success)
            {
                MessageBox.Show($"{resultFillComboBoxes.Message}", $"Hata oluştu");
                return;
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

        private void WebBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            // MessageBox.Show("nav");
            if (sender != null)
            {
                var tempWB = (WebBrowser)sender;
                //TextBox textBoxTemp = Application.OpenForms["Form2"].Controls["textBox1"] as TextBox;

                ////listBox1.Items.Add(webBrowser1.Url.OriginalString.ToString());

                //textBoxTemp.Invoke((Action)delegate {
                //    textBoxTemp.AppendText(tempWB.Url.Query.ToString());
                //} );

                //SearchEngineDal.InsertTempTest(tempWB.Url.OriginalString);

                //webBrowser1.Dispose();

            }
        }

        private void WebBrowser2_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            // MessageBox.Show("nav");
            if (sender != null)
            {
                var tempWB = (WebBrowser)sender;
                //TextBox textBoxTemp = Application.OpenForms["Form2"].Controls["textBox2"] as TextBox;

                ////listBox1.Items.Add(webBrowser1.Url.OriginalString.ToString());

                //textBoxTemp.Invoke((Action)delegate {
                //    textBoxTemp.AppendText(tempWB.Url.Query.ToString());
                //});

                //SearchEngineDal.InsertTempTest(tempWB.Url.OriginalString);
                //webBrowser1.Dispose();

            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            
            try
            {
                Thread t1 = new Thread(new ThreadStart(Process));
                t1.SetApartmentState(ApartmentState.STA);
                t1.IsBackground = true;
                t1.Start();


                //Process();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                
            }



        }

        public void Process()
        {
            try
            {
                                
                string searchPhrase = textBoxSearchPhrase.Text.Trim();
                List<int> searchEngineIdList = new List<int>();

                if (comboBoxSearchEngine1.InvokeRequired)
                {
                    comboBoxSearchEngine1.Invoke(new MethodInvoker(delegate { searchEngineIdList.Add(comboBoxSearchEngine1.SelectedIndex); }));
                }
                else
                {
                    searchEngineIdList.Add(comboBoxSearchEngine1.SelectedIndex);
                }
                if (comboBoxSearchEngine2.InvokeRequired)
                {
                    comboBoxSearchEngine2.Invoke(new MethodInvoker(delegate { searchEngineIdList.Add(comboBoxSearchEngine2.SelectedIndex); }));
                }
                else
                {
                    searchEngineIdList.Add(comboBoxSearchEngine2.SelectedIndex);
                }

                for (int i = 0; i < int.Parse(textBoxSearchLoop.Text.Trim()); i++)
                {


                    // Kontrolleri yap. Başarılı ise devam et. Değil ise süreci sonlandır
                    // Genel kontrolleri bu aşamada yapıyoruz.
                    var resultCheckPoint = CheckPointProcess();
                    if (!resultCheckPoint.Success)
                    {
                        MessageBox.Show($"{resultCheckPoint.Message}", $"Hata oluştu");
                        return;
                    }

                    Guid searchCompareId = Guid.NewGuid();

                    foreach (int itemsearchEngineId in searchEngineIdList)
                    {
                        // Her arama motoru için kontrolleri bu aşamada yapıyoruz.
                        SearchAndInsertDbRequest searchAndInsertDbRequest = new SearchAndInsertDbRequest();
                        searchAndInsertDbRequest.SearchPhrase = searchPhrase;
                        searchAndInsertDbRequest.SearchEngineId = itemsearchEngineId;
                        searchAndInsertDbRequest.SearchCompareId = searchCompareId;
                        var resultSearchAndInsertDb = SearchAndInsertDb(searchAndInsertDbRequest);
                        if (!resultSearchAndInsertDb.Success)
                        {
                            MessageBox.Show($"{resultSearchAndInsertDb.Message}", $"Hata oluştu");
                            return;
                        }

                    }



                }

                MessageBox.Show("bitti");
            }
            catch (Exception exception) 
            {

                MessageBox.Show($"{exception.Message}", $"Hata oluştu");
            }
            finally
            {
                
            }


        }



        //Genel kontrolleri burada yapacağız.
        private Result<string> CheckPointProcess()
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxSearchPhrase.Text.Trim()))
                {
                    return new Result<string>(false, string.Format("Arama alanı boş olamaz."), "Hatalı");
                }

                int comboBoxSearchEngine1SelectedIndex = 0;

                if (comboBoxSearchEngine1.InvokeRequired)
                {
                    comboBoxSearchEngine1.Invoke(new MethodInvoker(delegate { comboBoxSearchEngine1SelectedIndex = comboBoxSearchEngine1.SelectedIndex; }));
                }
                else
                {
                    comboBoxSearchEngine1SelectedIndex = comboBoxSearchEngine1.SelectedIndex;
                }

                if (comboBoxSearchEngine1SelectedIndex == 0)
                {
                    return new Result<string>(false, string.Format("Birinci arama motorunu seçiniz."), "Hatalı");
                }


                int comboBoxSearchEngine2SelectedIndex = 0;
                if (comboBoxSearchEngine2.InvokeRequired)
                {
                    comboBoxSearchEngine2.Invoke(new MethodInvoker(delegate { comboBoxSearchEngine2SelectedIndex = comboBoxSearchEngine2.SelectedIndex; }));
                }
                else
                {
                    comboBoxSearchEngine2SelectedIndex = comboBoxSearchEngine2.SelectedIndex;
                }


                if (comboBoxSearchEngine2SelectedIndex == 0)
                {
                    return new Result<string>(false, string.Format("İkinci arama motorunu seçiniz."), "Hatalı");
                }

                if (comboBoxSearchEngine1SelectedIndex == comboBoxSearchEngine2SelectedIndex)
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

        private Result<string> SearchAndInsertDb(SearchAndInsertDbRequest searchAndInsertDbRequest)
        {
            try
            {

                //ilk işlem için arama işlemini başlat.
                SearchProcessRequest searchProcessRequest = new SearchProcessRequest();
                searchProcessRequest.SearchPhrase = searchAndInsertDbRequest.SearchPhrase; ;
                searchProcessRequest.SearchEngineId = searchAndInsertDbRequest.SearchEngineId;

                var resultSearchProcess = SearchProcess(searchProcessRequest);
                if (!resultSearchProcess.Success)
                {

                    return new Result<string>(false, $"işlemlerinde hata oluştu.{Environment.NewLine} Hata:{resultSearchProcess.Message}", "Hatalı");
                }



                //Todo: ifade türünü tespit et. tek kelime/çok kelime
                string[] searchPhraseList = searchAndInsertDbRequest.SearchPhrase.Split(' ');
                int phraseTypeId = searchPhraseList.Count() > 1 ? 2 : 1;


                // Sonuç bulma süresi. Milisaniye
                Search search1 = new Search();
                search1.SearchCompareId = searchAndInsertDbRequest.SearchCompareId;
                search1.SearchPhrase = searchAndInsertDbRequest.SearchPhrase;
                search1.PhraseTypeId = phraseTypeId;
                search1.SearchEngineId = searchAndInsertDbRequest.SearchEngineId;
                search1.MeasuredParameterId = 1;
                search1.MeasuredParameterPerformance = resultSearchProcess.Data.TotalMilliseconds;

                var resultInsertSearch1 = SearchEngineDal.InsertSearch(search1);
                if (!resultInsertSearch1.Success)
                {
                    return new Result<string>(false, string.Format("Arama motoru süre işlemi veritabanına ekleme hatası oluştu. Hata:{0}", resultInsertSearch1.Message), "Hata");
                }


                // Arama motorunda gösterilen toplam sonuç sayısı. Adet
                Search search2 = new Search();
                search2.SearchCompareId = searchAndInsertDbRequest.SearchCompareId;
                search2.SearchPhrase = searchAndInsertDbRequest.SearchPhrase;
                search2.PhraseTypeId = phraseTypeId;
                search2.SearchEngineId = searchAndInsertDbRequest.SearchEngineId;
                search2.MeasuredParameterId = 2;
                search2.MeasuredParameterPerformance = resultSearchProcess.Data.TotalSearchResult;

                var resultInsertSearch2 = SearchEngineDal.InsertSearch(search2);
                if (!resultInsertSearch2.Success)
                {
                    return new Result<string>(false, string.Format("Arama motoru süre işlemi veritabanına ekleme hatası oluştu. Hata:{0}", resultInsertSearch2.Message), "Hata");
                }



                return new Result<string>(true, string.Format("işlemleri başarılı"), "Başarılı");
            }
            catch (Exception exception)
            {

                return new Result<string>(false, $"işlemlerinde hata oluştu.{Environment.NewLine} Hata:{exception.Message}", "Hatalı");
            }
        }

        private Result<SearchProcessResponse> SearchProcess(SearchProcessRequest searchProcessRequest)
        {
            SearchProcessResponse response = new SearchProcessResponse();
            try
            {

                SearchEngineInfoManager searchEngineInfoManager = new SearchEngineInfoManager();

                SearchEngineInfoModel searchEngineInfoModel = searchEngineInfoManager.GetSearchEngineInfo((SearchEngineEnums)searchProcessRequest.SearchEngineId, searchProcessRequest.SearchPhrase);

                WebBrowser wb = new WebBrowser();
                string data = "";

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                wb.Navigate(searchEngineInfoModel.SearchEngineFullUrl);
                while (wb.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }

                stopwatch.Stop();
                response.TotalMilliseconds = stopwatch.Elapsed.TotalMilliseconds;

                data = wb.DocumentText;

                switch (searchProcessRequest.SearchEngineId)
                {
                    case (int)SearchEngineEnums.Google:
                        StringBuilder sb = new StringBuilder();
                        foreach (HtmlElement elm in wb.Document.All)
                            if (elm.GetAttribute("id") == "result-stats")
                                sb.Append(elm.InnerHtml);
                        var googleResultStats = sb.ToString();
                        if (!string.IsNullOrEmpty(googleResultStats))
                        {
                            string[] googleList = googleResultStats.Split(' ');
                            response.TotalSearchResult = double.Parse(googleList[1].ToString());

                        }


                        break;
                    case (int)SearchEngineEnums.Yandex:
                        StringBuilder sb2 = new StringBuilder();
                        foreach (HtmlElement elm in wb.Document.All)
                            if (elm.GetAttribute("className") == "serp-adv__found")
                                sb2.Append(elm.InnerText);
                        var yandexResult = sb2.ToString();
                        if (!string.IsNullOrEmpty(yandexResult))
                        {
                            string[] yandexList = yandexResult.Split(' ');

                            response.TotalSearchResult = yandexList[1] == "milyon" ? double.Parse(yandexList[0].ToString()) * 1000000 : double.Parse(yandexList[0].ToString()) * 1000;

                        }


                        break;
                    case (int)SearchEngineEnums.Bing:
                        StringBuilder sb3 = new StringBuilder();
                        foreach (HtmlElement elm in wb.Document.All)
                            if (elm.GetAttribute("className") == "sb_count")
                                sb3.Append(elm.InnerText);
                        var bingResult = sb3.ToString();
                        if (!string.IsNullOrEmpty(bingResult))
                        {
                            string[] bingList = bingResult.Split(' ');

                            response.TotalSearchResult = double.Parse(bingList[0].ToString());

                        }
                        break;
                    default:
                        break;
                }

                wb.Dispose();

                return new Result<SearchProcessResponse>(true, $"Başarılı", response);
            }
            catch (Exception exception)
            {

                return new Result<SearchProcessResponse>(false, $"Arama işlemlerinde hata oluştu.{Environment.NewLine} Hata:{exception.Message}", null);
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void buttonShowChart_Click(object sender, EventArgs e)
        {
            ShowChartProcess();
        }

        private void ShowChartProcess()
        {
            try
            {
                int showRowCount = int.Parse(textBoxShowCount.Text.Trim());

                //Point leri temizleme.
                foreach (var series in chartMeasuredParameter1.Series)
                {
                    series.Points.Clear();
                }

                foreach (var series in chartMeasuredParameter1Average.Series)
                {
                    series.Points.Clear();
                }

                foreach (var series in chartMeasuredParameter2.Series)
                {
                    series.Points.Clear();
                }

                foreach (var series in chartMeasuredParameter2Average.Series)
                {
                    series.Points.Clear();
                }

                foreach (var item in Enum.GetValues(typeof(SearchEngineEnums)))
                {
                    if (((SearchEngineEnums)item)==SearchEngineEnums.NoSelection)
                    {
                        continue;
                    }

                    var resultFillChart1 = FillChart1(((SearchEngineEnums)item), showRowCount);
                    if (!resultFillChart1.Success)
                    {
                        MessageBox.Show($"{resultFillChart1.Message}", $"Hata oluştu");
                        return;
                    }

                    var resultFillChart2 = FillChart2(((SearchEngineEnums)item), showRowCount);
                    if (!resultFillChart2.Success)
                    {
                        MessageBox.Show($"{resultFillChart2.Message}", $"Hata oluştu");
                        return;
                    }

                }

               


            }
            catch (Exception exception)
            {

                MessageBox.Show($"{exception.Message}", $"Hata oluştu"); 
            }

        }

        private Result<string> FillChart1(SearchEngineEnums searchEngineEnum, int showRowCount) 
        {
            try
            {
                string seriesName = searchEngineEnum.ToString(); 

                if (chartMeasuredParameter1.Series.IsUniqueName(seriesName))
                {
                    chartMeasuredParameter1.Series.Add(seriesName);
                }

                if (chartMeasuredParameter1Average.Series.IsUniqueName(seriesName))
                {
                    chartMeasuredParameter1Average.Series.Add(seriesName);
                }


                //chartMeasuredParameter.Series[seriesName].ChartType = SeriesChartType.Spline;

                //chartMeasuredParameter1Average.Series[seriesName].ChartType = SeriesChartType.Pie;

                var resultGetDatatableFromSearchTopChoiceBySearchEngineIdAndMeasuredParameterId = SearchEngineDal.GetDatatableFromSearchTopChoiceBySearchEngineIdAndMeasuredParameterId(((int)searchEngineEnum), 1, showRowCount);
                if (!resultGetDatatableFromSearchTopChoiceBySearchEngineIdAndMeasuredParameterId.Success)
                {
                    //MessageBox.Show($"{resultGetDatatableFromSearchTopChoiceBySearchEngineIdAndMeasuredParameterId.Message}", $"Hata oluştu");
                    return new Result<string>(false, $"işlemlerinde hata oluştu.{Environment.NewLine} Hata:{resultGetDatatableFromSearchTopChoiceBySearchEngineIdAndMeasuredParameterId.Message}", "Hatalı"); ;
                }

                double totalMeasured =0;
                foreach (DataRow item in resultGetDatatableFromSearchTopChoiceBySearchEngineIdAndMeasuredParameterId.Data.Rows)
                {
                    double itemMeasuredParameterPerformance = double.Parse(item["MeasuredParameterPerformance"].ToString());
                    chartMeasuredParameter1.Series[seriesName].Points.Add(itemMeasuredParameterPerformance);
                    totalMeasured += itemMeasuredParameterPerformance;
                }
                double averageMeasured = totalMeasured / resultGetDatatableFromSearchTopChoiceBySearchEngineIdAndMeasuredParameterId.Data.Rows.Count;
                chartMeasuredParameter1Average.Series[seriesName].Points.Add(averageMeasured);

                return new Result<string>(true, string.Format("işlemleri başarılı"), "Başarılı");
            }
            catch (Exception exception)
            {

                return new Result<string>(false, $"işlemlerinde hata oluştu.{Environment.NewLine} Hata:{exception.Message}", "Hatalı");
            }

        }

        private Result<string> FillChart2(SearchEngineEnums searchEngineEnum, int showRowCount)
        {
            try
            {
                string seriesName = searchEngineEnum.ToString(); 

                if (chartMeasuredParameter2.Series.IsUniqueName(seriesName))
                {
                    chartMeasuredParameter2.Series.Add(seriesName);
                }

                if (chartMeasuredParameter2Average.Series.IsUniqueName(seriesName))
                {
                    chartMeasuredParameter2Average.Series.Add(seriesName);
                }

                //chartMeasuredParameter.Series[seriesName].ChartType = SeriesChartType.Spline;
                //chartMeasuredParameter2Average.Series[seriesName].ChartType = SeriesChartType.Pie;

                var resultGetDatatableFromSearchTopChoiceBySearchEngineIdAndMeasuredParameterId = SearchEngineDal.GetDatatableFromSearchTopChoiceBySearchEngineIdAndMeasuredParameterId(((int)searchEngineEnum), 2, showRowCount);
                if (!resultGetDatatableFromSearchTopChoiceBySearchEngineIdAndMeasuredParameterId.Success)
                {
                    //MessageBox.Show($"{resultGetDatatableFromSearchTopChoiceBySearchEngineIdAndMeasuredParameterId.Message}", $"Hata oluştu");
                    return new Result<string>(false, $"işlemlerinde hata oluştu.{Environment.NewLine} Hata:{resultGetDatatableFromSearchTopChoiceBySearchEngineIdAndMeasuredParameterId.Message}", "Hatalı"); ;
                }

                double totalMeasured = 0;
                foreach (DataRow item in resultGetDatatableFromSearchTopChoiceBySearchEngineIdAndMeasuredParameterId.Data.Rows)
                {
                    double itemMeasuredParameterPerformance = double.Parse(item["MeasuredParameterPerformance"].ToString());
                    chartMeasuredParameter2.Series[seriesName].Points.Add(itemMeasuredParameterPerformance);
                    totalMeasured += itemMeasuredParameterPerformance;
                }
                double averageMeasured = totalMeasured / resultGetDatatableFromSearchTopChoiceBySearchEngineIdAndMeasuredParameterId.Data.Rows.Count;
                chartMeasuredParameter2Average.Series[seriesName].Points.Add(averageMeasured);

                return new Result<string>(true, string.Format("işlemleri başarılı"), "Başarılı");
            }
            catch (Exception exception)
            {

                return new Result<string>(false, $"işlemlerinde hata oluştu.{Environment.NewLine} Hata:{exception.Message}", "Hatalı");
            }

        }


    }
}
